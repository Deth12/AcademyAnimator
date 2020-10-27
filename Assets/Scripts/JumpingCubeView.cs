using UnityEngine;

namespace Academy.Animations
{
    [RequireComponent(typeof(Animation))]
    public class JumpingCubeView : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem _particleSystem = default;
        
        private Animation _animation;

        private void Awake()
        {
            _animation = GetComponent<Animation>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _animation[_animation.clip.name].speed *= -1;
            }
        }

        private void PlayPuff()
        {
            _particleSystem.Play();
        }
    }
}
