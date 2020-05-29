using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team
{
    public string teamName { get; set; }
    public int teamPoints { get; set; }

    public Team (string name)
    {
        teamName = name;                
    }
}

public class TeamHandler : MonoBehaviour
{    
    public List<Team> teamList = new List<Team>();

     void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("TeamHandler");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        
    }

    public void AddTeam(string name)
    {
        Team team = new Team(name);
        teamList.Add(team);
    }

    public void RemoveTeam(int index)
    {
        teamList.RemoveAt(index);
    }    
}
