using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using Firebase.Unity.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CreateRoom : MonoBehaviour
{
    bool trans = false, logInformation = false, createDone = false;
    string logContend = "";
    InputField room;
    InputField[] player = new InputField[4];
    Text log;

    DependencyStatus dependencyStatus = DependencyStatus.UnavailableOther;
    protected bool isFirebaseInitialized = false;

    // Start is called before the first frame update
    private void Awake()
    {
        room = transform.GetChild(0).Find("InputField").GetComponent<InputField>();
        for (int i = 0; i < 4; i++)
        {
            player[i] = transform.GetChild(i+1).Find("InputField").GetComponent<InputField>();
        }
        log = transform.Find("Log").GetChild(0).GetComponent<Text>();
    }
    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                InitializeFirebase();
            }
            else
            {
                Debug.LogError(
                  "Could not resolve all Firebase dependencies: " + dependencyStatus);
                log.text = "Could not resolve all Firebase dependencies: " + dependencyStatus;
            }
        });

    }
    protected virtual void InitializeFirebase()
    {
        FirebaseApp app = FirebaseApp.DefaultInstance;
        // NOTE: You'll need to replace this url with your Firebase App's database
        // path in order for the database connection to work correctly in editor.
        app.SetEditorDatabaseUrl("https://dtdgjcheat.firebaseio.com/");
        if (app.Options.DatabaseUrl != null)
            app.SetEditorDatabaseUrl(app.Options.DatabaseUrl);
        isFirebaseInitialized = true;
    }
    



    // Update is called once per frame
    void Update()
    {
        if (logInformation) ShowLogInfo();
        if (createDone) UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }

    public void Confirm() {
        
        Debug.Log("confirm");
        if (trans) {
            Debug.Log("忙碌中");
            return;
        } 
        if (string.IsNullOrEmpty(room.text))
        {
            LogInfo("房間名不能為空");
            return;
        }
        else {
            trans = true;
            FirebaseDatabase.DefaultInstance.GetReference(room.text).GetValueAsync().ContinueWith(task => {
                if (task.IsFaulted)
                {
                    LogInfo("發生錯誤");
                }
                else if (task.IsCompleted)
                {
                    trans = false;
                    DataSnapshot snapshot = task.Result;
                    if (snapshot.Value != null)
                    {
                        LogInfo("已有相同房間名");
                    }
                    else {
                        Debug.Log("go check player");

                        CheckPlayerName();

                        Debug.Log("end callback");
                    }
                }
            });
        }


    }
    void CheckPlayerName() {
        int playerNum = 0;
        bool sameName = false;
        for (int i = 0; i < 4; i++)
        {
            if (!string.IsNullOrEmpty(player[i].text))
            {
                for (int j = i+1; j < 4; j++)
                {
                    if (string.IsNullOrEmpty(player[j].text)) continue;
                    if (player[i].text.CompareTo(player[j].text) == 0)
                    {
                        sameName = true;
                        break;
                    }
                }
                if (sameName)
                {
                    LogInfo("玩家名字不能相同");
                    playerNum = 0;
                    return;
                }
                playerNum++;
            }
        }

        Debug.Log("有" + playerNum + "位玩家");

        if (playerNum < 4) {
            LogInfo("需要四位玩家才能遊玩");
        } 
        else {
            Debug.Log("準備創建");
            DatabaseReference roomRef = FirebaseDatabase.DefaultInstance.GetReference(room.text);
            string[] playerName = new string[4];
            for (int i = 0; i < 4; i++)
            {
                (roomRef.Child(player[i].text)).Child("HP").SetValueAsync(100);
                (roomRef.Child(player[i].text)).Child("ATK").SetValueAsync(100);
                (roomRef.Child(player[i].text)).Child("DEF").SetValueAsync(100);
                GameManager.Instance.PlayerName[i] = player[i].text;
            }
            LogInfo("創建完成，等待跳轉");
            createDone = true;
            ChangePlayerInfo.s_ChangePlayerInfo = new ChangePlayerInfo(roomRef);
           // GameManager.Instance.PlayerName = playerName;
            
        }
    }

    void LogInfo(string c) {
        Debug.Log(c);
        logInformation = true;
        logContend = c;

    }
    void ShowLogInfo() {
        logInformation = false;
        log.text = logContend;
    }
}
