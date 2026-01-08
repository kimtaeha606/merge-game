// AnimalData.cs
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Animal Data", fileName = "AnimalData_")]
public sealed class AnimalData : ScriptableObject
{
    [Header("Identity")]
    public string id;          // e.g. "Chicken_T1"
    [Range(1, 10)] public int tier = 1;

    [Header("Economy")]
    public int incomePerTick = 1;     // e.g. 1, 3, 10 ...
    public float tickInterval = 3f;   // e.g. 3.0f

    [Header("Progression")]
    public AnimalData nextTierAnimal; // Tier10 => null
}
