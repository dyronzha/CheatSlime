using System.Collections;
using System.Collections.Generic;

using Eccentric.Utils;

using UnityEngine;
namespace CheatSlime.Player {
    public class Attack : Component {
        [SerializeField] string horiButton = "";
        [SerializeField] string vertButton = "";
        [SerializeField] string button = "";
        [SerializeField] string animString = "";
        [SerializeField] Collider2D trigger = null;
        [SerializeField] float coolDown = 0f;
        [SerializeField] int damage = 0;
        public int Damage { get { return damage; } }
        Direction direction = Direction.LEFT;
        Timer timer;
        void Start ( ) {
            timer = new Timer (coolDown);
        }

        // Update is called once per frame
        void Update ( ) {
            if (Input.GetButtonDown (button) && timer.IsFinished) {
                //attack motion
                Parent.Am.SetTrigger (animString);
                List<Collider2D> colliders = new List<Collider2D> ( );
                trigger.OverlapCollider (new ContactFilter2D ( ), colliders);
                if (colliders.Count != 0) {
                    DetectPlayerOrEnemy (colliders);
                }
                timer.Reset ( );
            }
        }

        void DetectPlayerOrEnemy (in List<Collider2D> cols) {
            foreach (Collider2D col in cols) {
                if (col.gameObject.tag == "Player")
                    AttackPlayer (col.GetComponent<Player> ( ));
                else if (col.gameObject.tag == "Enemy")
                    AttackEnemy ( );
            }

        }
        void AttackPlayer (Player player) {
            player.TakeDamage (damage);
        }
        void AttackEnemy ( ) { }
        void AttackDirection ( ) {
            if (Input.GetButton (vertButton)) {
                direction = Input.GetAxis (vertButton) == 1 ? Direction.UP : Direction.DOWN;
            }
            else if (Input.GetButton (horiButton)) {
                direction = Input.GetAxis (horiButton) == 1 ? Direction.LEFT : Direction.RIGHT;
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
