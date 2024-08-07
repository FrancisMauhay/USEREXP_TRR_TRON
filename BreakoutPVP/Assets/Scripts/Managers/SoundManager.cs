using UnityEngine;
using System;

public class SoundManager : MonoBehaviour {

    public static SoundManager Instance;

    [SerializeField] Sound[] misc, game, bgm;
    public AudioSource soundSource;

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    void Start() {
        Play("battle", 2);
    }

    public void Play(string name, int mode) {
        Sound s = null;

        switch (mode) {
            case 0: s = Array.Find(misc, i => i.name == name); break;
            case 1: s = Array.Find(game, i => i.name == name); break;
            case 2: s = Array.Find(bgm, i => i.name == name); break;
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

[Serializable]
public class Sound {
    public string name;
    public AudioClip clip;
    public bool loop;

    [Range(0f, 1f)]
    public float volume = 0.8f;

    [HideInInspector]
    public AudioSource source;
}
