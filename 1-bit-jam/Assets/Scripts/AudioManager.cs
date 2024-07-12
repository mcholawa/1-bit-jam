using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource playerMovementSource;
    public AudioSource ambientSource;
    public AudioSource musicSource;
    public AudioSource typingSource;

    public AudioClip[] playerMovementClips;
    public AudioClip ambientClip;
    public AudioClip musicClip;
    public AudioClip deathClip;
    public AudioClip cantMoveClip;
    public AudioClip typingClip;

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
        if (typingClip != null){
            typingSource.clip = typingClip;
            typingSource.loop = true;
        }
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
    public void PlayTypingSound(){
        Debug.Log("playing typing");
        typingSource.Play();
    }
    public void StopTypingSound(){
        typingSource.Stop();
    }
    public void PlayPlayerMovementSound()
    {
        playerMovementSource.volume = 0.3f;
        if (playerMovementClips.Length > 0)
        {
            int index = Random.Range(0, playerMovementClips.Length);
            playerMovementSource.clip = playerMovementClips[index];
            playerMovementSource.Play();
        }
    }
     public void PlayDeathSound(){
        if (deathClip != null){
           // Debug.Log("death sound");
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
        //playerMovementSource.Stop();
        musicSource.Stop();
    }
     public void PlayBackgroundSounds(){
        ambientSource.Play();
        //playerMovementSource.Stop();
        musicSource.Play();
    }
    public void SetAmbientVolume(float volume)
    {
        ambientSource.volume = volume;
    }
}