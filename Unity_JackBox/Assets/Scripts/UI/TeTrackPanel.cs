using System.Collections;
using System.Collections.Generic;
using System.IO;
using SimpleFileBrowser;
using UnityEngine;
using UnityEngine.UI;

public class TeTrackPanel : MonoBehaviour
{
    [SerializeField]
    Button questionButton;
    [SerializeField]
    Button answerButton;
    [SerializeField]
    Button clearButton;
    [SerializeField]
    public Text questionText;

    [SerializeField]
    public Text answerText;
    [SerializeField]
    public InputField tags;
    public InputField points;

    public string answerTrackPath;
    public string questionTrackPath;

    static string lastPath = Application.streamingAssetsPath;

    // Start is called before the first frame update    
    void Start()
    {
        answerButton.onClick.AddListener(ClickAnswer);
        questionButton.onClick.AddListener(ClickQuestion);
        clearButton.onClick.AddListener(ClearButton);
    }

    void ClickQuestion()
    {
        FileBrowser.SetFilters(true, new FileBrowser.Filter( "Треки",  ".mp3", ".wav", ".ogg"));
        FileBrowser.SetDefaultFilter(".mp3");    
        FileBrowser.ShowLoadDialog((path) => { LoadQuestion(path); lastPath = path;}, null, false, lastPath, "Выберите трек", "Загрузить");
    }

    void ClickAnswer()
    {
        FileBrowser.SetFilters(true, new FileBrowser.Filter( "Треки", ".mp3", ".wav", ".ogg"));
        FileBrowser.SetDefaultFilter(".mp3");    
        FileBrowser.ShowLoadDialog((path) => { LoadAnswer(path); lastPath = path; }, null, false, lastPath, "Выберите трек", "Загрузить");
    }

    void LoadAnswer(string path)
    {
        path = path.Replace('\\', '/');
        string filename = Path.GetFileNameWithoutExtension(path);
        answerTrackPath = path;
        answerText.text = filename;
    }

    void LoadQuestion(string path)
    {
        path = path.Replace('\\', '/');
        string filename = Path.GetFileNameWithoutExtension(path);
        questionTrackPath = path;
        questionText.text = filename;
    }

    void ClearButton()
    {
        questionText.text = "Трек вопрос";
        answerText.text = "Трек ответ";
        points.text = "";
        questionTrackPath = null;
        answerTrackPath = null;
        tags.text = null;
    }

}
