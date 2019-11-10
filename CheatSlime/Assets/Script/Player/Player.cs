using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CheatSlime.Player {
    public class Player : MonoBehaviour {
        [SerializeField] new string name = "";
        [SerializeField] int health = 0;
        [SerializeField] int armor = 0;
        Attack atkComponent = null;
        Movement moveComponent = null;
        public int Health { get { return health; } }
        public int Damage { get { return atkComponent.Damage; } set { atkComponent.Damage = value; } }

        public int Armor { get { return armor; } }
        public string Name { get { return name; } set { name = value; } }
        public Transform Tf { get; protected set; }
        public Animator Am { get; protected set; }
        public string PlayerInput { get; set; }
        public PlayerManager Pm { get; set; }
        public int Id { get; set; }
        public bool IsFacingRight { get { return moveComponent.IsFacingRight; } }

        // Start is called before the first frame update
        void Awake ( ) {
            Tf = transform;
            Am = GetComponent<Animator> ( );
            atkComponent = GetComponent<Attack> ( );
            moveComponent = GetComponent<Movement> ( );
        }

        // Update is called once per frame
        void Update ( ) {

        }
        public void TakeDamage (int damage) {
            int difference = damage - armor;
            health = health - (difference > 0 ? difference : 0);
            if (health <= 0) Dead ( );
            else {
                int rand = Random.Range (0, 2);
                if (rand == 0) Am.SetTrigger ("TakeDamage");
            }
            Debug.Log (PlayerInput + " TakeDamage: " + damage + " Remain health " + health);
            // ChangePlayerInfo.s_ChangePlayerInfo.UpLoadPlayerInfo (Name, 0, Health.ToString ( ));
            // ChangePlayerInfo.s_ChangePlayerInfo.UpLoadPlayerInfo (Name, 1, Damage.ToString ( ));
            // ChangePlayerInfo.s_ChangePlayerInfo.UpLoadPlayerInfo (Name, 2, Armor.ToString ( ));
        }

        void Dead ( ) {
            Am.SetTrigger ("Dead");
            moveComponent.enabled = false;
            atkComponent.enabled = false;
            Debug.Log (name + " is Dead");
        }
        void DeadAnimFin ( ) {
            Debug.Log (Id + " is dead deleting");
            Pm.PlayerDead (Id);
            this.gameObject.SetActive (false);
        }

        public void SetInput (string playerID, int minLv, int maxLv) {
            PlayerInput = playerID;
            moveComponent.HoriString = PlayerInput + "Horizontal";
            moveComponent.VertString = PlayerInput + "Vertical";
            //atkComponent.horiButton = PlayerInput + "Horizontal";
            //atkComponent.vertButton = PlayerInput + "Vertical";
            atkComponent.button = PlayerInput + "Attack";
            health = Random.Range (minLv, maxLv);
            atkComponent.Damage = Random.Range (minLv, maxLv);
            armor = Random.Range (minLv, maxLv);
        }
        public void GainExp (int exp) {
            health += exp;
            Damage += exp;
            armor += exp;

        }
    }
}
