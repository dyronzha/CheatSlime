using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using Firebase.Unity.Editor;

public class ChangePlayerInfo
{

    public static ChangePlayerInfo s_ChangePlayerInfo;

    DatabaseReference roomRef;

    // Start is called before the first frame update
    public ChangePlayerInfo(DatabaseReference room)
    {
        roomRef = room;
    }

    public void UpLoadPlayerInfo(string playerName, int infoType, string value) {
        if(infoType ==0) roomRef.Child(playerName).Child("HP").SetValueAsync(value);
        else if(infoType == 1) roomRef.Child(playerName).Child("ATK").SetValueAsync(value);
        else if(infoType == 2) roomRef.Child(playerName).Child("DEF").SetValueAsync(value);

    }
}
