using System.Collections;
using System.Collections.Generic;
using SimpleFileBrowser;
using UnityEngine;
using UnityEngine.UI;

public class CategoryEditor : MonoBehaviour
{
    public InputField categoryName;
    [SerializeField]
    public TrackEditor[] tracks;

    public Button saveCategory;
    public Button loadCategory;
    // Start is called before the first frame update
    void Start()
    {
        saveCategory.onClick.AddListener(SaveCategory);
        loadCategory.onClick.AddListener(ClickLoadCategory);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SaveCategory()
    {
        try
        {
             Category category = new Category();
        category.categoryName = categoryName.text;
        for (int i = 0; i<Constants.TRACK_COUNT; i++)
        {
            category.pathTracksQuest[i] = tracks[i].questionTrackPath;
            category.pathTracksAnsw[i] = tracks[i].answerTrackPath;
            category.tracksTags[i] = tracks[i].tags.text + ", " + System.IO.Path.GetFileNameWithoutExtension(category.pathTracksAnsw[i]);            
            category.pointsTracks[i] = int.Parse(tracks[i].points.text);
        }
        FileHandler.SaveConfig(category);
        StartCoroutine(PrintMessage("Успешно сохранено!"));
        }
        catch(System.FormatException ex)
        {
            StartCoroutine(PrintMessage("Ошибка! Заполните все поля!"));
        }
       
    }
    IEnumerator PrintMessage(string msg)
    {
        string backup = categoryName.text;
        categoryName.text = msg;
        yield return new WaitForSeconds(2f);
        categoryName.text = backup;
    }
    void LoadCategory(string path)
    {
        Category category = Category.CreateCategory(path);
        categoryName.text = category.categoryName;
        for (int i = 0; i<Constants.TRACK_COUNT; i++)
        {
            string answName = System.IO.Path.GetFileNameWithoutExtension(category.pathTracksAnsw[i]);
            string questName = System.IO.Path.GetFileNameWithoutExtension(category.pathTracksAnsw[i]);
            tracks[i].answerText.text = answName;
            tracks[i].questionText.text = questName;
            tracks[i].questionTrackPath = category.pathTracksQuest[i];
            tracks[i].answerTrackPath = category.pathTracksAnsw[i];
            tracks[i].tags.text =  category.tracksTags[i];            
            tracks[i].points.text = category.pointsTracks[i].ToString();
        }
    }

    void ClickLoadCategory()
    {
        FileBrowser.SetFilters(true, new FileBrowser.Filter( "Конфиг-файл", ".json"));
        FileBrowser.SetDefaultFilter(".json");    
        FileBrowser.ShowLoadDialog((path) => { LoadCategory(path); }, null, false, Application.streamingAssetsPath, "Выберите трек", "Загрузить");
    }

    public void LoadScene(int index)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(index);
    }
}
