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

    private void OnEnable()  => Debug.Log("PurchaseRequestor OnEnable");
    private void Start()     => Debug.Log("PurchaseRequestor Start");




    // UI Button OnClick()에 연결
    public void OnClickPurchase()
    {
        Debug.Log("OnClickPurchase CALLED - A");

        Debug.Log($"Refs: pm={(purchaseManager!=null)} dm={(diceManager!=null)} bm={(boardManager!=null)}");

        if (purchaseManager == null || diceManager == null || boardManager == null)
        {
            Debug.LogError("PurchaseRequestor: Missing references.");
            return;
        }

        Debug.Log("OnClickPurchase - B (before empty check)");

        if (!boardManager.TryGetRandomEmptyIndex(out _))
        {
            Debug.Log("공간 없음");
            return;
        }

        Debug.Log("OnClickPurchase - C (after empty check)");

        int cost = diceManager.GetRollCost();
        Debug.Log($"OnClickPurchase - D cost={cost}");

        if (cost <= 0)
        {
            Debug.LogWarning($"PurchaseRequestor: invalid cost={cost}");
            return;
        }

        Debug.Log("OnClickPurchase - E (request purchase)");
        purchaseManager.RequestPurchase(1);
        Debug.Log("OnClickPurchase - F (after request)");
    }
}