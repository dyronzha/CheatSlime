using System.Collections;
using System.Collections.Generic;
using P = CheatSlime.Player;
using CheatSlime.Move;

using Eccentric.Utils;

using UnityEngine;
namespace CheatSlime.Enemy {
    public class Enemy : MonoBehaviour {
        [SerializeField] Vector2 range = Vector2.zero;
        [SerializeField] bool bRenderInvert = false;
        [SerializeField] float moveSpeed = 0f;
        [SerializeField] float expRatio = 0.1f;
        [SerializeField] Type type = 0;
        [SerializeField] int health = 0;
        [SerializeField] int armor = 0;
        [SerializeField] int damage = 0;
        RangeRandomMove movement = null;
        public int Health { get { return health; } }
        public int Armor { get { return armor; } }
        // Start is called before the first frame update
        void Start ( ) {
            movement = new RangeRandomMove (range, moveSpeed, transform.position);
        }

        // Update is called once per frame
        void Update ( ) {
            transform.position = movement.GetNextPos (transform.position);
            Render.ChangeDirection (movement.IsFacingRight, transform, bRenderInvert);
        }

        public int TakeDamage (P.Player player) {
            int exp = 0;
            int difference = 0;
            switch (type) {
                case Type.HP:
                    difference = player.Health - health;
                    break;
                case Type.ATK:
                    difference = player.Damage - damage;
                    break;
                case Type.DEF:
                    difference = player.Armor - armor;
                    break;
            }
            if (difference >= 0) {
                exp = Mathf.FloorToInt (difference * expRatio);
                Dead ( );
            }
            return exp;
        }

        void Dead ( ) {
            Debug.Log ("EnemyDead");
        }


    }
}
