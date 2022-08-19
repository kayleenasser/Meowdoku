using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Volume : MonoBehaviour
{
    public AudioMixer mixer;

    public void Set_Vol(float slider_val)
    {
        mixer.SetFloat("MusicVolume", Mathf.Log10 (slider_val) * 20); // Represents slider value as a log base 10 since its in decibals, then mutiplies by factor of 20
        // Now a scale of -80 - 0 on a logarithmic scale
    }
}
