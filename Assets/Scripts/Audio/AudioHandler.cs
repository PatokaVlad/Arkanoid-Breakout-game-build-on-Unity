using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    private AudioSource _audioSource;

    [SerializeField]
    private AudioClip bounceSound = null;
    [SerializeField]
    private AudioClip breakSound = null;
    [SerializeField]
    private AudioClip dieSound = null;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayBounceClip() => _audioSource.PlayOneShot(bounceSound);

    public void PlayBreakClip() => _audioSource.PlayOneShot(breakSound);

    public void PlayDieClip() => _audioSource.PlayOneShot(dieSound);


}
