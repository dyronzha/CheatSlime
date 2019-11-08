using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CheatSlime.Player {
    public class Movement : Component {
        [SerializeField] string horiString = "";
        [SerializeField] string vertString = "";
        [SerializeField] string animHoriString = "";
        [SerializeField] [Range (0, 50)] float moveSpeed = 0f;
        [SerializeField] [Tooltip ("Invert Render Direction")] bool bRenderInvert = false;
        float horiMove = 0;
        float vertMove = 0;
        public bool IsFacingRight { get; protected set; }

        void Start ( ) {
            Parent.Sr.flipX = bRenderInvert;
        }

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
            Vector3 direction = new Vector3 (horiMove, vertMove, 0f) * moveSpeed;
            Parent.Tf.Translate (direction * Time.deltaTime);
        }

        void Render ( ) {
            Parent.Am.SetFloat (animHoriString, Mathf.Abs (horiMove));
            Parent.Sr.flipX = bRenderInvert?!IsFacingRight : IsFacingRight;
        }
    }
}
