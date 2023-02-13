using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[AddComponentMenu("Audio/Multiple Audio Clips")]
[HideScriptField]
public class MultipleAudioClips : MonoBehaviour
{
    public List<AudioClip> clips;
    private AudioSource audioSrc;

    void Start() => audioSrc = GetComponent<AudioSource>();

    public void PlayClip(string name, float volumeScale = 1.0f)
    {
        foreach (AudioClip clip in clips)
        {
            if (clip.name == name) audioSrc.PlayOneShot(clip, volumeScale);
            return;
        }

        throw new KeyNotFoundException($"Clip with name {name} does not exist. Consider checking your entry for typos, or add the required clip.");
    }

    public void PlayClip(int index, float volumeScale = 1.0f)
    {
        AudioClip clip = clips[index];
        if (clip != null)
        {
            audioSrc.PlayOneShot(clip, volumeScale);
            return;
        }

        throw new KeyNotFoundException($"Clip with index {index} does not exist. Consider checking your entry for typos, or add the required clip.");
    }

    public void PlayClip(AudioClip clip, float volumeScale = 1.0f)
    {
        if (clip == null) throw new ArgumentNullException("clip", $"Passed clip is null.");
        audioSrc.PlayOneShot(clip, volumeScale);
    }
}
