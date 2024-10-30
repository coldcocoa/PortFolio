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
    public AudioClip itemGet;
    public AudioClip boosterGet;
    public AudioClip boosterOn;
    public AudioClip HPGet;
    public AudioClip Sold;
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

    public void ItemGet()
    {
        audioSource.PlayOneShot(itemGet);
    }
    public void BoosterGet()
    {
        audioSource.PlayOneShot(boosterGet);
    }
    public void BoosterOn()
    {
        audioSource.PlayOneShot(boosterOn);
    }
    public void HPGET()
    {
        audioSource.PlayOneShot(HPGet);
    }
    public void SOLD()
    {
        audioSource.PlayOneShot(Sold);
    }
}
