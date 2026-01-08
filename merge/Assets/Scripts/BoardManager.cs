using UnityEngine;
using System;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour
{
    private object?[] slots;

    /// <summary>
    /// 3×3 보드를 빈 상태로 리셋한다.
    /// GameManager에서 이 함수를 호출한다.
    /// </summary>
    public void ResetBoard()
    {
        slots = new object?[9]; // 기본값 null → 전부 빈 칸
    }


    /// <summary>
    /// 랜덤 빈 칸 인덱스를 반환한다.
    /// 빈 칸이 없으면 false.
    /// (보드는 '자리를 정해주는' 역할만 함)
    /// </summary>
    public bool TryGetRandomEmptyIndex(out int index)
    {
        index = -1;

        // 빈 칸 수집
        List<int> empties = null;
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] != null) continue;

            empties ??= new List<int>(9);
            empties.Add(i);
        }

        if (empties == null || empties.Count == 0)
            return false;

        int pick = UnityEngine.Random.Range(0, empties.Count);
        index = empties[pick];
        return true;
    }

    /// <summary>
    /// 지정한 칸에 배치한다. (칸이 비어있을 때만)
    /// </summary>
    public bool TryPlaceAt(int index, AnimalInstance instance)
    {
        if (slots == null || slots.Length != 9) return false;
        if (index < 0 || index >= 9) return false;
        if (instance == null) return false;
        if (slots[index] != null) return false;

        slots[index] = instance;
        return true;
    }
    

}
