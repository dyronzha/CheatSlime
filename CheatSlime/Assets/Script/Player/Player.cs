using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CheatSlime.Player {
    public class Player : MonoBehaviour {
        [SerializeField] int id = 99;
        [SerializeField] new string name = "";
        Attack atkComponent = null;
        Movement moveComponent = null;
        [SerializeField] int health = 0;
        [SerializeField] int armor = 0;
        public int Health { get { return health; } }
        public int Damage { get { return atkComponent.Damage; } }
        public int Armor { get { return armor; } }
        public string Name { get { return name; } set { name = value; } }
        public Transform Tf { get; protected set; }
        public SpriteRenderer Sr { get; protected set; }
        public Animator Am { get; protected set; }
        public Movement MoveComponent { get { return moveComponent; } }

        // Start is called before the first frame update
        void Awake ( ) {
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
            int rand=Random.Range(0,2);
            if(rand==0)Am.SetTrigger("TakeDamage");
            int difference = damage - armor;
            health -= difference > 0 ? difference : 0;
            if (health <= 0) Dead ( );
        }

        void Dead ( ) {
            Am.SetTrigger("Dead");
            Debug.Log (name + " is Dead");
        }
    }
}
