using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [Header("Audio Clips")]
    public AudioClip buttonClickSound;
    public AudioClip jumpSound;
    public AudioClip hitSound;
    public AudioClip buttonCanCelSound;
    public AudioClip MainBgm;
    public AudioClip RobbyBgm;
    private AudioSource audioSource;

    private void Awake()
    {
        // ½Ì±ÛÅæ ÆÐÅÏ ±¸Çö
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
        }

        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void PlayButtonClickSound()
    {
        audioSource.PlayOneShot(buttonClickSound);
    }

    public void PlayJumpSound()
    {
        audioSource.PlayOneShot(jumpSound);
    }

    public void PlayHitSound()
    {
        audioSource.PlayOneShot(hitSound);
    }
    public void PlayButtonCancelSound()
    {
        audioSource.PlayOneShot(buttonCanCelSound);
    }
    public void MainBGM()
    {
        audioSource.PlayOneShot(MainBgm);
    }
    public void RobbyBGM()
    {
        audioSource.PlayOneShot(RobbyBgm);
    }
}
