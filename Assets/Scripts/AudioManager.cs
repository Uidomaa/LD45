/*
Music from https://filmmusic.io
"Wholesome" by Kevin MacLeod (https://incompetech.com)
"Sneaky Adventure" by Kevin MacLeod (https://incompetech.com)
License: CC BY (http://creativecommons.org/licenses/by/4.0/)
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource[] audioSources;
    [Header("AUDIO CLIPS")]
    public AudioClip castSpellClip;
    public AudioClip spellHitClip;
    public AudioClip spellMissClip;
    public AudioClip playerHitClip;
    public AudioClip playerDeadClip;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    private void PlayClip (AudioClip clipToPlay, float vol = 1f, float pitch = 1f)
    {
        //Get free audiosource
        int asIndex = 0;
        for (int i = 0; i < audioSources.Length; i++)
        {
            if (!audioSources[i].isPlaying)
            {
                asIndex = i;
                break;
            }
        }
        //Play clip
        audioSources[asIndex].clip = clipToPlay;
        audioSources[asIndex].volume = vol;
        audioSources[asIndex].pitch = pitch;
        audioSources[asIndex].Play();
    }

    #region PUBLIC METHODS
    public void PlayCastSpell()
    {
        PlayClip(castSpellClip);
    }

    public void PlaySpellMiss()
    {
        PlayClip(spellMissClip);
    }

    public void PlaySpellHit()
    {
        PlayClip(spellHitClip);
    }

    public void PlayGhostHitPlayer()
    {
        PlayClip(playerHitClip);
    }

    public void PlayPlayerKilled()
    {
        PlayClip(playerDeadClip);
    }
    #endregion
}
