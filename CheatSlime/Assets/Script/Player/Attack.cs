using System.Collections;
using System.Collections.Generic;

using Eccentric.Utils;
using E = CheatSlime.Enemy;
using UnityEngine;
namespace CheatSlime.Player {
    public class Attack : Component {
        public string horiButton = "";
        public string vertButton = "";
        public string button = "";
        [SerializeField] string tAnimString = "";
        [SerializeField] string dAnimString = "";
        [SerializeField] string horiAnimString = "";
        [SerializeField] Collider2D lrTrigger = null;
        [SerializeField] Collider2D tTrigger = null;
        [SerializeField] Collider2D dTrigger = null;
        [SerializeField] float coolDown = 0f;
        [SerializeField] int damage = 0;
        public int Damage { get { return damage; } set { damage = value; } }
        Direction direction = Direction.LEFT;
        Timer timer = null;
        void Start ( ) {
            timer = new Timer (coolDown);
        }

        // Update is called once per frame
        void Update ( ) {
            if (Input.GetButtonDown (button) && timer.IsFinished) {
                //attack motion
                List<Collider2D> colliders = new List<Collider2D> ( );
                AttackDirection (ref colliders);
                if (colliders.Count != 0) {
                    DetectPlayerOrEnemy (colliders);
                }
                timer.Reset ( );
            }
        }

        void DetectPlayerOrEnemy (in List<Collider2D> cols) {
            foreach (Collider2D col in cols) {
                if (col.gameObject.tag == "Player") {
                    Debug.Log ("Attacking Player");
                    AttackPlayer (col.GetComponent<Player> ( ));
                }
                else if (col.gameObject.tag == "Enemy")
                    AttackEnemy (col.GetComponent<E.Enemy> ( ));
            }

        }
        void AttackPlayer (Player player) {
            player.TakeDamage (damage);
        }
        void AttackEnemy (E.Enemy enemy) {
            enemy.TakeDamage (this.Parent);
        }
        void AttackDirection (ref List<Collider2D> cols) {
            if (Input.GetButton (vertButton)) {
                direction = Input.GetAxisRaw (vertButton) >= 0 ? Direction.DOWN : Direction.UP;
            }
            else if (Input.GetButton (horiButton)) {
                direction = Input.GetAxisRaw (horiButton) >= 0 ? Direction.RIGHT : Direction.LEFT;
            }
            Debug.Log ("Attack Direction   " + direction);
            switch (direction) {
                case Direction.UP:
                    tTrigger.OverlapCollider (new ContactFilter2D ( ), cols);
                    Parent.Am.SetTrigger (tAnimString);
                    break;
                case Direction.DOWN:
                    dTrigger.OverlapCollider (new ContactFilter2D ( ), cols);
                    Parent.Am.SetTrigger (dAnimString);
                    break;
                case Direction.LEFT:
                    lrTrigger.OverlapCollider (new ContactFilter2D ( ), cols);
                    Parent.Am.SetTrigger (horiAnimString);
                    break;
                case Direction.RIGHT:
                    lrTrigger.OverlapCollider (new ContactFilter2D ( ), cols);
                    Parent.Am.SetTrigger (horiAnimString);
                    break;
            }

        }

        enum Direction {
            LEFT,
            RIGHT,
            UP,
            DOWN
        }
    }
}
