using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using Firebase.Unity.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoView : MonoBehaviour
{

    public static DatabaseReference s_playerRef;

    bool hpChange, atkChange, defChange;
    string hp, atk, def;

    bool logInformation = false;
    string logContend = "";

    Text name;
    Text HP, ATK, DEF;
    Text log;


    // Start is called before the first frame update
    private void Awake()
    {
        name = transform.Find("Name").GetComponent<Text>();
        HP = transform.Find("HP").Find("Text").GetComponent<Text>();
        ATK = transform.Find("ATK").Find("Text").GetComponent<Text>();
        DEF = transform.Find("DEF").Find("Text").GetComponent<Text>();
        log = transform.Find("Log").GetChild(0).GetComponent<Text>();
    }
    void Start()
    {
        name.text = s_playerRef.Key;

        s_playerRef.Child("HP").GetValueAsync().ContinueWith(task => {
            if (task.IsFaulted)
            {
                Debug.Log("failllllll");
                // Handle the error...
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.Value == null) Debug.Log("找不到");
                else
                {
                    Debug.Log(snapshot.Value);
                    hpChange = true;
                    hp = snapshot.Value.ToString();
                    Debug.Log("hhhppp " + hp);
                }
            }
        });
        s_playerRef.Child("ATK").GetValueAsync().ContinueWith(task2 => {
            if (task2.IsFaulted)
            {
                // Handle the error...
            }
            else if (task2.IsCompleted)
            {
                DataSnapshot snapshot = task2.Result;
                if (snapshot.Value == null) Debug.Log("找不到");
                else
                {
                    atkChange = true;
                    atk = snapshot.Value.ToString();
                }
            }
        });
        s_playerRef.Child("DEF").GetValueAsync().ContinueWith(task3 => {
            if (task3.IsFaulted)
            {
                // Handle the error...
            }
            else if (task3.IsCompleted)
            {
                DataSnapshot snapshot = task3.Result;
                if (snapshot.Value == null) Debug.Log("找不到");
                else
                {
                    defChange = true;
                    def = snapshot.Value.ToString();
                }
            }
        });

        StartListener();
    }
    protected void StartListener()
    {

        s_playerRef.ChildChanged += (object sender2, ChildChangedEventArgs e2) =>
        {
            if (e2.DatabaseError != null)
            {
                Debug.LogError(e2.DatabaseError.Message);
                return;
            }
            if (e2.Snapshot != null)
            {
                if (e2.Snapshot.Key.CompareTo("HP") == 0) {
                    hp = e2.Snapshot.Value.ToString();
                    hpChange = true;
                }
                else if (e2.Snapshot.Key.CompareTo("ATK") == 0)
                {
                    atk = e2.Snapshot.Value.ToString();
                    atkChange = true;
                }
                else if (e2.Snapshot.Key.CompareTo("DEF") == 0)
                {
                    def = e2.Snapshot.Value.ToString();
                    defChange = true;
                }

            }
        };
    }


    // Update is called once per frame
    void Update()
    {
        if (logInformation) ShowLogInfo();
        if (hpChange) {
            HP.text = hp;
            hpChange = false;
        }
        if (atkChange) {
            ATK.text = atk;
            atkChange = false;
        } 
        if (defChange) {
            DEF.text = def;
            defChange = false;
        } 
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
