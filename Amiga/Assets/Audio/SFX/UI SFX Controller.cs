using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class UISFXController : MonoBehaviour
{
    public AudioClip pause;
    public AudioClip resume;
    public AudioClip moveSlot;
    public AudioClip removeSlot;
    public AudioClip missSlot;
    public AudioClip drop;
    public AudioClip attach;
    public AudioClip detach;
    public AudioClip switchMenus;
    public AudioClip slider;
    public AudioClip quit;

    private AudioSource src;

    [SerializeField] private AudioMixerGroup uiGroup;
    [SerializeField] private AudioMixerGroup quitGroup;
    [SerializeField] private AudioMixerGroup dropGroup;

    private void Start()
    {
        src = GetComponent<AudioSource>();
    }

    // Helper function to play any given clip
    private void PlaySound(AudioClip clip)
    {
        if (clip != null) 
        {
            src.clip = clip;
            src.Play();
        }
    }

    public void PlayPause()
    {
        src.outputAudioMixerGroup = uiGroup;
        PlaySound(pause);
    }

    public void PlayResume()
    {
        src.outputAudioMixerGroup = uiGroup;
        PlaySound(resume);
    }

    public void PlayMoveSlot()
    {
        src.outputAudioMixerGroup = uiGroup;
        PlaySound(moveSlot);
    }

    public void PlayRemoveSlot()
    {
        src.outputAudioMixerGroup = uiGroup;
        PlaySound(removeSlot);
    }

    public void PlayMissSlot()
    {
        src.outputAudioMixerGroup = uiGroup;
        PlaySound(missSlot);
    }

    public void PlayDrop()
    {
        src.outputAudioMixerGroup = dropGroup;
        PlaySound(drop);
    }

    public void PlayAttach()
    {
        src.outputAudioMixerGroup = uiGroup;
        PlaySound(attach);
    }

    public void PlayDetach()
    {
        src.outputAudioMixerGroup = uiGroup;
        PlaySound(detach);
    }

    public void PlaySwitchMenus()
    {
        src.outputAudioMixerGroup = uiGroup;
        PlaySound(switchMenus);
    }

    public void PlaySlider()
    {
        src.outputAudioMixerGroup = uiGroup;
        PlaySound(slider);
    }

    public void PlayQuit()
    {
        src.outputAudioMixerGroup = quitGroup;
        PlaySound(quit);
    }
}
