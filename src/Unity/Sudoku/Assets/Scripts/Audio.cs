using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    private AudioSource audio_file;
    private GameObject[] game_music;
    private bool already_playing = false;
    public void PlayMusic()
    {
        if (audio_file.isPlaying) return;
        audio_file.Play();
    }
    public void StopMusic()
    {
        audio_file.Stop();
    }
    private void Awake()
    {
        game_music = GameObject.FindGameObjectsWithTag("BGMUSIC");

        foreach (GameObject other_instance in game_music)
        {
            if (other_instance.scene.buildIndex == -1) // Only 1 current scene playing, therefore one instance of music
            {
                already_playing = true;
            }
        }

        if (already_playing == true)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(transform.gameObject);
        audio_file = GetComponent<AudioSource>();
    }
}
