using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private BoardManager boardManager;

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
        if (!boardManager.TryGetRandomEmptyIndex(out int idx))
        {
            Debug.Log("보드 가득 참");
            return;
        }

        //AnimalInstance animal = new AnimalInstance(/*...*/);

        //bool placed = boardManager.TryPlaceAt(idx, animal);
        
        //if (placed)
        //{
            //Debug.Log($"Animal placed at slot index {idx}");
        //}

        //if (!placed)
          //  Debug.LogWarning($"배치 실패: idx={idx}");
    }
}