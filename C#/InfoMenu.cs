using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using JMRSDK.Toolkit.UI;
using JMRSDK.Toolkit;
using TMPro;
using System;

public class InfoMenu : MonoBehaviour
{
    [SerializeField] JMRUIPrimaryRadioButtonGroup genderRadioGroup;
    [SerializeField] JMRUIPrimaryRadioButton maleRadioButton;
    [SerializeField] JMRUIPrimaryRadioButton femaleRadioButton;

    [Space]
    [SerializeField] TMP_Dropdown heightDropDown;
    [SerializeField] TMP_Dropdown weightDropDown;
    [SerializeField] TMP_Dropdown ageDropDown;


    [Space]
    [SerializeField] List<string> heightData = new List<string>();
    [SerializeField] List<string> weightData = new List<string>();
    [SerializeField] List<string> ageData = new List<string>();

    [SerializeField] TextMeshProUGUI infoText;

    private void OnEnable()
    {
        heightDropDown.options = new List<TMP_Dropdown.OptionData>();
        heightDropDown.AddOptions(heightData);

        weightDropDown.options = new List<TMP_Dropdown.OptionData>();
        weightDropDown.AddOptions(weightData);

        ageDropDown.options = new List<TMP_Dropdown.OptionData>();
        ageDropDown.AddOptions(ageData);
    }


    public void OnGenderRadioButtonClick(bool male)
    {
        genderRadioGroup.OnItemClick(male ? maleRadioButton : femaleRadioButton);
    }

    public void Next()
    {
        var userData = User.user.GetUser();
        userData.userWieght = weightDropDown.options[weightDropDown.value].text;
        userData.userHieght = heightDropDown.options[heightDropDown.value].text;
        userData.userGender = (maleRadioButton.IsOn) ? "Male" : "Female";
        userData.userAge = ageDropDown.options[ageDropDown.value].text;
        infoText.text = "Done";
        User.user.CreateUser(userData);
        CharacterManager.characterManager.SetupCharacter(maleRadioButton.IsOn);
        ScreenManager.screenManager.ShowScreen(MenuScreens.Dashboard);
    }

    public void BackButton()
    {
        ScreenManager.screenManager.ShowScreen(MenuScreens.Login);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.I))
        {
            Next();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            maleRadioButton.IsOn = false;
            femaleRadioButton.IsOn = true;
        }
    }

    //#if UNITY_EDITOR
    //    private void Update()
    //    {
    //        if (Input.GetKey(KeyCode.I))
    //        {
    //            Next();
    //        }

    //        if (Input.GetKeyDown(KeyCode.F))
    //        {
    //            maleRadioButton.IsOn = false;
    //            femaleRadioButton.IsOn = true;
    //        }
    //    }
    //#endif

}
