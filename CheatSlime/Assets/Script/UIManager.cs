using System.Collections;
using System.Collections.Generic;

using CheatSlime.Player;

using Eccentric.Utils;

using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour {

    [SerializeField] Sliders p1UI;
    [SerializeField] Sliders p2UI;
    [SerializeField] Sliders p3UI;
    [SerializeField] Sliders p4UI;
    [SerializeField]float timeSection = 0f;
    Timer timer;
    // Start is called before the first frame update
    void Start ( ) {
        timer = new Timer (timeSection);
    }

    // Update is called once per frame
    void Update ( ) {
        if (timer.IsFinished) {
            UpdateUI (p1UI);
            UpdateUI (p2UI);
            UpdateUI (p3UI);
            UpdateUI (p4UI);
            timer.Reset();
        }
    }

    void UpdateUI (Sliders sliders) {
        int [ ] index = { 0, 1, 2 };
        List<int> tmp = new List<int> ( );
        tmp.AddRange (index);
        tmp.AddRange (ShuffleNum (in tmp));
        List<int> value = new List<int> ( );
        value.Add (sliders.player.Health);
        value.Add (sliders.player.Damage);
        value.Add (sliders.player.Armor);
        sliders.slider1.value = value [tmp [0]];
        sliders.slider2.value = value [tmp [1]];
        sliders.slider3.value = value [tmp [2]];
    }

    List<int> ShuffleNum (in List<int> numbers) {
        List<int> result = new List<int> ( );
        result.AddRange (numbers);
        for (int i = 0; i < numbers.Count; i++) {
            int sNum = Random.Range (0, result.Count);
            int tmp = result [i];
            result [i] = result [sNum];
            result [sNum] = tmp;
        }
        return result;
    }

    [System.Serializable]
    class Sliders {
        public Slider slider1;
        public Slider slider2;
        public Slider slider3;
        public Player player;
    }
}
