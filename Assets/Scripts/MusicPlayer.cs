using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {

    private AudioSource audioSource;
    [SerializeField] AudioClip[] clips;
    
    [Range(0, 1)][SerializeField] float bgmVolume = 0.80f;

    // Start is called before the first frame update
    void Awake() {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = bgmVolume;
        SetUpSingleton();
    }

    // Update is called once per frame
    void Update() {
        if(!audioSource.isPlaying) {
            audioSource.clip = GetRandomClip();
            audioSource.Play();
        }
    }

    private AudioClip GetRandomClip() {
        int clipIndex = Random.Range(0, clips.Length);
        var clip = clips[clipIndex];
        return clip;
    }

    private void SetUpSingleton() {
        var count = FindObjectsOfType<AudioSource>();
        if (count.Length > 1) {
            Destroy(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void changeVolume(float volume) {
        audioSource.volume = volume;
    }

    public void ToggleMusic() {
        audioSource.mute = !audioSource.mute;
    }
}
