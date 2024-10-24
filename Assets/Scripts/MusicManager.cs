using System.Collections;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    [SerializeField] private AudioSource musicSource;  // èrÛd≥o audio dla muzyki
    private Coroutine currentMusicCoroutine;  // Przechowywanie odniesienia do coroutiny

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Odtwarza wybranπ muzykÍ z moøliwoúciπ fade-in.
    /// </summary>
    public void PlayMusic(AudioClip musicClip, float volume = 1.0f, bool immediate = false)
    {
        if (immediate)
        {
            PlayMusicImmediately(musicClip, volume);  // OdtwÛrz natychmiast
        }
        else
        {
            StartMusicWithFade(musicClip, volume);  // OdtwÛrz z fade-in
        }
    }

    /// <summary>
    /// Natychmiastowa zmiana muzyki (bez fade-in).
    /// </summary>
    private void PlayMusicImmediately(AudioClip musicClip, float volume)
    {
        if (currentMusicCoroutine != null)
        {
            StopCoroutine(currentMusicCoroutine);
        }

        musicSource.clip = musicClip;
        musicSource.volume = volume;
        musicSource.Play();
    }

    /// <summary>
    /// Odtwarzanie muzyki z fade-in.
    /// </summary>
    private void StartMusicWithFade(AudioClip musicClip, float targetVolume, float fadeDuration = 1.0f)
    {
        if (currentMusicCoroutine != null)
        {
            StopCoroutine(currentMusicCoroutine);
        }

        currentMusicCoroutine = StartCoroutine(FadeInMusic(musicClip, targetVolume, fadeDuration));
    }

    /// <summary>
    /// Coroutine dla fade-in muzyki.
    /// </summary>
    private IEnumerator FadeInMusic(AudioClip newClip, float targetVolume, float fadeDuration)
    {
        if (musicSource.isPlaying)
        {
            yield return StartCoroutine(FadeOutMusic(fadeDuration));
        }

        musicSource.clip = newClip;
        musicSource.Play();

        float currentVolume = 0.0f;
        musicSource.volume = currentVolume;

        while (currentVolume < targetVolume)
        {
            currentVolume += Time.deltaTime / fadeDuration;
            musicSource.volume = Mathf.Clamp(currentVolume, 0, targetVolume);
            yield return null;
        }
    }

    /// <summary>
    /// Coroutine dla fade-out obecnie odtwarzanej muzyki.
    /// </summary>
    private IEnumerator FadeOutMusic(float fadeDuration)
    {
        float startVolume = musicSource.volume;

        while (musicSource.volume > 0)
        {
            musicSource.volume -= startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        musicSource.Stop();
        musicSource.clip = null;
    }

    /// <summary>
    /// Wstrzymuje bieøπcπ muzykÍ z fade-out.
    /// </summary>
    public void StopMusic(float fadeDuration = 1.0f)
    {
        if (currentMusicCoroutine != null)
        {
            StopCoroutine(currentMusicCoroutine);
        }

        StartCoroutine(FadeOutMusic(fadeDuration));
    }
}
