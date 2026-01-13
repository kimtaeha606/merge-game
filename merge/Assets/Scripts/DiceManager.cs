using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour
{
    [Header("Roll Cost")]
    [SerializeField] private int rollCost = 10;
    [SerializeField] private float rollCostGrowthPerLevel = 1.10f; // 레벨당 굴리기 비용 증가율
    public int GetRollCost() => rollCost;

    [Header("Dice Level")]
    [SerializeField] private int level = 1;
    [SerializeField] private int maxLevel = 20;
    public int Level => level;

    [Header("Upgrade Cost")]
    [SerializeField] private int upgradeBaseCost = 50;       // 1->2 업글 비용
    [SerializeField] private float upgradeCostGrowth = 1.35f; // 레벨당 업글 비용 증가율

    public int GetUpgradeCost()
    {
        // level=1일 때 baseCost, level=2일 때 baseCost*growth ...
        return Mathf.RoundToInt(upgradeBaseCost * Mathf.Pow(upgradeCostGrowth, level - 1));
    }

    [Header("Animal Pool (All Tiers)")]
    [SerializeField] private List<AnimalData> animalPool;

    [Header("Probability Settings")]
    [SerializeField] private float baseWeight = 100f;
    [SerializeField, Range(0.01f, 0.99f)] private float tierDecay = 0.5f;

    [Header("Upgrade -> Probability Tuning")]
    [SerializeField] private float tierDecayIncreasePerLevel = 0.03f; // 업글 1회당 tierDecay 증가량
    [SerializeField, Range(0.01f, 0.99f)] private float maxTierDecay = 0.95f; // 너무 쉬워지는 것 방지

    /// <summary>
    /// 업그레이드 적용(결제는 UpgradeManager가 먼저 승인한 뒤 호출)
    /// </summary>
    public bool ApplyUpgrade()
    {
        if (level >= maxLevel) return false;

        level++;

        // 1) 굴리기 비용 성장 (선택)
        rollCost = Mathf.Max(1, Mathf.RoundToInt(rollCost * rollCostGrowthPerLevel));

        // 2) 고티어 확률 상향: tierDecay를 1에 가깝게(감쇠 약하게)
        tierDecay = Mathf.Min(maxTierDecay, tierDecay + tierDecayIncreasePerLevel);

        return true;
    }

    public AnimalData RollAnimal()
    {
        if (animalPool == null || animalPool.Count == 0)
        {
            Debug.LogWarning("Animal pool is empty");
            return null;
        }

        float totalWeight = 0f;
        foreach (var animal in animalPool)
            totalWeight += GetWeight(animal);

        if (totalWeight <= 0f)
        {
            Debug.LogWarning("Total weight is 0. Check weights/settings.");
            return animalPool[0];
        }

        float roll = Random.Range(0f, totalWeight);
        float acc = 0f;

        foreach (var animal in animalPool)
        {
            acc += GetWeight(animal);
            if (roll <= acc)
                return animal;
        }

        return animalPool[0];
    }

    private float GetWeight(AnimalData animal)
    {
        if (animal == null) return 0f;

        // tier가 1 미만이면 방어
        int t = Mathf.Max(1, animal.tier);

        // tier가 높을수록 tierDecay^(t-1) 만큼 감쇠
        // 업그레이드로 tierDecay가 커지면(0.5 -> 0.6 -> 0.7 ...) 고티어 감쇠가 약해져 확률이 올라감
        return baseWeight * Mathf.Pow(tierDecay, t - 1);
    }
}
