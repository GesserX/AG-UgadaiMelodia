using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamLogo : MonoBehaviour
{
    public Team team;
    //public int teamPoints { get; set;}
    //public string teamName{ get; set;}

    [SerializeField]
    Text teamNameUI;
    [SerializeField]
    Text teamPointsUI;


    // Update is called once per frame
    void Update()
    {
        teamNameUI.text = team.teamName;
        teamPointsUI.text = team.teamPoints.ToString();
    }
}
