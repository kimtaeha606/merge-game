using UnityEngine;

public sealed class MergeManager : MonoBehaviour
{
    /// <summary>
    /// 드래그 앤 드롭용 처리:
    /// 1) to가 비어있으면 Move
    /// 2) 같은 Tier이고 to가 업그레이드 가능하면 Merge (to가 업그레이드 슬롯, from은 삭제)
    /// 3) 그 외에는 Swap
    /// </summary>
    
    public bool TryMergeOrSwap(BoardManager board, int fromIndex, int toIndex)
    {
        if (board == null) return false;
        if (fromIndex == toIndex) return false;

        if (!board.TryGetAt(fromIndex, out var fromAnimal))
            return false;

        if (!board.TryGetAt(toIndex, out var toAnimal))
        {
            board.ClearAt(fromIndex);
            board.TrySetAt(toIndex, fromAnimal);
            return true;
        }
        if (fromAnimal.Tier == toAnimal.Tier)
        {
            var next = toAnimal.Data != null ? toAnimal.Data.nextTierAnimal : null;

            // 최상위(=nextTierAnimal null)면 머지 불가 → Swap으로 떨어짐
            if (next != null)
            {
                // to 업그레이드
                toAnimal.TrySetData(next, restTickTimer: true);

                // from 삭제
                board.ClearAt(fromIndex);

                Debug.Log($"Merged T{fromAnimal.Tier} -> T{toAnimal.Tier} (slot {toIndex})");
                return true;
            }
        }
        board.TrySwap(fromIndex, toIndex);
        return true;

    }
}
