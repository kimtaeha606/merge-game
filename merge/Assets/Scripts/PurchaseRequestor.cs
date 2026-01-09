using UnityEngine;

/// <summary>
/// UI 버튼 클릭 → (보드 빈칸 선행 체크) → 결제 요청
/// 상품이 1개뿐인 MVP 전용 Requestor.
/// </summary>
public class PurchaseRequestor : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private PurchaseManager purchaseManager;
    [SerializeField] private DiceManager diceManager;
    [SerializeField] private BoardManager boardManager;

    // UI Button OnClick()에 연결
    public void OnClickPurchase()
    {
        if (purchaseManager == null || diceManager == null || boardManager == null)
        {
            Debug.LogError("PurchaseRequestor: Missing references.");
            return;
        }

        // 1) 보드 빈칸 선행 체크 (공간 없으면 결제 자체를 막음)
        if (!boardManager.TryGetRandomEmptyIndex(out _))
        {
            Debug.Log("공간 없음");
            return;
        }

        // 2) 비용 산출 후 결제 요청
        int cost = diceManager.GetRollCost();
        if (cost <= 0)
        {
            Debug.LogWarning($"PurchaseRequestor: invalid cost={cost}");
            return;
        }

        purchaseManager.RequestPurchase(cost);
    }
}