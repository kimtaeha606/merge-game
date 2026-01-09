using UnityEngine;

public class UpgradeRequestor : MonoBehaviour
{
    [SerializeField] private UpgradeManager upgradeManager;

    // Button OnClick에 연결
    public void RequestDiceUpgrade()
    {
        upgradeManager.TryBuyDiceUpgrade();
    }
}