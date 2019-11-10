using System.Collections;
using System.Collections.Generic;

using CheatSlime.Enemy;

using UnityEngine;

namespace CheatSlime.Player {
    public enum PlayerAvatar {
        Pumpkin,
        Muscle,
        Magic,
        Chucky
    }
    public class PlayerManager : MonoBehaviour {
        [SerializeField] List<GameObject> slimePrefab = new List<GameObject> ( );
        [SerializeField] float maxNum = 0;
        [SerializeField] Transform slimeSpawnPos;
        [SerializeField] Vector2 slimeLvRange = Vector2.zero;
        [SerializeField] Vector2 playerLvRange = Vector2.zero;
        [SerializeField] Transform slimeParent = null;
        List<Enemy.Enemy> slimes = new List<Enemy.Enemy> ( );
        Player [ ] players = new Player [4];
        List<int> avatarID = new List<int> ( );

        int perSpawnNum;
        float spawnTime = .0f, spawnRandomTime = .0f;
        Vector3 [ ] spawnPos;

        // Start is called before the first frame update
        private void Awake ( ) {

            spawnPos = new Vector3 [slimeSpawnPos.childCount];
            for (int i = 0; i < spawnPos.Length; i++) {
                spawnPos [i] = slimeSpawnPos.GetChild (i).position;
            }
            for (int i = 0; i < maxNum; i++) {
                 Debug.Log(i+" "+maxNum);
                SpawnSlime ( );
            }
        }

        void Start ( ) {
            for (int i = 0; i < players.Length; i++) {
                players [i] = transform.GetChild (i).GetComponent<Player> ( );
                SetPlayer (i);
                avatarID.Add (i);
            }
            //Debug.Log(Mathf.Log(0f, 0.5f));
            //Debug.Log(Mathf.Log(0.01f, 0.5f));
            //Debug.Log(Mathf.Log(0.1f, 0.5f));
            //Debug.Log(Mathf.Log(0.5f,0.5f));
            //Debug.Log(Mathf.Log(1.0f, 0.5f));
        }

        // Update is called once per frame
        void Update ( ) {

            if (slimes.Count < maxNum && spawnTime > spawnRandomTime) {
                spawnTime = .0f;
                spawnRandomTime = Random.Range (1.0f, 5.0f);
                if (Random.Range (0, 100) > 30) {
                    int count = 0;
                    int randNum = Random.Range (3, 8);
                    while (count < randNum) {
                        count++;
                        SpawnSlime ( );
                    }
                }
            }
            spawnTime += Time.deltaTime;

        }

        public void SetPlayer (int avatarID) {
            players [avatarID].SetInput (GameManager.Instance.PlayerInput [avatarID], (int) playerLvRange.x, (int) playerLvRange.y);
            players [avatarID].Name = GameManager.Instance.PlayerName [avatarID];
            players [avatarID].Pm = this;
            players [avatarID].Id = avatarID;
            //ChangePlayerInfo.s_ChangePlayerInfo.UpLoadPlayerInfo (players [avatarID].Name, 0, players [avatarID].Health.ToString ( ));
            //ChangePlayerInfo.s_ChangePlayerInfo.UpLoadPlayerInfo (players [avatarID].Name, 1, players [avatarID].Damage.ToString ( ));
            //ChangePlayerInfo.s_ChangePlayerInfo.UpLoadPlayerInfo (players [avatarID].Name, 2, players [avatarID].Armor.ToString ( ));
        }

        public void PlayerDead (int avatarID) {
            this.avatarID.Remove (avatarID);
            if (this.avatarID.Count == 1) {
                GameManager.Instance.GameEnd (this.avatarID [0]);
            }
        }
        void SpawnSlime ( ) {
            Vector3 pos = spawnPos [Random.Range (0, spawnPos.Length)];
            int type = Random.Range (0, 3);
            int lv = Mathf.CeilToInt (Random.Range (slimeLvRange.x, slimeLvRange.y));
            GameObject spawn = Instantiate (slimePrefab [type], pos, Quaternion.identity, slimeParent);
            Enemy.Enemy slimeObj = spawn.GetComponent<Enemy.Enemy> ( );
            slimeObj.pm = this;
            slimes.Add (slimeObj);
            slimeObj.SetEnemy (lv);
        }
        public void SlimeDead (Enemy.Enemy enemy) {
            slimes.Remove (enemy);
        }

    }
}
