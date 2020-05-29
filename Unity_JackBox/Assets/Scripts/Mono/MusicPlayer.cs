using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour
{
    AudioSource audioSource;
    bool isPaused = false;
    void Start()
    {        
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(FindObjectOfType<RoundHandler>().GetGameState() != GameStates.trackPlaying && FindObjectOfType<RoundHandler>().GetGameState() != GameStates.answerProcessing)
        {
            StopTrack();
        }

    }

    public void PlayTrack(string trackPath, float time)
    { 
        StartCoroutine(PlayAudio( "file:///" + trackPath, time));
    }

    public void PauseTrack()
    {
        isPaused = true;
        audioSource.Pause();
    }

    public void ResumeTrack()
    {
        audioSource.Play();
        isPaused = false;
    }    

    public void StopTrack()
    {
        isPaused = false;
        audioSource.Stop();
        StopAllCoroutines();
    }

    IEnumerator PlayAudio(string path, float time)
    {               
        UnityWebRequest URL = UnityWebRequestMultimedia.GetAudioClip(path, AudioType.WAV);
        var async = URL.SendWebRequest();
        while(!URL.isDone)
        {
            yield return null;
        }
        audioSource.clip = DownloadHandlerAudioClip.GetContent(URL);
        FindObjectOfType<RoundHandler>().SetGameState(GameStates.trackPlaying);
        while(isPaused)
        {
            yield return null;
        }
        audioSource.Play();
        yield return new WaitForSeconds(time);
        StopTrack();
    }

}
