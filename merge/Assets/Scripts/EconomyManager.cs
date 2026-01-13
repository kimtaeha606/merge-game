using Unity.VisualScripting;
using System;
using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private BoardManager boardManager;
    [SerializeField] private MoneyManager moneyManager;
    
    public event Action<int, int> OnRevenueTick; 
    public void Tick(float tickDelta)
    {
        if (boardManager == null || moneyManager == null) return;

        int totalPayout = 0;

        for (int i = 0; i < boardManager.SlotCount; i++)
        {
            if (!boardManager.TryGetAt(i, out var animal)) continue;

            animal.TickTimer += tickDelta;

            float interval = animal.TickInterval;
            if (interval <= 0f) continue; // 0이면 while 무한루프 방지

            while (animal.TickTimer >= interval)
            {
                animal.TickTimer -= interval;

                int amount = animal.IncomePerTick;
                if (amount <= 0) continue;

                totalPayout += amount;

                // "틱마다" 팝업 발행 (여기서 RevenueView가 팝업 생성)
                OnRevenueTick?.Invoke(i, amount);
            }
        }

        if (totalPayout > 0)
            moneyManager.Add(totalPayout);
    }
    
}
