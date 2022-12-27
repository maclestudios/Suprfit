using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Splash : MonoBehaviour
{
    [SerializeField] VideoPlayer video;


    private void OnEnable()
    {
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds((float)video.length + .5f);
        //Character.character.ShowCharacter();
        ScreenManager.screenManager.ShowScreen(MenuScreens.Login);
    }
}
