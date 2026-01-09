using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private BoardManager boardManager;
<<<<<<< HEAD
    [SerializeField] private PurchaseManager purchaseManager;
    [SerializeField] private DiceManager diceManager;

=======
    [SerializeField] private GameConfig config;
>>>>>>> aabf87823d45e9aee525c559e3b53fbb118f50e7
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
<<<<<<< HEAD
        if (boardManager == null)
=======
        boardManager.ResetBoard();
    }

    // 예: 버튼 클릭/주사위 사용 시 호출할 함수
    public void TrySpawnAnimalOnce()
    {
        if (config == null || config.animalsByTier == null || config.animalsByTier.Length == 0)
        {
            Debug.LogError("GameConfig 또는 animalsByTier 설정 안됨");
            return;
        }

        if (!boardManager.TryGetRandomEmptyIndex(out int idx))
>>>>>>> aabf87823d45e9aee525c559e3b53fbb118f50e7
        {
            Debug.LogError("GameManager: BoardManager missing");
            return;
        }

<<<<<<< HEAD
        boardManager.ResetBoard();
=======
        AnimalData t1 = config.animalsByTier[0]; // Tier1
        if (t1 == null)
        {
            Debug.LogError("animalsByTier[0] (Tier1)이 비어있음");
            return;
        }

        AnimalInstance animal = new AnimalInstance(t1);

        bool placed = boardManager.TryPlaceAt(idx, animal);

        if (placed)
        {
            Debug.Log($"Spawned {t1.id} (Tier {t1.tier}) income={t1.incomePerTick} interval={t1.tickInterval} at slot {idx}");
        }
        else
        {
            Debug.LogWarning($"배치 실패: idx={idx}");
        }
>>>>>>> aabf87823d45e9aee525c559e3b53fbb118f50e7
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
