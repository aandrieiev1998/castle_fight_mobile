using Systems;
using UnityEngine;

namespace Entities.Mobs
{
    public abstract class Hero : Mob
    {
        [SerializeField] private ManaSystem _manaSystem;
        [SerializeField] private AbilitiesSystem _abilitiesSystem;
    }
}