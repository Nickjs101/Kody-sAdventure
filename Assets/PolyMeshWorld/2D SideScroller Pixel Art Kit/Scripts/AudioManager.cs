using UnityEngine;

public class AudioManager : MonoBehaviour {

    public static AudioManager instance;
    public AudioSource[] soundEffects;

    public AudioSource Bgm;

    public void Awake() { 
        instance = this;
    }

    public void PlaySFX(int SoundToPlay) {
        soundEffects[SoundToPlay].Stop();
        soundEffects[SoundToPlay].pitch = Random.Range(.9f, 1.1f);
        soundEffects[SoundToPlay].Play();
    }
}
