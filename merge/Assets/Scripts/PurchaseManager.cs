// PurchaseManager.cs
using System;
using UnityEngine;

/// <summary>
/// 결제(돈 차감)만 책임지고, 성공/실패를 이벤트로 알려준다.
/// 상품이 1개뿐이므로 Request 객체/타입 구분 없이 cost만 받는다.
/// </summary>
public class PurchaseManager : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField]  MoneyManager money;

    public event Action OnPurchaseSucceeded;
    public event Action OnPurchaseFailed; // (선택) 돈 부족/기타 사유 구분이 필요하면 enum 추가

    public void RequestPurchase(int cost)
    {
        if (cost <= 0)
        {
            Debug.LogWarning($"PurchaseManager: invalid cost={cost}");
            OnPurchaseFailed?.Invoke();
            return;
        }

        if (money == null)
        {
            Debug.LogError("PurchaseManager: EconomyManager reference is missing.");
            OnPurchaseFailed?.Invoke();
            return;
        }

        if (!money.TrySpend(cost))
        {
            OnPurchaseFailed?.Invoke();
            return;
        }

        OnPurchaseSucceeded?.Invoke();
    }
}
