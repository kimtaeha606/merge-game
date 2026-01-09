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
    Debug.Log($"[PM] enter cost={cost}");

    Debug.Log("[PM] before money null check");
    if (money == null) { Debug.LogError("[PM] money missing"); OnPurchaseFailed?.Invoke(); return; }

    Debug.Log("[PM] before TrySpend");
    bool ok = money.TrySpend(cost);
    Debug.Log($"[PM] after TrySpend ok={ok}");

    if (!ok) { Debug.Log("[PM] failed spend"); OnPurchaseFailed?.Invoke(); return; }

    Debug.Log("[PM] before success invoke");
    OnPurchaseSucceeded?.Invoke();
    Debug.Log("[PM] after success invoke");
}

}
