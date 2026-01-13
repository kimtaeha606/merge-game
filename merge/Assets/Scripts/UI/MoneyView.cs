/// 씬에 붙일 때 참고: https://chatgpt.com/c/696088f6-7a04-8320-8cb9-710e2caad528

using TMPro;
using UnityEngine;

public sealed class MoneyView : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private MoneyManager moneyManager;
    [SerializeField] private TMP_Text moneyText;

    [Header("Format")]
    [SerializeField] private string prefix = "$";

    private void Awake()
    {
        // 인스펙터에 연결 안 했으면 자동 탐색(씬에 1개 있다고 가정)
        if (moneyManager == null) moneyManager = FindFirstObjectByType<MoneyManager>();
    }

    private void OnEnable()
    {
        if (moneyManager != null)
            moneyManager.OnMoneyChanged += HandleMoneyChanged;

        // 시작 시 현재 값으로 1회 갱신(ResetMoney가 Start에서 안 불릴 수도 있으니)
        Refresh();
    }

    private void OnDisable()
    {
        if (moneyManager != null)
            moneyManager.OnMoneyChanged -= HandleMoneyChanged;
    }

    private void HandleMoneyChanged(int newMoney)
    {
        if (moneyText == null) return;
        moneyText.text = $"{prefix}{newMoney}";
    }

    private void Refresh()
    {
        if (moneyManager == null) return;
        HandleMoneyChanged(moneyManager.Money);
    }
}
