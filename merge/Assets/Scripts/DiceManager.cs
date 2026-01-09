using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour
{
    [Header("Roll Cost")]
    [SerializeField] private int rollCost = 10;
    public int GetRollCost() => rollCost;

    [Header("Animal Pool (All Tiers)")]
    [SerializeField] private List<AnimalData> animalPool;

    [Header("Probability Settings")]
    [SerializeField] private float baseWeight = 100f;
    [SerializeField] private float tierDecay = 0.5f;

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
        return baseWeight * Mathf.Pow(tierDecay, t - 1);
    }
}
