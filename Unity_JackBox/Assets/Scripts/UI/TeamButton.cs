using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamButton : MonoBehaviour
{
    Color buttonPressed = Color.blue;
    Color buttonUnpressed = Color.white;
    GameSetupHandler gameSetupHandler;

    Button button;
    public int buttonIndex;

    // Start is called before the first frame update
    void Start()
    {
        gameSetupHandler = FindObjectOfType<GameSetupHandler>();
        button = GetComponent<Button>();        
        button.onClick.AddListener(ButtonClicked);
    }

    void ButtonClicked()
    {
            gameSetupHandler.SelectTeam(buttonIndex);
            button.GetComponent<Image>().color = buttonPressed;
    }

    public void Deselect()
    {
        button.GetComponent<Image>().color = buttonUnpressed;
    }
}
