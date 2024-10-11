using UnityEngine;

namespace Player
{
    public class PlayerSound : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;

        public void Start()
        {

        }

        public void PlayJumpSound()
        {
            audioSource.Play();
        }

        public void PlayDragSound()
        {
            audioSource.Play();
        }

        public void StopAllSounds()
        {
            audioSource.Stop();
        }
    }
}