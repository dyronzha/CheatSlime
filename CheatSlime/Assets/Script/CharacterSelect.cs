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
    int[] whichCollider = new int[4];
    int[] whichDis = new int[4];

    Image[] characterDis = new Image[4];
    Collider2D[] characterCollider = new Collider2D[4];



    // Start is called before the first frame update
    private void Awake()
    {
        GameManager.Instance.PlayerName = new string[4] { "aaa", "bbb", "yee", "ccc" };
        for (int i = 0; i < 4; i++)
        {
            playerIcon[i] = transform.Find("PlayerIcon").GetChild(i).GetComponent<RectTransform>(); ;
            characterDis[i] = transform.GetChild(i).Find("Mask").GetComponent<Image>();
            characterCollider[i] = transform.GetChild(i).GetComponent<Collider2D>();
            transform.GetChild(i).Find("Text").GetComponent<Text>().text = GameManager.Instance.PlayerName[i];
        }
        
    }
    void Start()
    {
        minPosition = 0.5f*new Vector2(-Screen.width, -Screen.height) + new Vector2(30,30);
        maxPosition = 0.5f*new Vector2(Screen.width, Screen.height) - new Vector2(30, 30); ;
        Debug.Log(Screen.width);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 4; i++)
        {
            if (hasChoosen[i]) continue;

            Vector2 dir = new Vector2(Input.GetAxis(playerInput[i] + "Horizontal"), Input.GetAxis(playerInput[i] + "Vertical")).normalized;
            Vector2 nextPos = playerIcon[i].anchoredPosition + speed * dir * Time.deltaTime;
            if (nextPos.x > minPosition.x && nextPos.x < maxPosition.x && nextPos.y > minPosition.y && nextPos.y < maxPosition.y) {
                playerIcon[i].anchoredPosition = nextPos;
            }

            if (Input.GetButtonDown(playerInput[i] + "Attack"))
            {
                Debug.Log("attack" + playerIcon[i].transform.position);
                RaycastHit2D hit = Physics2D.Raycast(playerIcon[i].transform.position, Vector2.zero, 1 << LayerMask.NameToLayer("RoleSelect"));

                if (hit.collider != null)
                {
                    Debug.Log("hit something");
                    if (hit.transform.name.CompareTo("1") == 0) whichDis[i] = whichCollider[i] = 0;
                    else if (hit.transform.name.CompareTo("2") == 0) whichDis[i] = whichCollider[i] = 1;
                    else if (hit.transform.name.CompareTo("3") == 0) whichDis[i] = whichCollider[i] = 2;
                    else if (hit.transform.name.CompareTo("4") == 0) whichDis[i] = whichCollider[i] = 3;
                    hasChoosen[i] = true;
                    characterCollider[whichCollider[i]].enabled = false;
                    characterDis[whichDis[i]].enabled = true;
                    GameManager.Instance.PlayerInput[whichCollider[i]] = playerInput[i];
                    readyNum++;
                }
            }
            else if (Input.GetButtonDown(playerInput[i] + "Cancle")) {
                hasChoosen[i] = false;
                characterCollider[whichCollider[i]].enabled = true;
                characterDis[whichDis[i]].enabled = false;
                readyNum--;
            }

        }
        if (readyNum >= 4) ;
    }
}

