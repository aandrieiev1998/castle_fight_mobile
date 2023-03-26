using Buildings.Types;
using Match;
using Mechanics;
using UnityEngine;

namespace Buildings.Data
{
    public class BaseBuildingData : MonoBehaviour
    {
        public int _currentHp;
        public int _currentArmor;
        public ArmorType _armorType;
        public BaseBuildingType _buildingType;
        public PlayerTeam _playerTeam;
    }
}