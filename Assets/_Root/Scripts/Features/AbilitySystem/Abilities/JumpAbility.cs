using System;
using UnityEngine;
using JetBrains.Annotations;
using Object = UnityEngine.Object;

namespace Features.AbilitySystem.Abilities
{
    internal class JumpAbility : IAbility
    {
        private readonly AbilityItemConfig _config;

        public JumpAbility([NotNull] AbilityItemConfig config) =>
            _config = config ?? throw new ArgumentNullException(nameof(config));


        public void Apply(IAbilityActivator activator)
        {
            var projectile = _config.Projectile.GetComponent<Rigidbody2D>();
            Vector3 force = activator.ViewGameObject.transform.up * _config.Value;
            projectile.AddForce(force, ForceMode2D.Impulse);
        }
    }
}