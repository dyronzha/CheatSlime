using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CharacterSelect : MonoBehaviour
{
    [SerializeField]float speed;

    int readyNum = 0;
    Vector2 maxPosition, minPosition;
    bool[] hasChoosen = new bool[4] { false, false, false, false };
    string[] playerInput = new string[4] { "p1", "p2", "p3", "p4"};
    RectTransform[] playerIcon = new RectTransform[4];
    Image[] characterDis = new Image[4];



    // Start is called before the first frame update
    void Start()
    {
        
        maxPosition = new Vector2(1, Screen.height);
        minPosition = new Vector2();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 4; i++)
        {
            Vector2 dir = new Vector2(Input.GetAxis(playerInput + "Horiztonal"), Input.GetAxis(playerInput + "Vertical")).normalized;
            Vector2 nextPos = playerIcon[i].anchoredPosition + speed * dir;

            playerIcon[i].anchoredPosition += speed * dir;

        }
    }
}

