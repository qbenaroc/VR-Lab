using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSaber : MonoBehaviour
{
    //public AudioClip beamAudio;

    //private AudioSource _audioSource;
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        //_audioSource = GetComponent<AudioSource>();
    }

    public void TriggerBeam()
	{
        bool isOn = _animator.GetBool("LightSaberOn");

        /*
        if (!isOn)
            _audioSource.PlayOneShot(beamAudio);
        else
            _audioSource.Stop();
        */

        _animator.SetBool("LightSaberOn", !isOn);
	}
}