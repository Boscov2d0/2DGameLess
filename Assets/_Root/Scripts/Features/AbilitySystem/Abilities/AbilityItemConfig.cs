using UnityEngine;
using Features.Inventory.Items;

namespace Features.AbilitySystem.Abilities
{
    internal interface IAbilityItem
    {
        string Id { get; }
        Sprite Icon { get; }
        AbilityType Type { get; }
        GameObject Projectile { get; }
        float Value { get; }
    }

    [CreateAssetMenu(fileName = nameof(AbilityItemConfig), menuName = "Configs/" + nameof(AbilityItemConfig))]
    internal class AbilityItemConfig : ScriptableObject, IAbilityItem
    {
        [field: SerializeField] public ItemConfig ItemConfig;
        [field: SerializeField] public AbilityType Type { get; private set; }
        [field: SerializeField] public GameObject Projectile { get; private set; }
        [field: SerializeField] public float Value { get; private set; }

        public string Id => ItemConfig.Id;
        public Sprite Icon => ItemConfig.Icon;
    }
}
