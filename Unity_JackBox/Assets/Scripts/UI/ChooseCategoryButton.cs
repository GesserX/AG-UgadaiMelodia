using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseCategoryButton : MonoBehaviour
{
    

    Color buttonPressed = Color.blue;
    Color buttonUnpressed = Color.white;

    GameSetupHandler gameSetupHandler;

    Button button;
    int buttonIndex;

    bool pressed;

    // Start is called before the first frame update
    void Start()
    {
        gameSetupHandler = FindObjectOfType<GameSetupHandler>();
        button = GetComponent<Button>();
        buttonIndex = transform.GetSiblingIndex();
        button.onClick.AddListener(ButtonClicked);
    }

    void ButtonClicked()
    {
        if(!pressed)
        {
            gameSetupHandler.AddCategory(buttonIndex);
            button.GetComponent<Image>().color = buttonPressed;
            pressed = true;
        }
        else
        {
            gameSetupHandler.RemoveCategory(buttonIndex);
            button.GetComponent<Image>().color = buttonUnpressed;
            pressed = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
