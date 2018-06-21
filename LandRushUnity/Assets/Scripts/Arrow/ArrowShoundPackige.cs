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

    private int _start;

    // Use this for initialization
    protected override void Start()
    {
        _shound = GetCachedComponent<AudioSource>();
    }

//-------------------------------------------------
    public void NockShoundPlay()
    {
        _start = 0;
        float speed = 0;
        if (_shound.clip != null)
            speed = _shound.clip.length;
        Invoke("ShoundPlay", speed);
    }

    public void PullShoundPlay()
    {
        _start = 3;
        float speed = 0;
        if (_shound.clip != null)
            speed = _shound.clip.length;
        Invoke("ShoundPlay", speed);
    }

    public void ShotShoundPlay()
    {
        _start = 6;
        float speed = 0;
        if (_shound.clip != null)
            speed = _shound.clip.length;
        Invoke("ShoundPlay", speed);
    }

    private void ShoundPlay()
    {
        if (_shound != null && _shound.isActiveAndEnabled)
        {
            //randomly apply a volume between the volume min max
            _shound.volume = Random.Range(VolMin, VolMax);

            //randomly apply a pitch between the pitch min max
            _shound.pitch = Random.Range(PitchMin, PitchMax);

            // play the sound
            _shound.PlayOneShot(AudioClipList[Random.Range(_start, _start + 3)]);
        }
    }
}