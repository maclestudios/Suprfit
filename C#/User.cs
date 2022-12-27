using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : MonoBehaviour
{

    private UserData userData;

    public static User user;

    private void Awake()
    {
        user = this;
        DontDestroyOnLoad(this);
    }

    public void CreateUser(UserData newUser)
    {
        userData = newUser;
    }

    public UserData GetUser()
    {
        return userData;
    }
}

[System.Serializable]
public class UserData
{
    public string userName, userHieght, userWieght, userBMI, userAge, userGender;
}
