using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CheatSlime.Player {
    public class Player : MonoBehaviour {
        [SerializeField] new string name = "";
        Attack atkComponent = null;
        Movement moveComponent = null;
        [SerializeField]int health = 0;
        [SerializeField]int armor = 0;
        public int Health { get { return health; } }
        public int Damage { get { return atkComponent.Damage; } }
        public int Armor { get { return armor; } }
        public string Name { get { return name; } }
        public Transform Tf { get; protected set; }
        public SpriteRenderer Sr { get; protected set; }
        public Animator Am { get; protected set; }
        // Start is called before the first frame update
        void Start ( ) {
            Tf = transform;
            Sr = GetComponent<SpriteRenderer> ( );
            Am = GetComponent<Animator> ( );
            atkComponent = GetComponent<Attack> ( );
            moveComponent = GetComponent<Movement> ( );
        }

        // Update is called once per frame
        void Update ( ) {

        }
        public void TakeDamage (int damage) {
            int difference = damage - armor;
            health -= difference > 0 ? difference : 0;
            if (health <= 0) Dead ( );
        }

        void Dead ( ) {
            Debug.Log (name + " is Dead");
        }
    }
}
