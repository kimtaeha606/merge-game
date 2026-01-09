using Unity.VisualScripting;
using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private BoardManager boardManager;
    [SerializeField] private MoneyManager moneyManager;

    public void Tick(float tickDelta)
    {
        if (boardManager == null || moneyManager == null) return;

        int payout = 0;

        for (int i = 0; i < boardManager.SlotCount; i++)
        {
            if (!boardManager.TryGetAt(i, out var animal)) continue;

            animal.TickTimer += tickDelta;

            float interval = animal.TickInterval;
            if (interval < 0f) continue;

            while (animal.TickTimer >= interval)
            {
                animal.TickTimer -= interval;
                payout += animal.IncomePerTick;
            }
        }

        if (payout > 0)
            moneyManager.Add(payout);
    }
    
}
