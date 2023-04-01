using System.Collections.Generic;
using Scripts3.Mechanics;
using Scripts3.Systems;
using UnityEngine;

namespace Scripts3.Mobs
{
    public abstract class Hero : Mob, IManaSystem, IAbilitiesSystem
    {
        [SerializeField] private float _mana;
        [SerializeField] private float _manaRegen;
        public int Level { get; set; } 
        public int Experience { get; set; }

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

        public List<Ability> Abilities { get; set; }
    }
}