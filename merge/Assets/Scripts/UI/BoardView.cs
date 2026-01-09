using UnityEngine;

public class BoardView : MonoBehaviour
{
    [SerializeField] private BoardManager boardManager;
    [SerializeField] private RectTransform[] slotParents; // 9
    [SerializeField] private AnimalView animalPrefab;

    private AnimalView[] spawned;

    private void Awake()
    {
        spawned = new AnimalView[9];
    }

    private void OnEnable()
    {
        if (boardManager == null) return;
        boardManager.OnPlaced += HandlePlaced;
        boardManager.OnCleared += HandleCleared;
        boardManager.OnSwapped += HandleSwapped;
        boardManager.OnReset += HandleReset;
    }

    private void OnDisable()
    {
        if (boardManager == null) return;
        boardManager.OnPlaced -= HandlePlaced;
        boardManager.OnCleared -= HandleCleared;
        boardManager.OnSwapped -= HandleSwapped;
        boardManager.OnReset -= HandleReset;
    }

    private void HandlePlaced(int index, AnimalInstance inst)
    {
        if (!IsValidIndex(index)) return;

        if (spawned[index] != null)
            Destroy(spawned[index].gameObject);

        var view = Instantiate(animalPrefab, slotParents[index]);
        spawned[index] = view;

        view.Bind(inst.Data); // ★ 여기
    }

    private void HandleCleared(int index)
    {
        if (!IsValidIndex(index)) return;

        if (spawned[index] != null)
        {
            Destroy(spawned[index].gameObject);
            spawned[index] = null;
        }
    }

    private void HandleSwapped(int a, int b)
    {
        if (!IsValidIndex(a) || !IsValidIndex(b)) return;

        (spawned[a], spawned[b]) = (spawned[b], spawned[a]);

        if (spawned[a] != null) spawned[a].transform.SetParent(slotParents[a], false);
        if (spawned[b] != null) spawned[b].transform.SetParent(slotParents[b], false);
    }

    private void HandleReset()
    {
        for (int i = 0; i < spawned.Length; i++)
        {
            if (spawned[i] != null) Destroy(spawned[i].gameObject);
            spawned[i] = null;
        }
    }

    private bool IsValidIndex(int i)
    {
        return slotParents != null
               && i >= 0
               && i < slotParents.Length
               && i < spawned.Length
               && slotParents[i] != null;
    }
}
