using UnityEngine;

namespace Player
{
    public class PlayerAudio : MonoBehaviour
    {
        [SerializeField] private AudioClip dragStartClip;
        [SerializeField] private AudioClip draggingClip;
        [SerializeField] private AudioClip dragReleaseClip;
        [SerializeField] private AudioSource _audioSource;


        public void PlayDragStartSound()
        {
            PlaySound(dragStartClip);
        }

        public void PlayDraggingSound()
        {
            PlaySound(draggingClip, true); 
        }

        public void StopDraggingSound()
        {
            _audioSource.Stop();
        }

        public void PlayDragReleaseSound()
        {
            PlaySound(dragReleaseClip);
        }
        
        private void PlaySound(AudioClip clip, bool loop = false)
        {
            _audioSource.clip = clip;
            _audioSource.loop = loop;
            _audioSource.Play();
        }
    }
}