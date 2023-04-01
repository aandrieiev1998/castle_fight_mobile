namespace Assets.MobileOptimizedWater.Scripts
{
    using UnityEngine;

    public class AnimationStarter : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Motion _animation;

        public void Awake()
        {
            _animator.Play(_animation.name);
        }
    }
}
