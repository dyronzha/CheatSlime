using System.Collections;
using System.Collections.Generic;

using Eccentric.Utils;
using Rend = Eccentric.Utils.Render;
using UnityEngine;
namespace CheatSlime.Player {
    public class Movement : Component {
        [SerializeField] string horiString = "";
        [SerializeField] string vertString = "";
        [SerializeField] string animHoriString = "";
        [SerializeField] [Range (0, 10)] float moveSpeed = 0f;
        [SerializeField] [Tooltip ("Invert Render Direction")] bool bRenderInvert = false;
        float horiMove = 0;
        float vertMove = 0;
        public bool IsFacingRight { get; protected set; }
        public string HoriString { set { horiString = value; } }
        public string VertString { set { vertString = value; } }

        //string playerInput = string.Empty;
        //public string PlayerInput { get { return playerInput; } set { playerInput = value; Debug.Log (value); } }

        void Start ( ) { }

        // Update is called once per frame
        void Update ( ) {
            Move ( );
            Render ( );
        }

        void Move ( ) {
            horiMove = Input.GetAxisRaw (horiString);
            vertMove = Input.GetAxisRaw (vertString);

            bool moveX = false, moveY = false;
            float moveLength = Time.deltaTime * moveSpeed;
            float detectLength = moveLength * 1.2f;
            Vector3 curPos = Parent.Tf.position;
            Vector3 detectX = new Vector3(horiMove, 0, 0);
            Vector3 detectY = new Vector3(0, vertMove, 0);
            Vector3 nextPos = curPos;
            Vector3 detectPos = curPos + new Vector3(0, -0.2f, 0);

            if (!Physics2D.Raycast(detectPos, detectX, detectLength, 1 << LayerMask.NameToLayer("Ground"))) {
                moveX = true;
                nextPos += detectX * moveLength; 
            }
            if (!Physics2D.Raycast(detectPos, detectY, moveLength * (vertMove<.0f?2.4f:1.2f), 1 << LayerMask.NameToLayer("Ground"))) {
                moveY = true;
                nextPos += detectY * moveLength;
            }
            if (moveX && moveY)
            {
                if (!Physics2D.Raycast(detectPos, new Vector2(horiMove, vertMove), detectLength, 1 << LayerMask.NameToLayer("Ground")))
                {
                    Parent.Tf.position = nextPos;
                }
            }
            else {
                Parent.Tf.position = nextPos;
            }


            if (horiMove != 0f)
                IsFacingRight = horiMove > 0f ? true : false;
            //Vector2 direction = new Vector2 (horiMove, vertMove) * moveSpeed;
            //Parent.Tf.Translate (direction * Time.deltaTime);
        }

        void Render ( ) {
            Parent.Am.SetFloat (animHoriString, Mathf.Abs (horiMove) + 0.01f);
            Rend.ChangeDirection (IsFacingRight, Parent.Tf, bRenderInvert);
        }

    }
}
