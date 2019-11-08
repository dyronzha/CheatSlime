using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CheatSlime.Player {
    public class Component : MonoBehaviour {
        protected Player Parent = null;

        protected virtual void Awake ( ) {
            Parent = GetComponent<Player> ( );
        }

    }
}
