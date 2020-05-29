using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RoundHandler : MonoBehaviour
{
    GameStates gameState;
    RoundGrid roundGrid;
    int activeCategory = 0;
    int activeTrack = 0;

    
    void Start() //Инициализация раунда
    {
        roundGrid = RoundGrid.CreateRoundGrid(); // создаем сетку раунда
        initUI(); // Инициализация графического интерфейса
        SetGameState(GameStates.categoryPicking); // Устанавливаем состояние игры на "Выбор категории"
    }

    void Update()
    {
       if(Input.GetMouseButton(1))
        {
            BackToPreviouseGameState();
        }        
    }    

    #region getters-setters
    public GameStates GetGameState()
    {
        return gameState;
    }

     public void SetGameState(GameStates gameState)
    {
        this.gameState = gameState;
    }

    public int GetActiveCategory()
    {
        return activeCategory;
    }

    public void SetActiveCategory(int num)
    {
        activeCategory = num;
    }
    public int GetActiveTrack()
    {
        return activeTrack;
    }

    public void SetActiveTrack(int num)
    {
        activeTrack = num;
    }
    #endregion
    
    void initUI()
    {
        // Здесь - присутствует жуткий костыль, который будет дорабатываться.                      
        
        UIHandler [] handlers = FindObjectsOfType<UIHandler>(); // Подключаем обработчики графического интерфейса         
        handlers[0].FillGrid(roundGrid); // заполняем первое поле (вывод для ведущего)
        handlers[0].AddTeams();
        if(handlers.Length>1) // если второе поле (вывод для игроков) активно заполним его
        {
            handlers[1].FillGrid(roundGrid); // заполняем второе поле (вывод для игроков)
            handlers[1].AddTeams();            
        }
        
        //Конец костыля.
        

    }    


    public void BackToPreviouseGameState()
    {
        switch(GetGameState())
        {
            case GameStates.categoryPicking:
                break;
            case GameStates.trackPicking:
                SetGameState(GameStates.categoryPicking);
                break;
            case GameStates.trackPlaying:
                SetGameState(GameStates.categoryPicking);
                FindObjectOfType<MusicPlayer>().StopTrack();
                break;
            default:
                break;
        }
    }

    public void ReceiveRequest(Request request)
    {
        if(GetGameState() != GameStates.answerProcessing && request.answer != "")
        {
            StopAllCoroutines();
            if(GetGameState() == GameStates.trackPlaying)
            {
                FindObjectOfType<SfxPlayer>().PlaySound("push");
                FindObjectOfType<UIHandler>().PushNotification(request.team.teamName.ToUpper() + "\n" + request.answer);

                StartCoroutine(ProcessRequest(request));
            }
        }       
    }

    IEnumerator ProcessRequest(Request request)
    {
        SetGameState(GameStates.answerProcessing);
        FindObjectOfType<MusicPlayer>().PauseTrack();
        yield return new WaitForSeconds(3f);
        bool answer = AnswerChecker.Calculate(roundGrid.tracksTags[activeCategory, activeTrack], request.answer, 70);
            if(answer)
            {
                FindObjectOfType<SfxPlayer>().PlaySound("true");
                FindObjectOfType<UIHandler>().PushNotification(request.team.teamName.ToUpper() + "\n" + "ответ верный!");
                request.team.teamPoints += roundGrid.pointsTracks[activeCategory, activeTrack];
                FindObjectOfType<UIHandler>().DisableTrackButton(activeCategory, activeTrack);                
                SetGameState(GameStates.categoryPicking);
            }
            else
            {
                FindObjectOfType<SfxPlayer>().PlaySound("false");
                FindObjectOfType<UIHandler>().PushNotification(request.team.teamName.ToUpper() + "\n" + "ответ неверный!");
                yield return new WaitForSeconds(1f);
                FindObjectOfType<MusicPlayer>().ResumeTrack();
                SetGameState(GameStates.trackPlaying);
            }
    }

}
