using System;
using UnityEngine;

public sealed class MoneyManager : MonoBehaviour
{
    [Header("State")]
    [SerializeField] private int money = 0;

    public int Money => money;

    public event Action<int> OnMoneyChanged;

    public void ResetMoney(int startMoney = 0)
    {
        money = Mathf.Max(0,startMoney);
        OnMoneyChanged?.Invoke(money);
    }
    public void Add(int amount)
    {
        if (amount < 0) return;

        money += amount;
        OnMoneyChanged?.Invoke(money);
    }

    public bool TrySpend(int amount)
    {
        if (amount <= 0) return true;
        if (money < amount) return false;

        money -= amount;
        OnMoneyChanged?.Invoke(money);
        return true;

    }
}
