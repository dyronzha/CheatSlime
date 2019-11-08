using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CheatSlime.Player {
    public class Player : MonoBehaviour {
        public Transform Tf { get; protected set; }
        public SpriteRenderer Sr { get; protected set; }
        public Animator Am { get; protected set; }
        // Start is called before the first frame update
        void Start ( ) {
            Tf = transform;
            Sr = GetComponent<SpriteRenderer> ( );
            Am = GetComponent<Animator> ( );
        }

        // Update is called once per frame
        void Update ( ) {

        }
    }
}
