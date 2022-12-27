using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DashBoard : MonoBehaviour
{
    [SerializeField] GameObject menuHolder;
    [SerializeField] GameObject infoHolder;

    [Space]
    [SerializeField] GameObject[] selectedIcons;

    [Space]
    [SerializeField] TextMeshProUGUI infoTitleText;
    [SerializeField] TextMeshProUGUI infoDisText;

    [Space]
    [SerializeField] DifficultyObject[] difficultyObjects;
    [SerializeField] ExerciseObject[] exerciseObjects;
    [Space]
    [Header("Individual Mode")]
    [SerializeField] TMP_Dropdown exerciseSetDropDown;
    [SerializeField] TMP_Dropdown exerciseNameDropDown;


    int selectedIndex;

    private void Awake()
    {
        ToggleRightMenus();
        if(CharacterManager.characterManager)
            CharacterManager.characterManager.ResetMe();

        var tmp = new List<string>();
        exerciseSetDropDown.options = new List<TMP_Dropdown.OptionData>();
        for (int i = 0; i < difficultyObjects.Length; i++)
        {
            tmp.Add(difficultyObjects[i].difficultyType.ToString());
        }
        exerciseSetDropDown.AddOptions(tmp);

        var tmp2 = new List<string>();
        exerciseNameDropDown.options = new List<TMP_Dropdown.OptionData>();
        for (int i = 0; i < exerciseObjects.Length; i++)
        {
            tmp2.Add(exerciseObjects[i].exerciseName);
        }
        exerciseNameDropDown.AddOptions(tmp2);
    }

    public void OnClick(int i)
    {
        switch (i)
        {
            case 0:
                OpenDifficiultyInfo(i);
                break;
            case 1:
                OpenDifficiultyInfo(i);
                break;
            case 2:
                OpenDifficiultyInfo(i);
                break;
            case 3:
                break;
            case 4:
                ScreenManager.screenManager.ShowScreen(MenuScreens.EditBMI);
                break;
            case 5:
                BackButton();
                break;
        }
    }

    private void BackButton()
    {
        if (infoHolder.activeInHierarchy)
        {
            foreach (var item in selectedIcons)
            {
                item.SetActive(false);
            }

            ToggleRightMenus();
        }
        else
        {
            Debug.Log("LOL");
            MessageHandler.messageHandler.ShowDualButtonPopUp("ARE YOU SURE YOU WANT TO LOGOUT?",
                () => {
                    ScreenManager.screenManager.ShowScreen(MenuScreens.Login);
                },
                () => { });

        }
    }

    private void OpenDifficiultyInfo(int index)
    {
        foreach (var item in selectedIcons)
        {
            item.SetActive(false);
        }

        selectedIndex = index;
        var type = difficultyObjects[index];
        selectedIcons[index].SetActive(true);
        infoDisText.text = type.discription;
        infoTitleText.text = type.difficultyType.ToString();
        ToggleRightMenus(true);
    }

    public void StartDifficultyMode()
    {
        difficultyObjects[selectedIndex].individualMode = false;
        ScreenManager.screenManager.difficultyObject = difficultyObjects[selectedIndex];
        ScreenManager.screenManager.ShowScreen(MenuScreens.ExersiseDashboard);
    }

    public void StartIndividualMode()
    {
        var difficulty = difficultyObjects[exerciseSetDropDown.value];
        difficulty.individualMode = true;
        difficulty.individualExercise = exerciseObjects[exerciseNameDropDown.value];
        ScreenManager.screenManager.difficultyObject = difficultyObjects[selectedIndex];
        ScreenManager.screenManager.ShowScreen(MenuScreens.ExersiseDashboard);

    }

    private void ToggleRightMenus(bool showInfo = false)
    {
        menuHolder.SetActive(!showInfo);
        infoHolder.SetActive(showInfo);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Alpha0))
        {
            OnClick(0);
        }
        else if (Input.GetKey(KeyCode.Alpha1))
        {
            OnClick(1);
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            OnClick(2);
        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            OnClick(3);
        }
        else if (Input.GetKey(KeyCode.Alpha4))
        {
            OnClick(4);
        }
        else if (Input.GetKey(KeyCode.Alpha5))
        {
            OnClick(5);
        }
        else if (Input.GetKey(KeyCode.Alpha6) && infoHolder.activeInHierarchy)
        {
            StartDifficultyMode();
        }
        else if (Input.GetKey(KeyCode.Alpha7))
        {
            StartIndividualMode();
        }
        else if (Input.GetKey(KeyCode.V))
            BackButton();
    }

    //#if UNITY_EDITOR
    //    private void Update()
    //    {
    //        if (Input.GetKey(KeyCode.Alpha0))
    //        {
    //            OnClick(0);
    //        }else if (Input.GetKey(KeyCode.Alpha1))
    //        {
    //            OnClick(1);
    //        }else if (Input.GetKey(KeyCode.Alpha2))
    //        {
    //            OnClick(2);
    //        }else if (Input.GetKey(KeyCode.Alpha3))
    //        {
    //            OnClick(3);
    //        }else if (Input.GetKey(KeyCode.Alpha4))
    //        {
    //            OnClick(4);
    //        }else if (Input.GetKey(KeyCode.Alpha5))
    //        {
    //            OnClick(5);
    //        }else if (Input.GetKey(KeyCode.Alpha6) && infoHolder.activeInHierarchy)
    //        {
    //            StartDifficultyMode();
    //        }
    //    }
    //#endif
}
