using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using Firebase.Unity.Editor;

public class CheckID : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Z)) {
            FirebaseDatabase.DefaultInstance.GetReference("GameCount").Child("test").SetValueAsync(30);
            FirebaseDatabase.DefaultInstance.GetReference("GameCount").Child("test2").SetValueAsync(-30);
        }
        
        if (Input.GetKeyDown(KeyCode.Space)) {
            FirebaseDatabase.DefaultInstance.GetReference("GameCount").GetValueAsync().ContinueWith(task => {
                if (task.IsFaulted)
                {
                    // Handle the error...
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    if (snapshot.Value == null) Debug.Log("找不到");
                    else {
                        Debug.Log(snapshot.Value);
                        FirebaseDatabase.DefaultInstance.GetReference("GameCount").SetValueAsync(20);
                    }
                    // Do something with snapshot...
                }
            });
        }




    }
}
