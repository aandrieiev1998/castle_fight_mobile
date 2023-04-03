using System;
using UnityEngine;

namespace Systems
{
    [Serializable]
    public class MovementSystem
    {
        [SerializeField] private float _movementSpeed;

        public float MovementSpeed
        {
            get => _movementSpeed;
            set => _movementSpeed = value;
        }
    }
}