using Buildings.Types;
using Match;
using Mechanics;
using UnityEngine;

namespace Buildings.Data
{
    public class MobBuildingData : MonoBehaviour
    {
        public int _currentHp;
        public int _currentArmor;
        public ArmorType _armorType;
        public MobBuildingType _buildingType;
        public PlayerTeam _playerTeam;
    }
}