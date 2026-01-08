using UnityEngine;

[CreateAssetMenu(menuName = "Ranch/Game Config", fileName = "GameConfig")]
public class GameConfig : ScriptableObject
{
    [Header("Animals")]
    public AnimalData[] animalsByTier; // index = tier-1 (Tier1은 0번)

    [Header("Merge Rule")]
    public int mergeRequireCount = 2; // 지금은 2로 고정해도 됨(테스트용)
}
