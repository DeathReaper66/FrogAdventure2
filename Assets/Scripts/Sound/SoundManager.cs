using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    private AudioSource _source;

    private void Awake()
    {
        _source = GetComponent<AudioSource>();

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != null && Instance != this)
            Destroy(gameObject);
    }

    public void PlaySound(AudioClip audioClip)
    {
        _source.PlayOneShot(audioClip);
    }
}
