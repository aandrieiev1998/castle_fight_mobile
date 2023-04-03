using System.Collections.Generic;
using Entities.Mobs;
using Mechanics;
using Systems;
using UnityEngine;

namespace Mobs
{
    public abstract class Hero : Mob, IManaSystem, IAbilitiesSystem
    {
        [SerializeField] private float _mana;
        [SerializeField] private float _manaRegen;
        public int Level { get; set; }
        public int Experience { get; set; }

        public List<Ability> Abilities { get; set; }

        public float Mana
        {
            get => _mana;
            set => _mana = value;
        }

        public float ManaRegen
        {
            get => _manaRegen;
            set => _manaRegen = value;
        }
    }
}