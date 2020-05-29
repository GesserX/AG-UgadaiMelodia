using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


[RequireComponent(typeof(AudioSource))]
public class SfxPlayer : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip pushClip;
    public AudioClip trueClip;
    public AudioClip falseClip;
    public AudioClip categoryChooseClip;
    void Start()
    {        
        audioSource = GetComponent<AudioSource>();
    }


    public void PlaySound(string effectName)
    {
        switch(effectName)
        {
            case "push": audioSource.PlayOneShot(pushClip); break;
            case "true": audioSource.PlayOneShot(trueClip); break;
            case "false": audioSource.PlayOneShot(falseClip); break;
            case "categoryChoose": audioSource.PlayOneShot(categoryChooseClip); break;
            default: break;
        }
    }

}
