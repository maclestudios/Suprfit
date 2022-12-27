using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JMRSDK.Toolkit.UI;
using TMPro;

public class LoginManager : MonoBehaviour
{
    [SerializeField] JMRUIPrimaryInputField emailIF,passwordIF;
    [SerializeField] TextMeshProUGUI infoText;

    public void Login()
    {
        emailIF.Text = "SuprFit";
        passwordIF.Text = "root";
        if (string.IsNullOrEmpty(emailIF.Text) || string.IsNullOrEmpty(passwordIF.Text))
        {
            infoText.text = "Please fill all the details".ToUpper();
            return;
        }

        if(emailIF.Text.ToLower().Equals("suprfit") && passwordIF.Text.Equals("root"))
        {
            infoText.text = "Login";

            UserData user = new UserData();
            user.userName = emailIF.Text.Trim();
            User.user.CreateUser(user);
            ScreenManager.screenManager.ShowScreen(MenuScreens.LoginInfoPanel);
        }
        else
        {
            infoText.text = "Invalid User".ToUpper();
        }
    }

    public void BackButton()
    {
        MessageHandler.messageHandler.ShowDualButtonPopUp("ARE YOU SURE YOU WANT TO QUIT?", () => { Application.Quit(); },
            () => { });
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.L))
        {
            emailIF.Text = "SuprFit";
            passwordIF.Text = "root";
            Login();
        }else if (Input.GetKey(KeyCode.V))
            BackButton();
    }

    //#if UNITY_EDITOR
    //    private void Update()
    //    {
    //        if (Input.GetKey(KeyCode.L))
    //        {
    //            emailIF.Text = "SuprFit";
    //            passwordIF.Text = "root";
    //            Login();
    //        }
    //    }
    //#endif
}
