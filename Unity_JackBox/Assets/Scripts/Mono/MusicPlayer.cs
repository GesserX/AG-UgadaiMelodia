using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;


[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour
{    
    public AudioSource audioSource;
    public AudioImporter importer;
    bool isPaused = false;

    void Start()
    {        
        audioSource = GetComponent<AudioSource>();
        importer = GetComponent<AudioImporter>();
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
        string audioType = Path.GetExtension(path);
        if(audioType == ".wav" || audioType == ".ogg")
        {
            UnityWebRequest URL = UnityWebRequestMultimedia.GetAudioClip(path, AudioType.WAV);
            var async = URL.SendWebRequest();
            while(!URL.isDone)
            {
                yield return null;
            }
            audioSource.clip = DownloadHandlerAudioClip.GetContent(URL);            
        } 
        else if(audioType == ".mp3")
        {
            if (importer.isDone)
                Destroy(audioSource.clip);

            importer.Import(path);

            while (!importer.isDone)
                yield return null;

            audioSource.clip = importer.audioClip;            
        }
        
        
        FindObjectOfType<RoundHandler>().SetGameState(GameStates.trackPlaying);
            while(isPaused)
                yield return null;

        audioSource.Play();
        yield return new WaitForSeconds(time);
        StopTrack();
    }

}
