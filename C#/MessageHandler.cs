using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageHandler : MonoBehaviour
{
    [SerializeField] GameObject box;
    [SerializeField] CanvasGroup canvasGroup;

    public static MessageHandler messageHandler;
    public TextMeshProUGUI descText;
    public GameObject singleButtonPopUp, dualbuttonPopUp;
    private Action okButtonCallBackMethod = null, yesButtonCallBackMethod = null, noButtonCallBackMethod = null;


    [HideInInspector]
    public bool isPanelOpen;


    private void Awake()
    {
        messageHandler = this;
    }

    private void OnDestroy()
    {
        messageHandler = null;
    }

    private void Anim()
    {
        canvasGroup.alpha = 0;
        canvasGroup.gameObject.SetActive(true);
        canvasGroup.LeanAlpha(1, 0.5f);
        box.transform.GetChild(0).localPosition = new Vector2(0, -Screen.height);
        box.transform.GetChild(0).LeanMoveLocalY(0, 0.5f).setEaseOutExpo().delay = 0.1f;
    }

    public void ShowSingleButtonPopUp(string message, Action callBackMethod)
    {
        isPanelOpen = true;
        singleButtonPopUp.SetActive(true);
        dualbuttonPopUp.SetActive(false);

        okButtonCallBackMethod = callBackMethod;
        descText.text = message;
        box.SetActive(true);
        Anim();
    }


    public void ShowDualButtonPopUp(string message, Action clickYes, Action clickNo)
    {
        isPanelOpen = true;
        singleButtonPopUp.SetActive(false);
        dualbuttonPopUp.SetActive(true);

        yesButtonCallBackMethod = clickYes;
        noButtonCallBackMethod = clickNo;


        descText.text = message;
        box.SetActive(true);
        Anim();
    }


    public void OnClickOnOk()
    {
        isPanelOpen = false;
        canvasGroup.gameObject.SetActive(false);
        box.SetActive(false);
        if (okButtonCallBackMethod != null)
        {
            okButtonCallBackMethod();
            okButtonCallBackMethod = null;
        }
    }

    public void OnClickOnYes()
    {
        isPanelOpen = false;
        canvasGroup.gameObject.SetActive(false);
        box.SetActive(false);
        if (yesButtonCallBackMethod != null)
        {
            yesButtonCallBackMethod();
            yesButtonCallBackMethod = null;
        }
    }

    public void OnClickOnNo()
    {
        isPanelOpen = false;
        canvasGroup.gameObject.SetActive(false);
        box.SetActive(false);
        if (noButtonCallBackMethod != null)
        {
            noButtonCallBackMethod();
            noButtonCallBackMethod = null;
        }
    }


}

