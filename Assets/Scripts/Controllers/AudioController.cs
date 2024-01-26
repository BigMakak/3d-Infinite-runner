using System.Collections.Generic;
using UnityEngine;


public class AudioController : MonoBehaviour
{

    //Singleton pattern for easy use
    public static AudioController Instance {get; private set;}

    [SerializeField] private List<Audio> audios = new List<Audio>();

    private AudioSource m_audioSource;

    void Awake()
    {
        m_audioSource = GetComponent<AudioSource>();

        if(m_audioSource == null) 
        {
            Debug.LogError("No audio source found, sending error");
        }

        if(Instance != null && Instance != this) 
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    /// <summary>
    /// Plays an audio in it's associated Audio Source 
    /// </summary>
    /// <param name="_name">Name of the audio</param>
    public void PlayAudio(string _name) 
    {
        Audio _currAudio = audios.Find( (_audio) => _audio.AudioName == _name);

        m_audioSource.clip = _currAudio.clip;
        m_audioSource.Play();
    }
}
