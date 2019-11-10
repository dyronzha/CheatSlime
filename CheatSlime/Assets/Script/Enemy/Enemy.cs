using System.Collections;
using System.Collections.Generic;
using P = CheatSlime.Player;
using CheatSlime.Move;
using CheatSlime.Player;

using Eccentric.Utils;

using UnityEngine;
namespace CheatSlime.Enemy {
    public class Enemy : MonoBehaviour {
        public PlayerManager pm = null;
        [SerializeField] Vector2 range = Vector2.zero;
        [SerializeField] bool bRenderInvert = false;
        [SerializeField] float moveSpeed = 0f;
        [SerializeField] float expRatio = 0.1f;
        [SerializeField] Type type = 0;
        [SerializeField] int health = 0;
        [SerializeField] int armor = 0;
        [SerializeField] int damage = 0;
        [SerializeField] float rayCastDistance = 0f;
        [SerializeField] LayerMask groundLayer = 0;
        [SerializeField] TextMesh text = null;
        RangeRandomMove movement = null;
        new SpriteRenderer renderer = null;
        public int Health { get { return health; } }
        public int Armor { get { return armor; } }
        Animator anim = null;
        // Start is called before the first frame update
        void Start ( ) {
            movement = new RangeRandomMove (range, moveSpeed, transform.position);
            renderer = GetComponent<SpriteRenderer> ( );
            anim = GetComponent<Animator> ( );
        }

        // Update is called once per frame
        void Update ( ) {
            if (CheckWall ( )) movement.FindNewTargetPos ( );
            transform.position = movement.GetNextPos (transform.position);
            Render.ChangeDirectionXWithSpriteRender (movement.IsFacingRight, renderer);
        }

        bool CheckWall ( ) {
            bool bTouchWall = false;
            RaycastHit2D rResult = Physics2D.Raycast (transform.position, transform.right, rayCastDistance, groundLayer);
            RaycastHit2D lResult = Physics2D.Raycast (transform.position, -transform.right, rayCastDistance, groundLayer);
            RaycastHit2D uResult = Physics2D.Raycast (transform.position, transform.up, rayCastDistance, groundLayer);
            if (rResult.collider != null || lResult.collider != null || uResult.collider != null) bTouchWall = true;
            return bTouchWall;

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
            if (difference < 0) {
                anim.SetTrigger ("Damage");
            }
            else if (difference >= 0) {
                exp = Mathf.FloorToInt (difference * expRatio);
                Dead ( );
            }
            return exp;
        }

        void Dead ( ) {
            anim.SetTrigger ("Dead");
        }
        void DeadAnimFin ( ) {
            pm.SlimeDead (this);
            Destroy (this.gameObject);
        }

        public void SetEnemy (int lv) {
            health = lv;
            damage = lv;
            armor = lv;
            text.text = lv.ToString ( );
        }

    }
}
