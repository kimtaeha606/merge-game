//boardview 구독이유는 좌표를 economymanager에게 받고 씬에 있는 그 좌표를 월드 좌표로 변환하기 위해서 이다.

using UnityEngine;

public class RevenueView : MonoBehaviour
{
    [SerializeField] private EconomyManager economyManager;
    //[SerializeField] private BoardView boardView;

    private void OnEnable()
    {
        if (economyManager != null)
            economyManager.OnRevenueTick += HandleRevenueTick;
    }

    private void OnDisable()
    {
        if (economyManager != null)
            economyManager.OnRevenueTick -= HandleRevenueTick;
    }

    private void HandleRevenueTick(int slotIndex, int amount)
    {
        //Vector3 pos = boardView.GetSlotWorldPosition(slotIndex);
        //SpawnPopup(pos, amount);
    }

    private void SpawnPopup(Vector3 worldPos, int amount)
    {
        // 프리팹 Instantiate + amount 표시 + 애니메이션
    }
}