using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerDebugger : MonoBehaviour
{

    public Dropdown teamsDropdown;
    public InputField answerInput;
    public Button sendAnswerButton;

    // Start is called before the first frame update
    void Start()
    {
        FillDropdown(teamsDropdown);
        sendAnswerButton.onClick.AddListener(SendRequest);
    }    

    void FillDropdown(Dropdown dropdown)
    {
        dropdown.ClearOptions();

        List<Team> teams = FindObjectOfType<TeamHandler>().teamList;
        List<string> options = new List<string>();
        if(teams.Count > 0)
        {
            foreach(Team team in teams)
            {
                options.Add(team.teamName);
            }

            dropdown.AddOptions(options);
        }
    }

    void SendRequest()
    {
        Request request = new Request(FindObjectOfType<TeamHandler>().teamList[teamsDropdown.value], answerInput.text);
        if(request.answer != "")
        {
            FindObjectOfType<RoundHandler>().ReceiveRequest(request);
            gameObject.SetActive(false);
        }
    }
}
