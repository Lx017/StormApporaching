using UnityEngine;

namespace GameDesign
{
    [CreateAssetMenu(fileName = "Player", menuName = "GameDesign/Characters/Player")]
    public class PlayerSO : ScriptableObject
    {
        [field: SerializeField] public PlayerGroundedData GroundedData { get; private set; }
        [field: SerializeField] public PlayerCombatData CombatData { get; private set; }
    }
}