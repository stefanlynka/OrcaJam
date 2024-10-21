using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwooshAudio : MonoBehaviour
{

    public AudioSource AudioSwoosh;
    public static SwooshAudio Instance;

    void Awake() {
        Instance = this;
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void PlaySwoosh() {
        AudioSwoosh.Play();
    }

}
