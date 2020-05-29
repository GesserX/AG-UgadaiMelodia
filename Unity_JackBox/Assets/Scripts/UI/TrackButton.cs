using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class TrackButton : MonoBehaviour
{
    [SerializeField]
    int trackNumber;
    [SerializeField]
    Text trackPoints;
    string trackPath;
    Button button;

    [SerializeField]
    private Sprite trackUnchecked; 
    [SerializeField]
    private Sprite trackChecked;
    [SerializeField]
    private Sprite trackDisabled;

    [SerializeField]
    private bool wasPlayed = false;
    // Start is called before the first frame update
    void Start()
    {
        //trackPoints = GetComponentInChildren<Text>();        
        trackNumber = transform.GetSiblingIndex();
        button = GetComponent<Button>();
        button.onClick.AddListener(ButtonClicked);
    }
    void Update()
    {
        GameStates gameState = FindObjectOfType<RoundHandler>().GetGameState();
        if(gameState == GameStates.trackPicking)
        button.enabled = true;
        else
        button.enabled = false;   

        if(wasPlayed)
        {
            button.interactable = false;
            GetComponent<Image>().sprite = trackDisabled;
        }

     
    }
    void ButtonClicked()
    {
        FindObjectOfType<RoundHandler>().SetActiveTrack(trackNumber);
        FindObjectOfType<RoundHandler>().SetGameState(GameStates.trackPlaying);
        //Play track
        FindObjectOfType<MusicPlayer>().PlayTrack(trackPath, 30.0f);
    }


    public void SetSprite(bool isChecked)
    {                       
        if(isChecked)
            GetComponent<Image>().sprite = trackChecked;
            else
            GetComponent<Image>().sprite = trackUnchecked;
    }

    public bool GetWasPlayed()
    {
        return wasPlayed;
    }
    public void SetWasPlayed()
    {
        wasPlayed = true;
    }
    public void SetTrack(string path)
    {
        trackPath = path;
    }
    public void SetTrackPoints(int points)
    {
        trackPoints.text = points.ToString();
    }

}
