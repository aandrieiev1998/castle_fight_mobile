using Match;
using Mechanics;
using UnityEngine;

namespace Mobs
{
    public class MobData : MonoBehaviour
    {
        public int _currentHp;
        public int _currentDamage;
        public int _currentArmor;
        public PlayerTeam _currentTeam;
        public ArmorType _currentArmorType;
        public Transform _target;
    }
}