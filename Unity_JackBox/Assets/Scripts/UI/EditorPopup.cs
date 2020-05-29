using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditorPopup : MonoBehaviour
{

    [SerializeField]
    Text label;
    [SerializeField]
    InputField teamName;
    [SerializeField]
    Button addButton;
    [SerializeField]
    Button editButton;

    // Start is called before the first frame update
    void Start()
    {
        addButton.onClick.AddListener(ClickAddButton);
        editButton.onClick.AddListener(ClickEditButton);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(1))
        {
            ClosePopup();
        }
    }

    void ClickAddButton()
    {
        FindObjectOfType<TeamHandler>().AddTeam(teamName.text);
        FindObjectOfType<GameSetupHandler>().RefreshTeamList();
        ClosePopup();
    }

        void ClickEditButton()
    {
        int currentTeam = FindObjectOfType<GameSetupHandler>().selectedTeam;
        FindObjectOfType<TeamHandler>().teamList[currentTeam].teamName = teamName.text;
        FindObjectOfType<GameSetupHandler>().RefreshTeamList();
        ClosePopup();
    }

    public void OpenAddPopup()
    {
        teamName.text = "";
        addButton.gameObject.SetActive(true);
        editButton.gameObject.SetActive(false);
        gameObject.SetActive(true);
    }

    public void OpenEditPopup()
    {
        int currentTeam = FindObjectOfType<GameSetupHandler>().selectedTeam;
        addButton.gameObject.SetActive(false);
        editButton.gameObject.SetActive(true);
        teamName.text = FindObjectOfType<TeamHandler>().teamList[currentTeam].teamName;
        gameObject.SetActive(true);
    }

    public void ClosePopup()
    {
        gameObject.SetActive(false);
    }


}
