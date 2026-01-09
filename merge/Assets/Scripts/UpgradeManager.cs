using System;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private MoneyManager money;
    [SerializeField] private DiceManager dice;

    public event Action<int> OnDiceUpgradeSucceeded; // newLevel
    public event Action OnDiceUpgradeFailed;

    public void TryBuyDiceUpgrade()
    {
        int cost = dice.GetUpgradeCost();
        if (!money.TrySpend(cost))
        {
            OnDiceUpgradeFailed?.Invoke();
            return;
        }

        dice.ApplyUpgrade();
        OnDiceUpgradeSucceeded?.Invoke(dice.Level);
    }
}
