using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using JMRSDK.Toolkit.UI;

public class BMICalculator : MonoBehaviour
{
    [SerializeField] JMRUIPrimaryInputField heightIF, weightIF, ageIF;
    [SerializeField] TextMeshProUGUI infoText;
    [SerializeField] TextMeshProUGUI bmiText;

    public void Next()
    {
        if (string.IsNullOrEmpty(heightIF.Text) || string.IsNullOrEmpty(weightIF.Text)
            || string.IsNullOrEmpty(ageIF.Text))
        {
            infoText.text = "Please fill all the details".ToUpper();
            return;
        }

        if (float.Parse(weightIF.Text.Trim()) == -1)
        {
            infoText.text = "InValid Weight Value".ToUpper();
            return;
        }

        if (float.Parse(heightIF.Text.Trim()) == -1)
        {
            infoText.text = "InValid Height Value".ToUpper();
            return;
        }

        var kg = float.Parse(weightIF.Text.Trim());
        var m = float.Parse(heightIF.Text.Trim());

        var bmi = kg / m * m;
        bmiText.text = $"BMI : {bmi}";

        var userData = User.user.GetUser();
        userData.userWieght = weightIF.Text.Trim();
        userData.userHieght = heightIF.Text.Trim();
        userData.userBMI = $"{bmi}";
        userData.userAge = ageIF.Text.Trim();
        infoText.text = "Done";
        User.user.CreateUser(userData);
    }


    public void BackButton()
    {
        ScreenManager.screenManager.ShowScreen(MenuScreens.Dashboard);
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKey(KeyCode.I))
        {
            heightIF.Text = "5.8";
            weightIF.Text = "75";
            ageIF.Text = "22";
            Next();
        }
    }
#endif


}
