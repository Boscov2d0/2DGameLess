using UnityEngine;

namespace BattleScripts
{
    [CreateAssetMenu(fileName = nameof(ValueDataManager), menuName = "Configs/" + nameof(ValueDataManager))]
    internal class ValueDataManager : ScriptableObject
    {
        [field: SerializeField] public int CrimeRate { get; private set; }
        [field: SerializeField] public int ConditionRoDecreaseCrimeRate { get; private set; }
    }
}