// AnimalInstance.cs
using System;
using UnityEngine;

[Serializable]
public sealed class AnimalInstance
{
    [SerializeField] private AnimalData data;

    public AnimalData Data => data;
    public string Id => data != null ? data.id : "(null)";
    public int Tier => data != null ? data.tier : 0;
    public int IncomePerTick => data != null ? data.incomePerTick : 0;
    public float TickInterval => data != null ? data.tickInterval : 0f;

    public AnimalInstance(AnimalData data)
    {
        this.data = data;
    }
}
