using System.Collections;
using System.Collections.Generic;

using Eccentric.Utils;

using UnityEngine;
namespace CheatSlime.Player {
    public class Send : Component {
        [SerializeField] float sendCd = 0f;
        Timer timer = null;
        void Start ( ) {
            timer = new Timer (sendCd);
        }

        // Update is called once per frame
        void Update ( ) {
            if (timer.IsFinished) {
                //send message
                timer.Reset ( );
            }
        }

    }
}
