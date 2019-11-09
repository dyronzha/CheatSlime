using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using Firebase.Unity.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSignIn : MonoBehaviour
{
    DependencyStatus dependencyStatus = DependencyStatus.UnavailableOther;
    protected bool isFirebaseInitialized = false;

    bool trans = false, logInformation = false, connectDone = false, searchPlayer = false;
    string logContend = "";
    InputField room;
    InputField player;
    Text log;

    // Start is called before the first frame update
    private void Awake()
    {
        room = transform.GetChild(0).Find("InputField").GetComponent<InputField>();
        player = transform.GetChild(1).Find("InputField").GetComponent<InputField>();
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
                LogInfo("Could not resolve all Firebase dependencies: " + dependencyStatus);
                Debug.LogError(
                  "Could not resolve all Firebase dependencies: " + dependencyStatus);
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
        if (searchPlayer) SearchPlayer();
        if (connectDone) UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void Confirm() {
        LogInfo("登入中");
        if (trans)
        {
            Debug.Log("忙碌中");
            return;
        }
        if (string.IsNullOrEmpty(room.text))
        {
            LogInfo("房間名不能為空");
            return;
        }
        else
        {
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
                    if (snapshot.Value == null)
                    {
                        LogInfo("沒有此房間");
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(player.text))
                        {
                            LogInfo("玩家名不能為空");
                            return;
                        }
                        else
                        {
                            searchPlayer = true;
                        }
                    }
                }
            });
        }
    }

    void SearchPlayer() {
        trans = true;
        searchPlayer = false;
        FirebaseDatabase.DefaultInstance.GetReference(room.text).Child(player.text).GetValueAsync().ContinueWith(task => {
            if (task.IsFaulted)
            {
                LogInfo("發生錯誤");
            }
            else if (task.IsCompleted)
            {
                trans = false;
                DataSnapshot snapshot = task.Result;
                if (snapshot.Value == null)
                {
                    LogInfo(room.text + "房間沒有  " + player.text  +" 玩家 ");
                }
                else
                {
                    connectDone = true;
                    PlayerInfoView.s_playerRef = FirebaseDatabase.DefaultInstance.GetReference(room.text).Child(player.text);
                    LogInfo("連接成功，等待跳轉");
                }
            }
        });
    }

        void LogInfo(string c)
    {
        Debug.Log(c);
        logInformation = true;
        logContend = c;

    }
    void ShowLogInfo()
    {
        logInformation = false;
        log.text = logContend;
    }

}
