using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
public class ResultManager : MonoBehaviour {
    [SerializeField] List<Sprite> winnerSpr = new List<Sprite> ( );
    [SerializeField] Image winnerImg = null;
    [SerializeField] Text winnerText = null;
    // Start is called before the first frame update
    void Start ( ) {
        winnerImg.sprite = winnerSpr [GameManager.Instance.winnerID];
        winnerText.text = GameManager.Instance.PlayerName[GameManager.Instance.winnerID];
    }
}
