using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using JMRSDK.Toolkit.UI;
using CodeMonkey.Utils;

public class ExcersiseDashboard : MonoBehaviour
{
    public TextMeshProUGUI exNameText;
    public TextMeshProUGUI exHoldTimeText;
    public TextMeshProUGUI exRepeatTimeText;
    public TextMeshProUGUI exLeftText;
    public TextMeshProUGUI infoPanelText;

    [Space]
    [SerializeField] GameObject startingTimerPanel;
    [SerializeField] TextMeshProUGUI startingTimerText;

    [Space]
    [SerializeField] GameObject infoPanel;


    DifficultyObject difficultyObject;

    public static ExcersiseDashboard dashboard;

    private void Awake()
    {
        dashboard = this;
        difficultyObject = ScreenManager.screenManager.difficultyObject;
    }

    private void OnEnable()
    {
        exNameText.text = exHoldTimeText.text = exRepeatTimeText.text = "";
        startingTimerText.text = $"Starting Yoga in 5";
        ProceedStartTimer();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.V))
            BackButton();
    }


    public void BackButton()
    {
        //Character.character.ExitExMode();
        ScreenManager.screenManager.ShowScreen(MenuScreens.Dashboard);
    }

    private void ProceedStartTimer()
    {
        startingTimerPanel.SetActive(true);
        StartCoroutine(Countdown(5,(value)=> {
            startingTimerText.text = $"Starting Yoga in {value}";
        },()=>
        {
            SetupCharacter();
        }));
    }

    private void SetupCharacter()
    {
        startingTimerPanel.SetActive(false);
        infoPanel.SetActive(true);
        //Character.character.StartYoga(difficultyObject);
        CharacterManager.characterManager.SetupCharacterExerciseMode(difficultyObject);
    }

    IEnumerator Countdown(int seconds,System.Action<int> tickCallback = null,System.Action resultCallBack = null)
    {
        int counter = seconds;
        while (counter > 0)
        {
            yield return new WaitForSeconds(1);
            counter--;
            tickCallback?.Invoke(counter);
        }
        resultCallBack?.Invoke();
    }


    private void OnDestroy()
    {
        dashboard = null;
    }
}
