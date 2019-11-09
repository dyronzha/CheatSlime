using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CheatSlime.Player {
    public enum PlayerAvatar { 
        Pumpkin, Muscle, Magic, Chucky
    }
    public class PlayerManager : MonoBehaviour
    {

        Player[] players = new Player[4];
        

        // Start is called before the first frame update
        private void Awake()
        {
            for (int i = 0; i < players.Length; i++)
            {
                players[i] = transform.GetChild(i).GetComponent<Player>();
                SetPlayer(i);
            }
        }

        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetPlayer(int avatarID) {
            players[avatarID].MoveComponent.PlayerInput = GameManager.Instance.PlayerInput[avatarID];
            players[avatarID].Name = GameManager.Instance.PlayerName[avatarID];
        }

    }
}


