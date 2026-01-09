using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private BoardManager boardManager;
    [SerializeField] private PurchaseManager purchaseManager;
    [SerializeField] private DiceManager diceManager;

    private void Start()
    {
        StartNewGame();
        // TrySpawnAnimalOnce(); // 테스트용 제거/주석 권장
    }

    private void OnEnable()
    {
        if (purchaseManager != null)
            purchaseManager.OnPurchaseSucceeded += HandlePurchaseSucceeded;
    }

    private void OnDisable()
    {
        if (purchaseManager != null)
            purchaseManager.OnPurchaseSucceeded -= HandlePurchaseSucceeded;
    }

    public void StartNewGame()
    {
        if (boardManager == null)
        {
            Debug.LogError("GameManager: BoardManager missing");
            return;
        }

        boardManager.ResetBoard();
    }

    private void HandlePurchaseSucceeded()
    {
        if (boardManager == null || diceManager == null)
        {
            Debug.LogError("GameManager: Missing references");
            return;
        }

        // (안전망) 혹시 사이에 보드가 꽉 찼으면 중단
        if (!boardManager.TryGetRandomEmptyIndex(out int idx))
        {
            Debug.Log("공간 없음(구매는 성공했는데 배치 불가). 현재 구조에선 Requestor가 선행 체크하므로 거의 안 일어남.");
            return;
        }

        // 주사위 굴려 AnimalData 얻기
        AnimalData data = diceManager.RollAnimal();
        if (data == null)
        {
            Debug.LogWarning("RollAnimal returned null");
            return;
        }

        // AnimalInstance 생성 + 배치
        AnimalInstance animal = new AnimalInstance(data);

        bool placed = boardManager.TryPlaceAt(idx, animal);
        if (placed)
            Debug.Log($"Placed {data.name} (tier {data.tier}) at slot index {idx}");
        else
            Debug.LogWarning($"배치 실패: idx={idx}");
    }
}
