
using System;
using UnityEngine;

public class AudioEffectPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource clickEmpty;
    [SerializeField] private AudioSource clickObject;
    [SerializeField] private AudioSource clickButtonDenied;
    [SerializeField] private AudioSource clickButtonSuccess;
    [SerializeField] private AudioSource alert;
    
    public static AudioEffectPlayer Instance  { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    
    public void PlayClickSimple()
    {
        if (clickEmpty != null)
            clickEmpty.Play();
    }

    public void PlayClickObject()
    {
        if (clickObject != null)
            clickObject.Play();
    }

    public void PlayButtonDenied()
    {
        if (clickButtonDenied != null)
            clickButtonDenied.Play();
    }

    public void PlayButtonSuccess()
    {
        if (clickButtonSuccess != null)
            clickButtonSuccess.Play();
    }

    public void PlayAlert()
    {
        if (alert != null)
            alert.Play();
    }
}