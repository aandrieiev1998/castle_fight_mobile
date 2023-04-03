using System;
using UnityEngine;

namespace Systems.Impl
{
    [Serializable]
    public class MovementSystem : IMovementSystem
    {
        [SerializeField] private float _movementSpeed;

        public float MovementSpeed
        {
            get => _movementSpeed;
            set => _movementSpeed = value;
        }
    }
}