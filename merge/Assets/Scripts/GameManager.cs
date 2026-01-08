using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private BoardManager boardManager;

    private void Start()
    {
        StartNewGame();
    }

    public void StartNewGame()
    {
        boardManager.ResetBoard();
    }
}
