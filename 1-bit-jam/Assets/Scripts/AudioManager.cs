using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource playerMovementSource;
    public AudioSource ambientSource;
    public AudioSource musicSource;

    public AudioClip[] playerMovementClips;
    public AudioClip ambientClip;
    public AudioClip musicClip;
    public AudioClip deathClip;
    public AudioClip cantMoveClip;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (ambientClip != null)
        {
            ambientSource.clip = ambientClip;
            ambientSource.loop = true;
            ambientSource.volume = 0.3f;
            ambientSource.Play();
        }

        if (musicClip != null)
        {
            musicSource.clip = musicClip;
            musicSource.loop = true;
            musicSource.volume = 0.3f;
            musicSource.Play();
        }
    }

    public void PlayPlayerMovementSound()
    {
        if (playerMovementClips.Length > 0)
        {
            int index = Random.Range(0, playerMovementClips.Length);
            playerMovementSource.clip = playerMovementClips[index];
            playerMovementSource.Play();
        }
    }
     public void PlayDeathSound(){
        if (deathClip != null){
            playerMovementSource.clip = deathClip;
            playerMovementSource.Play();
        }
     }
    public void PlayCantMoveSound(){
        if (cantMoveClip != null){
            playerMovementSource.clip = cantMoveClip;
            playerMovementSource.Play();
        }
    }
    public void StopBackgroundSounds(){
        ambientSource.Stop();
        playerMovementSource.Stop();
        musicSource.Stop();
    }
    public void SetAmbientVolume(float volume)
    {
        ambientSource.volume = volume;
    }
}