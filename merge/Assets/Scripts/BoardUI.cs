using Unity.VisualScripting;
using UnityEngine;

public sealed class BoardUI : MonoBehaviour
{
    [Header("Auto (optional)")]
    [SerializeField] private BoardManager boardManager;
    [SerializeField] private MergeManager mergeManager;

    [Header("UI")]
    [SerializeField] private SlotView[] slotViews;
    [SerializeField] private RectTransform[] slotContents;

    [Header("Prefab")]
    [SerializeField] private GameObject animalIconPrefab; // Drag + CanvasGroup + Image 붙은 프리팹

    // 슬롯마다 고정으로 들고 있을 Drag(아이콘)
    private Drag[] drags;

    private void Awake()
    {
        if (boardManager == null) boardManager = FindFirstObjectByType<BoardManager>();
        if (mergeManager == null) mergeManager = FindFirstObjectByType<MergeManager>();

        // SlotView 자동 수집
        if (slotViews == null || slotViews.Length == 0)
            slotViews = GetComponentsInChildren<SlotView>(includeInactive: true);

        // Content 수집
        slotContents = new RectTransform[slotViews.Length];
        for (int i = 0; i < slotViews.Length; i++)
        {
            slotContents[i] = slotViews[i].transform.Find("Content") as RectTransform;
        }

        // ★ 슬롯마다 Drag 1개를 미리 확보(없으면 생성)
        drags = new Drag[slotViews.Length];

        for (int i = 0; i < slotViews.Length; i++)
        {
            var content = slotContents[i];
            if (content == null) continue;

            // 이미 Drag가 있으면 그걸 사용
            var existing = content.GetComponentInChildren<Drag>(includeInactive: true);
            if (existing != null)
            {
                drags[i] = existing;
                continue;
            }

            // 없으면 프리팹으로 생성
            if (animalIconPrefab == null)
            {
                Debug.LogWarning("[BoardUI] animalIconPrefab is null. Cannot create drag icons.");
                continue;
            }

            var go = Instantiate(animalIconPrefab, content);
            drags[i] = go.GetComponent<Drag>();

            if (drags[i] == null)
                Debug.LogError("[BoardUI] animalIconPrefab does not have Drag component.");
        }
    }

    private void Start()
    {
        Refresh();
    }

    public void HandleDrop(int fromIndex, int toIndex)
    {
        if (boardManager == null || mergeManager == null) return;

        mergeManager.TryMergeOrSwap(boardManager, fromIndex, toIndex);
        Refresh();
    }

    public void Refresh()
    {
        if (boardManager == null || slotViews == null || drags == null) return;

        for (int i = 0; i < slotViews.Length; i++)
        {
            int slotIndex = slotViews[i].Index;
            if (slotIndex < 0 || slotIndex >= boardManager.SlotCount) continue;

            bool hasAnimal = boardManager.TryGetAt(slotIndex, out var animal);

            var drag = drags[i];
            if (drag == null) continue;

            // 드래그 아이콘이 "내가 어느 슬롯 소속인지" 갱신
            drag.BindSlotIndex(slotIndex);

            // 보드에 동물이 있으면 보이고, 없으면 숨긴다
            drag.gameObject.SetActive(hasAnimal);

            // (다음 단계) 에셋 이미지/텍스트 갱신은 여기서
            // if (hasAnimal) drag.SetSprite(animal.Data.icon);

            drag.BindSlotIndex(slotIndex);
            drag.gameObject.SetActive(hasAnimal);

            if (hasAnimal) drag.SetIcon(animal.Data.sprite);
            else drag.SetIcon(null);
        }
    }
}
