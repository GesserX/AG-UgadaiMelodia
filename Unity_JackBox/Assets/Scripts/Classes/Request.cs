using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Request
{
    public Team team { get; set; }
    public string answer { get; set; }

    public Request(Team team, string answer)
    {
        this.team = team;
        this.answer = answer;
    }
}
