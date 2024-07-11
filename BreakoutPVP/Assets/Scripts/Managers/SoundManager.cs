using UnityEngine;
using System;

public enum SoundType { MISC, PLAYER, BGM };

public class SoundManager : MonoBehaviour {
    public static SoundManager Instance;

    [SerializeField] Sound[] misc, players, bgm;
    [SerializeField] AudioSource soundSource;
    

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
    void Start() { 
        Play("Theme", SoundType.BGM); 
    }
    public void Play(string name, SoundType type) {
        Sound s = null;

        switch (type) { // gets sfx/music spceific to the type
            case SoundType.MISC:   s = Array.Find(misc, i => i.name == name);    break;
            case SoundType.PLAYER: s = Array.Find(players, i => i.name == name); break;
            case SoundType.BGM:    s = Array.Find(bgm, i => i.name == name);     break;
            default: break;
        }

        if (s != null) {
            soundSource.volume = s.volume;
            soundSource.loop = s.loop;
            soundSource.clip = s.clip;
            soundSource.PlayOneShot(s.clip);
        }
        else Debug.LogError("Sound not found!");
    }
    public void ToggleMute() { soundSource.mute = !soundSource.mute; }
    public void ToggleVolume(float v) { soundSource.volume = v; }
}

[System.Serializable] // makes it visible in the inspector
public class Sound {
    public AudioClip clip;
    public string name;
    public bool loop;

    [Range(0f, 1f)]
    public float volume = 0.8f;

    [HideInInspector]
    public AudioSource source;
}
