using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {

    private AudioSource musicPlayer;
    
    [Range(0, 1)][SerializeField] float bgmVolume = 0.80f;

    // Start is called before the first frame update
    void Awake() {
        musicPlayer = GetComponent<AudioSource>();
        SetUpSingleton();
    }

    // Update is called once per frame
    void Update() {
        
    }

    private void SetUpSingleton() {
        musicPlayer.volume = bgmVolume;
        var count = FindObjectsOfType<AudioSource>();
        if (count.Length > 1) {
            Destroy(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
        }
    }
}
