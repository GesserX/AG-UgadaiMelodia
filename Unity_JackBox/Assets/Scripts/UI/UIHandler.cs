using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public float lightSwitchTimer = 0.5f;

    [SerializeField]
    Transform[] categoryButtonsTransform;
    
    [SerializeField]
    Transform TeamsLogoTransform;

    [SerializeField]
    Transform[] trackButtonsTransform;

    [SerializeField]
    GameObject teamLogoPrefab;

    [SerializeField]
    GameObject notificationPrefab;

    GameStates gameState;


    bool categoryLights = false;
    bool trackLights = false;
    bool trackPlayLights = false;


    void Update()
    {
        gameState = FindObjectOfType<RoundHandler>().GetGameState();
        HandleGameState();
    }

    public void AddTeams()
    {
        if(FindObjectOfType<TeamHandler>() && FindObjectOfType<TeamHandler>().teamList.Count > 0)
        {
            foreach (Team team in FindObjectOfType<TeamHandler>().teamList)
            {
                GameObject logo = Instantiate(teamLogoPrefab, TeamsLogoTransform);
                logo.GetComponent<TeamLogo>().team = team;
            }
        }
        else
        {
            Team team = new Team("Null команда");
            FindObjectOfType<TeamHandler>().teamList.Add(team);
            GameObject logo = Instantiate(teamLogoPrefab, TeamsLogoTransform);
            logo.GetComponent<TeamLogo>().team = team;
        }
    }

    void HandleGameState()
    {
        switch (gameState)
        {
            case GameStates.categoryPicking:
            if(!categoryLights)
            {
                StopAllCoroutines();
                StartCoroutine("SwitchCategoryLights");
            }
            break;
            
            case GameStates.trackPicking:
            if(!trackLights)
            {
                StopAllCoroutines();
                StartCoroutine("SwitchTracklLights");
            }
            break;
            
            case GameStates.trackPlaying:
            if(!trackPlayLights)
            {
                StopAllCoroutines();
                SwitchCurrentTrackLight();
            }
            break;
            
            default:
            break;
        }
    }

    IEnumerator SwitchCategoryLights()
    {
        trackLights = false;
        trackPlayLights = false;
        categoryLights = true;
        SwitchAllLights(false);
        do{
            for(int index = 0; index < Constants.CATEGORIES_COUNT; index++)
            {
                categoryButtonsTransform[index].GetComponent<CategoryButton>().SetSprite(true);
                foreach(Transform t in trackButtonsTransform[index])
                {
                    if(!t.gameObject.GetComponent<TrackButton>().GetWasPlayed())
                    t.gameObject.GetComponent<TrackButton>().SetSprite(true);
                }
                
                yield return new WaitForSeconds(lightSwitchTimer);

                categoryButtonsTransform[index].GetComponent<CategoryButton>().SetSprite(false);
                foreach(Transform t in trackButtonsTransform[index])
                {
                    if(!t.gameObject.GetComponent<TrackButton>().GetWasPlayed())
                    t.gameObject.GetComponent<TrackButton>().SetSprite(false);
                }

            }            
        }while(gameState == GameStates.categoryPicking);        
    }
    IEnumerator SwitchTracklLights()
    {
        trackLights = true;
        trackPlayLights = false;
        categoryLights = false;
        SwitchAllLights(false);
        int activeCategory = FindObjectOfType<RoundHandler>().GetActiveCategory();
        for(int index = 0; index < Constants.CATEGORIES_COUNT; index++)
        {
            if(categoryButtonsTransform[index].GetComponent<CategoryButton>().GetCategoryNumber() == activeCategory)
                categoryButtonsTransform[index].gameObject.GetComponent<CategoryButton>().SetSprite(true);
        }

            do{
                for(int index = 0; index < Constants.TRACK_COUNT; index++)
                {
                    if(!trackButtonsTransform[activeCategory].GetChild(index).gameObject.GetComponent<TrackButton>().GetWasPlayed())
                    trackButtonsTransform[activeCategory].GetChild(index).gameObject.GetComponent<TrackButton>().SetSprite(true);
                    yield return new WaitForSeconds(lightSwitchTimer);
                    if(!trackButtonsTransform[activeCategory].GetChild(index).gameObject.GetComponent<TrackButton>().GetWasPlayed())
                    trackButtonsTransform[activeCategory].GetChild(index).gameObject.GetComponent<TrackButton>().SetSprite(false);
                }            
            }while(gameState == GameStates.trackPicking);
    }

    void SwitchAllLights(bool turnOn)
    {
        for(int index = 0; index < Constants.CATEGORIES_COUNT; index++)
            {                
                categoryButtonsTransform[index].gameObject.GetComponent<CategoryButton>().SetSprite(turnOn);           
                foreach(Transform t in trackButtonsTransform[index])
                    {
                        if(!t.GetComponent<TrackButton>().GetWasPlayed())
                        t.gameObject.GetComponent<TrackButton>().SetSprite(turnOn);
                    }
            }
    }

    void SwitchCurrentTrackLight()
    {
        trackLights = false;
        trackPlayLights = true;
        categoryLights = false;
        SwitchAllLights(false);
        int activeCategory = FindObjectOfType<RoundHandler>().GetActiveCategory();
        int activeTrack = FindObjectOfType<RoundHandler>().GetActiveTrack();
        trackButtonsTransform[activeCategory].GetChild(activeTrack).gameObject.GetComponent<TrackButton>().SetSprite(true);
    }

    public void FillGrid(RoundGrid grid)
    {
        for (int categoryIndex = 0; categoryIndex < Constants.CATEGORIES_COUNT; categoryIndex++)
            {            
                categoryButtonsTransform[categoryIndex].GetComponent<CategoryButton>().SetCategoryName(grid.categoryNames[categoryIndex]);
                for(int trackIndex = 0; trackIndex < Constants.TRACK_COUNT; trackIndex ++)
                {
                    trackButtonsTransform[categoryIndex].GetChild(trackIndex).GetComponent<TrackButton>().SetTrackPoints(grid.pointsTracks[categoryIndex,trackIndex]);
                    trackButtonsTransform[categoryIndex].GetChild(trackIndex).GetComponent<TrackButton>().SetTrack(grid.pathTracksQuest[categoryIndex,trackIndex]);
                }  
            }
    }

    public void DisableTrackButton(int category, int track)
    {
        trackButtonsTransform[category].GetChild(track).GetComponent<TrackButton>().SetWasPlayed();
    }

    public void PushNotification(string text)
    {
        GameObject notification = Instantiate(notificationPrefab, transform);
        notification.GetComponentInChildren<Text>().text = text;
        notification.GetComponent<Animator>().SetTrigger("CallNotification");
    }    
}
