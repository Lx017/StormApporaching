using UnityEngine;
using System.Collections.Generic;

public class SoundEffectPlayer : MonoBehaviour
{
    public static SoundEffectPlayer shared;

    public AudioClip[] soundEffects; // Assign clips in Inspector
    public AudioClip loopAudio1;      // Public loop clip, assign in Inspector
    public AudioClip loopAudio2;      // Public loop clip, assign in Inspector

    private Dictionary<string, AudioClip> soundDict;
    private AudioSource audioSource;
    private AudioSource loopSource; // Separate source for looping

    void Awake()
    {
        if (shared == null)
        {
            shared = this;
            DontDestroyOnLoad(gameObject); // Optional: persist between scenes
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = gameObject.AddComponent<AudioSource>();
        loopSource = gameObject.AddComponent<AudioSource>();
        loopSource.loop = true;

        // Initialize and fill the dictionary
        soundDict = new Dictionary<string, AudioClip>();
        foreach (AudioClip clip in soundEffects)
        {
            if (clip != null)
                soundDict[clip.name] = clip;
        }
    }

    void Start()
    {
        if (loopAudio1 != null)
        {
            LoopSoundEffect(loopAudio1);
        }
    }
    public void PlaySoundEffect(string filename, float distance = 1f)
    {
        float volume = Mathf.Clamp01(3f/distance); // Adjust volume based on distance
        Debug.Log("Playing sound: " + filename + " at volume: " + volume);
        if (soundDict.ContainsKey(filename))
        {
            audioSource.PlayOneShot(soundDict[filename], Mathf.Clamp01(volume));
        }
        else
        {
            Debug.LogWarning("Sound file not found: " + filename);
        }
    }

    static public void play(string fileName, float distance = 1f)// a simpler interface for other classes to call
    {
        if (shared != null)
        {
            shared.PlaySoundEffect(fileName, distance);
        }
        else
        {
            Debug.LogWarning("SoundEffectPlayer not initialized.");
        }
    }


    public void LoopSoundEffect(AudioClip clip)
    {
        if (clip != null)
        {
            loopSource.clip = clip;
            loopSource.Play();
        }
    }


    public void StopLoop()
    {
        loopSource.Stop();
        loopSource.clip = null;
    }

    public void SwitchLoopTo2()
    {
        StopLoop();
        if (loopAudio2 != null)
        {
            LoopSoundEffect(loopAudio2);
        }
    }

    void OnDestroy()
    {
        StopLoop();
    }
}
