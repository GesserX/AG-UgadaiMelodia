using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HostPanel : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    Animator animator;

    [SerializeField]
    Button secondMonitorButton;
    [SerializeField]
    GameObject disactivatedPanel;
    [SerializeField]
    GameObject answerDebuggerPanel;
    void Start()
    {
        disactivatedPanel.SetActive(true);
        secondMonitorButton.onClick.AddListener(SwitchSecondMonitor);
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {        
        animator.SetTrigger("MouseOver");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animator.SetTrigger("MouseExit");
    }

    public void OnPointerClick(PointerEventData eventData)
     {

     }

     void Update()
     {
        if(Display.displays.Length < 1)
            secondMonitorButton.interactable = false;
        if(!disactivatedPanel.activeSelf)
        {
            secondMonitorButton.GetComponentInChildren<Text>().text = "Выключить второй дисплей";
        }
        else
        {
            secondMonitorButton.GetComponentInChildren<Text>().text = "Включить второй дисплей";
        }
     }

     void SwitchSecondMonitor()
     {        
        if (disactivatedPanel.activeSelf)
        {
            disactivatedPanel.SetActive(false);
            if(Display.displays.Length > 1) // если подключен второй монитор - активируем
            Display.displays[1].Activate();            
        }
        else
        {
            disactivatedPanel.SetActive(true);            
        }
    }

    public void SwitchAnswerDebugger()
    {
        answerDebuggerPanel.SetActive(!answerDebuggerPanel.activeSelf);
    }

    public void ExitToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
