using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityScriptHelper;

// ReSharper disable once CheckNamespace
public class ArrowShoundPackige : MonoBehaviourEx
{
    public List<AudioClip> AudioClipList;
    private AudioSource _nockShound;
    private AudioSource _pullShound;
    private AudioSource _shotShound;
    public float VolMin;
    public float VolMax;

    public float PitchMin;

    public float PitchMax;
    // Use this for initialization

    //-------------------------------------------------
    public void NockShoundPlay()
    {
        ShoundPlay(_nockShound, 0);
    }

    public void PullShoundPlay()
    {
        ShoundPlay(_pullShound, 3);
    }

    public void ShotShoundPlay()
    {
        ShoundPlay(_shotShound, 6);
    }

    private void ShoundPlay(AudioSource shound, int start)
    {
        if (shound != null && shound.isActiveAndEnabled)
        {
            //randomly apply a volume between the volume min max
            shound.volume = Random.Range(VolMin, VolMax);

            //randomly apply a pitch between the pitch min max
            shound.pitch = Random.Range(PitchMin, PitchMax);

            // play the sound
            shound.PlayOneShot(AudioClipList[Random.Range(start, start + 3)]);
        }
    }
}