using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CategoryButton : MonoBehaviour
{
    [SerializeField]
    int categoryNumber;
    [SerializeField]
    Text categoryName;
    Button button;

    [SerializeField]
    private Sprite categoryUnchecked; 
    [SerializeField]
    private Sprite categoryChecked; 
    // Start is called before the first frame update
    void Start()
    {   
        categoryNumber = transform.GetSiblingIndex();
        button = GetComponent<Button>();
        button.onClick.AddListener(ButtonClicked);
    }

    void Update()
    {
        GameStates gameState = FindObjectOfType<RoundHandler>().GetGameState();
        if(gameState == GameStates.categoryPicking)
        button.enabled = true;
        else
        button.enabled = false;
    }

    void ButtonClicked()
    {
        FindObjectOfType<SfxPlayer>().PlaySound("categoryChoose");
        FindObjectOfType<RoundHandler>().SetGameState(GameStates.trackPicking);
        FindObjectOfType<RoundHandler>().SetActiveCategory(categoryNumber);       
    }

        public void SetSprite(bool isChecked)
    {                       
        if(isChecked)
            GetComponent<Image>().sprite = categoryChecked;
            else
            GetComponent<Image>().sprite = categoryUnchecked;
    }

    
    public int GetCategoryNumber()
    {
        return categoryNumber;
    }

    public void SetCategoryName(string name)
    {
        categoryName.text = name;
    }
}
