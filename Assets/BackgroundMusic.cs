using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{

    public AudioSource BackgroundAudio;
    public float AudioStartTime = 0;


    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        StartBackgroundAudio();

    }

    // Update is called once per frame
    void Update()
    {
        if (BackgroundAudio != null && BackgroundAudio.volume < 1)
        {
            BackgroundAudio.volume += 0.0025f;
        }
    }

    private void StartBackgroundAudio()
    {
        if (BackgroundAudio != null && BackgroundAudio.clip != null)
        {
            // Set the start time in seconds
            BackgroundAudio.time = AudioStartTime;

            BackgroundAudio.volume = 0;

            // Play the audio from the specified time
            BackgroundAudio.Play();
        }
    }

}
