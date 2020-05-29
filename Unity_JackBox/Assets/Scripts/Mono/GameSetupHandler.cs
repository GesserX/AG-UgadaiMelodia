using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSetupHandler : MonoBehaviour
{

    string[] configs;
    List<Category> categories;

    [SerializeField]
    Transform categoriesRect;//Родитель для кнопок категорий
    
    [SerializeField]
    Transform teamsRect;//Родитель для кнопок команд
    [SerializeField]
    GameObject categoryButtonPrefab;
    [SerializeField]
    GameObject teamButtonPrefab;

    public Text StartButtonText;

    List<int> chosenCategories;
    
    public int selectedTeam = 0;

    // Start is called before the first frame update
    void Start()
    {
        configs = FileHandler.GetConfigs();
        FillCategories();
        chosenCategories = new List<int>();
    }

    void FillCategories()
    {
        categories = new List<Category>();
        foreach(string config in configs)
        {
            Category category = Category.CreateCategory(config);
            categories.Add(category);
            GameObject categoryButton = Instantiate(categoryButtonPrefab);
            categoryButton.transform.GetChild(0).GetComponent<Text>().text = category.categoryName;
            categoryButton.transform.SetParent(categoriesRect, false);
        }
    }    

    public void AddCategory(int index)
    {
        chosenCategories.Add(index);
    }

    public void RemoveCategory(int index)
    {
        chosenCategories.Remove(index);
    }

    public void RemoveTeam()
    {
        FindObjectOfType<TeamHandler>().RemoveTeam(selectedTeam);
        RefreshTeamList();
    }

    public void SelectTeam(int index)
    {
        TeamButton[] teamButtons = FindObjectsOfType<TeamButton>();
        foreach(TeamButton button in teamButtons)
        {
            button.Deselect();
        }
        
        selectedTeam = index;
    }

    public void StartGame()
    {
        if(chosenCategories.Count > 3)
        {
            for (int i = 0; i < 4; i++)
            {
                PlayerPrefs.SetInt(i.ToString(), chosenCategories[i]);
            }
            SceneManager.LoadScene("Round");
        }        
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(0);        
    }

    public void RefreshTeamList()
    {
        foreach(Transform t in teamsRect)
        {
            Destroy(t.gameObject);
        }
        foreach(Team team in FindObjectOfType<TeamHandler>().teamList)
        {                        
            GameObject teamButton = Instantiate(teamButtonPrefab);
            teamButton.GetComponent<TeamButton>().buttonIndex = FindObjectOfType<TeamHandler>().teamList.IndexOf(team);
            teamButton.transform.GetChild(0).GetComponent<Text>().text = team.teamName;
            teamButton.transform.SetParent(teamsRect, false);
        }
    }

    void Update()
    {
        if(chosenCategories.Count < 4)
        {
            StartButtonText.text = "Вам нужно выбрать 4 категории";
        }
        else
        {
            StartButtonText.text = "Начать раунд";
        }
    }
}
