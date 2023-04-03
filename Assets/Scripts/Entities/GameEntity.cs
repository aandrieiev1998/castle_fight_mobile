using Systems;
using UnityEngine;

namespace Entities
{
    public abstract class GameEntity : MonoBehaviour
    {
        [SerializeField] private HealthSystem _healthSystem;
        [SerializeField] private TeamSystem _teamSystem;

        public HealthSystem HealthSystem => _healthSystem;

        public TeamSystem TeamSystem => _teamSystem;
    }
}