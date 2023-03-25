using UnityEngine;

namespace Buildings
{
    public class BuildingPlatform : MonoBehaviour
    {

        [SerializeField] private bool _isOccupied;
        
        

        public bool IsOccupied
        {
            get => _isOccupied;
            set => _isOccupied = value;
        }
    }
}