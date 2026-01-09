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
    [SerializeField] private GameObject animalIconPrefab;
    public void HandleDrop(int fromIndex, int toIndex)
    {
        if (boardManager == null || mergeManager == null) return;

        mergeManager.TryMergeOrSwap(boardManager, fromIndex, toIndex);

        Refresh();

        // 다음 단계에서 Refresh() 붙일 예정
        // Refresh();
    }

    private void Awake()
    {
        if (boardManager == null) boardManager = FindFirstObjectByType<BoardManager>();
        if (mergeManager == null) mergeManager = FindFirstObjectByType<MergeManager>();

        // SlotView 자동 수집 (BoardGrid 아래에 붙어있다면 이걸로 잡힘)
        if (slotViews == null || slotViews.Length == 0)
            slotViews = GetComponentsInChildren<SlotView>(includeInactive: true);

        if (slotViews != null && slotViews.Length > 0)
        {
            slotContents = new RectTransform[slotViews.Length];
            for (int i = 0; i < slotViews.Length; i++)
            {
                // SlotView가 붙은 오브젝트(=Slot)의 자식 중 "Content"를 찾음
                var t = slotViews[i].transform.Find("Content");
                slotContents[i] = t as RectTransform;
            }
        }
    }

    private void Start()
    {
        Refresh();
    }


    

    public void Refresh()
    {
        if (boardManager == null || slotViews == null || slotContents == null) return;

        // SlotView가 index를 들고 있으므로, index 기준으로 업데이트
        for (int i = 0; i < slotViews.Length; i++)
        {
            int slotIndex = slotViews[i].Index;
            if (slotIndex < 0 || slotIndex >= boardManager.SlotCount) continue;

            var content = slotContents[i];
            if (content == null) continue;

            bool hasAnimal = boardManager.TryGetAt(slotIndex, out var animal);

            // content 아래에 AnimalView가 있는지 확인
            var existingAnimalView = content.GetComponentInChildren<Drag>(includeInactive: true);

            if (!hasAnimal)
            {
                // 보드에 동물 없으면 UI 아이콘 제거
                if (existingAnimalView != null)
                    Destroy(existingAnimalView.gameObject);
                continue;
            }

            // 동물이 있는데 아이콘이 없으면 생성(프리팹 권장)
            if (existingAnimalView == null)
            {
                if (animalIconPrefab == null)
                {
                    // 프리팹이 없으면 "현재는 생성 못함" (드래그 테스트 단계에서는 OK)
                    continue;
                }

                var go = Instantiate(animalIconPrefab, content);
                existingAnimalView = go.GetComponent<Drag>();
            }

            // 아이콘이 있으면 slotIndex 바인딩 (드롭 시 fromIndex가 이 값으로 들어감)
            existingAnimalView.BindSlotIndex(slotIndex);

            // (선택) 여기서 tier에 따라 스프라이트/텍스트 갱신 가능
            // 예: 이미지 색, TMP 표시 등
        }
    }
}
