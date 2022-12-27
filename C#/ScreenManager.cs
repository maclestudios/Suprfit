using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JMRSDK;
using JMRSDK.InputModule;

public class ScreenManager : MonoBehaviour, IBackHandler
{
    [SerializeField] GameObject layer1Holder;


    [Header("Screens Data")]
    [SerializeField] GameObject[] menuScreens;

    GameObject currentScreen;
    [HideInInspector]
    public MenuScreens currentScreenType;

    public DifficultyObject difficultyObject { get; set; }

    public static ScreenManager screenManager;

    private void Awake()
    {
        JMRInputManager.Instance.AddGlobalListener(gameObject);
        screenManager = this;
    }

    private void Start()
    {
        ShowScreen(MenuScreens.Splash);
    }

    

    public void ShowScreen(MenuScreens menu)
    {
        if (currentScreen)
        {
            Destroy(currentScreen);
            currentScreen.SetActive(false);
        }

        currentScreenType = menu;
        currentScreen = Instantiate(menuScreens[(int)menu]);
        currentScreen.transform.SetParent(layer1Holder.transform, false);
        currentScreen.SetActive(true);
    }

    public void OnBackAction()
    {
        if(currentScreenType == MenuScreens.LoginInfoPanel)
        {
            ShowScreen(MenuScreens.Login);
        }else if(currentScreenType == MenuScreens.Login)
        {
            Application.Quit();
        }else if(currentScreenType == MenuScreens.ExersiseDashboard)
        {
//            Character.character.ExitExMode();
            ShowScreen(MenuScreens.Dashboard);
        }else if(currentScreenType == MenuScreens.Dashboard)
        {
            
        }
    }
}

public enum MenuScreens
{
    Login,
    LoginInfoPanel,
    Dashboard,
    ExersiseDashboard,
    EditBMI,
    Splash
}
