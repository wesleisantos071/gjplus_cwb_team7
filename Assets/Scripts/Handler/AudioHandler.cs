using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioHandler : MonoBehaviour {
    public static AudioHandler instance;
    public Sound[] sounds;
    public Sound[] musics;

    // Use this for initialization
    void Awake() {
        if (instance == null) {
            instance = this;
        }
        DontDestroyOnLoad(this);
        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
        foreach (Sound s in musics) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.loop = true;
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
        PlayMusic(musics[0].name);
    }

    private void Start() {
        instance.PlayBackground("FireBackground");
        instance.PlayMusic("MainMusic");
        ConfigureWaterJet();
    }

    public void Play(string soundName) {
        //if (DataHandler.instance.soundEnabled) {
        Sound s = Array.Find(sounds, sound => sound.name == soundName);
        if (s == null) {
            Debug.LogError("Sound not found for name'" + soundName + "'");
            return;
        }
        s.source.Play();
        //}
    }

    AudioSource currentMusic;
    AudioSource currentBackground;
    AudioSource waterJet;

    public void PlayMusic(string musicName) {
        DataHandler data = DataHandler.instance;
        Sound s = Array.Find(musics, sound => sound.name == musicName);
        if (s == null) {
            Debug.LogError("Music not found for name'" + musicName + "'");
            return;
        }
        if (instance.currentMusic != null) {
            if (musicName.Equals(instance.currentMusic.name)) { return; }
            if (instance.currentMusic.name.Equals(musicName)) {
                return;
            }
            instance.currentMusic.Stop();
        }
        instance.currentMusic = s.source;
        instance.currentMusic.name = musicName;
        //if (data.musicEnabled) {
        instance.currentMusic.Play();
        //}
    }

    public void PlayBackground(string backgroundSoundName) {
        DataHandler data = DataHandler.instance;
        Sound s = Array.Find(musics, sound => sound.name == backgroundSoundName);
        if (s == null) {
            Debug.LogError("backgroundSoundName not found for name'" + backgroundSoundName + "'");
            return;
        }
        if (instance.currentBackground != null) {
            if (backgroundSoundName.Equals(instance.currentBackground.name)) { return; }
            if (instance.currentBackground.name.Equals(backgroundSoundName)) {
                return;
            }
            instance.currentBackground.Stop();
        }
        instance.currentBackground = s.source;
        instance.currentBackground.name = backgroundSoundName;
        //if (data.musicEnabled) {
        instance.currentBackground.Play();
        //}
    }

    public void ConfigureWaterJet() {
        DataHandler data = DataHandler.instance;
        string soundName = "WaterJet";
        Sound s = Array.Find(musics, sound => sound.name == soundName);
        if (s == null) {
            Debug.LogError("backgroundSoundName not found for name'" + soundName + "'");
            return;
        }
        if (instance.waterJet != null) {
            if (soundName.Equals(instance.waterJet.name)) { return; }
            if (instance.waterJet.name.Equals(soundName)) {
                return;
            }
            instance.waterJet.Stop();
        }
        instance.waterJet = s.source;
        instance.waterJet.name = soundName;
    }

    internal void ResumeMusic() {
        if (instance.currentMusic != null) {
            instance.currentMusic.Play();
        }
    }

    internal void StopMusic() {
        if (instance.currentMusic != null) {
            instance.currentMusic.Stop();
        }
    }
    public void PlayWaterJet() {
        if (instance.waterJet != null) {
            instance.waterJet.Play();
        }
    }

    public void StopWaterJet() {
        if (instance.waterJet != null) {
            instance.waterJet.Stop();
        }
    }
}
