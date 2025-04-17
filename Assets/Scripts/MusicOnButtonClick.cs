using System.Collections;
using UnityEngine;

public class MusicOnButtonClick : MonoBehaviour
{
    public static MusicOnButtonClick Instance { get; private set; }

    [SerializeField] private AudioClip audioClip;

    private AudioSource musicSource;
    private AudioClip tempClip;

    private bool isButtonSoundPlaying;
    private void Start()
    {
        isButtonSoundPlaying = false;
        musicSource = this.GetComponent<AudioSource>();  

        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }
    public void ONButtonDown()
    {
        StartCoroutine(MusicManaage());
    }

    IEnumerator MusicManaage()
    {
        if(isButtonSoundPlaying)
        {
            yield break;
        }

        isButtonSoundPlaying=true;

        tempClip = musicSource.clip;
        musicSource.clip = audioClip;

        musicSource.loop = false;
        musicSource.Play();

        yield return new WaitUntil(() => !musicSource.isPlaying);
        musicSource.clip = tempClip;
        musicSource.Play();
        musicSource.loop = true;

        isButtonSoundPlaying = false;
    }
}
