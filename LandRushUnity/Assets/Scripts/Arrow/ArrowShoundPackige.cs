using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityScriptHelper;

// ReSharper disable once CheckNamespace
public class ArrowShoundPackige : MonoBehaviourEx
{
    public List<AudioClip> AudioClipList;
    private AudioSource _shound;
    public float VolMin;
    public float VolMax;

    public float PitchMin;

    public float PitchMax;
    // Use this for initialization
    protected override void Start()
    {
        _shound = GetCachedComponent<AudioSource>();
    }

//-------------------------------------------------
    public void NockShoundPlay()
    {
        ShoundPlay(_shound, 0);
    }

    public void PullShoundPlay()
    {
        ShoundPlay(_shound, 3);
    }

    public void ShotShoundPlay()
    {
        ShoundPlay(_shound, 6);
    }
    
    private void ShoundPlay(AudioSource shound, int start)
    {
        if (shound != null && shound.isActiveAndEnabled)
        {
            if (_shound.isPlaying)
                return;
            //randomly apply a volume between the volume min max
            shound.volume = Random.Range(VolMin, VolMax);

            //randomly apply a pitch between the pitch min max
            shound.pitch = Random.Range(PitchMin, PitchMax);

            // play the sound
            shound.PlayOneShot(AudioClipList[Random.Range(start, start + 3)]);
        }
    }
}