using Eccentric.Utils;

using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : TSingletonMonoBehavior<GameManager> {

    public int winnerID { get; set; }

    string [ ] playerInput = new string[4] { "p1","p2","p3","p4"};
    public string [ ] PlayerInput {
        set { playerInput = value; }
        get { return playerInput; }
    }

    string [ ] playerName = new string[4] { "aa", "bb", "cc", "dd"};
    public string [ ] PlayerName {
        set { playerName = value; }
        get { return playerName; }
    }

    protected override void Awake ( ) {
        base.Awake ( );
    }

    public void GameEnd (int winner) {
        winnerID = winner;
        SceneManager.LoadScene (4);
    }

}
