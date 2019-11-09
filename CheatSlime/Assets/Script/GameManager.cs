using Eccentric.Utils;

using UnityEngine;
public class GameManager : TSingletonMonoBehavior<GameManager> {



    string[] playerInput = new string[4];
    public string[] PlayerInput {
        set { playerInput = value; }
        get { return playerInput; }
    }

    string[] playerName = new string[4];
    public string[] PlayerName {
        set {playerName = value;}
        get {return playerName;}
    }

    protected override void Awake()
    {
        base.Awake();
    }

    public void GameEnd(int winner){
        
    }

}
