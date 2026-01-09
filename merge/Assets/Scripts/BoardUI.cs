using UnityEngine;

public sealed class BoardUI : MonoBehaviour
{
    [SerializeField] private BoardManager boardManager;
    [SerializeField] private MergeManager mergeManager;

    public void HandleDrop(int fromIndex, int toIndex)
    {
        if (boardManager == null || mergeManager == null) return;

        mergeManager.TryMergeOrSwap(boardManager, fromIndex, toIndex);

        // 다음 단계에서 Refresh() 붙일 예정
        // Refresh();
    }
}
