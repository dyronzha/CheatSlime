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

        string playerInput = string.Empty;
        public string PlayerInput { get { return playerInput; } set { playerInput = value; } }

        void Start ( ) { }

        // Update is called once per frame
        void Update ( ) {
            Move ( );
            Render ( );
        }

        void Move ( ) {
            horiMove = Input.GetAxisRaw (horiString);
            vertMove = Input.GetAxisRaw (vertString);
            if (horiMove != 0f)
                IsFacingRight = horiMove > 0f ? true : false;
            Vector2 direction = new Vector2 (horiMove, vertMove) * moveSpeed;
            Parent.Tf.Translate (direction * Time.deltaTime);
        }

        void Render ( ) {
            Parent.Am.SetFloat (animHoriString, Mathf.Abs (horiMove)+0.01f);
            Rend.ChangeDirection (IsFacingRight, Parent.Tf, bRenderInvert);
        }
    }
}
