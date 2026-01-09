using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private BoardManager boardManager;
    [SerializeField] private GameConfig config;
    private void Start()
    {
        StartNewGame();
        TrySpawnAnimalOnce(); // 테스트용: 시작하자마자 1마리 배치
    }

    public void StartNewGame()
    {
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
        {
            Debug.Log("보드 가득 참");
            return;
        }

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
    }
}