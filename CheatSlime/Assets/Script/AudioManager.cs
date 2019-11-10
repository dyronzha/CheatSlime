using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    static AudioManager audioManager;
    public static AudioManager S_AudioManager {
        get { return audioManager; }
    }

    AudioSource BGMAudio, effectAudio;


    public EffectAudioClip[] effectClips;
    Dictionary<string, AudioClip> effectAudioDic;
    Dictionary<string, float> effectAudioTime;
    Dictionary<string, int> effectAudioPlayNum;

    List<string> effectAudioTooMuch = new List<string>();


    // Start is called before the first frame update
    private void Awake()
    {
        audioManager = this;
        BGMAudio = transform.Find("BGMAudio").GetComponent<AudioSource>();
        effectAudio = transform.Find("EffectAudio").GetComponent<AudioSource>();
        effectAudioDic = new Dictionary<string, AudioClip>();
        effectAudioTime = new Dictionary<string, float>();
        effectAudioPlayNum = new Dictionary<string, int>();


        for (int i = 0; i < effectClips.Length; i++) {
            effectAudioDic.Add(effectClips[i].name, effectClips[i].clip);
            effectAudioTime.Add(effectClips[i].name, .0f);
            effectAudioPlayNum.Add(effectClips[i].name, 0);
            Debug.Log(effectAudioDic[effectClips[i].name]);
        }
        //effectAudioTime = new float[effectClips.Length];

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = effectAudioTooMuch.Count; i > 0; i--) {
            effectAudioTime[effectAudioTooMuch[i-1]] += Time.deltaTime;

            if (effectAudioTime[effectAudioTooMuch[i - 1]] > 0.5f) {
                effectAudioTime[effectAudioTooMuch[i - 1]] = .0f;
                effectAudioPlayNum[effectAudioTooMuch[i - 1]] = 0;
                //Debug.Log("解鎖 " + effectAudioTooMuch[i - 1] + "  第" + (i-1) + "個");
                effectAudioTooMuch.Remove(effectAudioTooMuch[i - 1]);
                
            }

            //if (effectAudioTime[effectAudioTooMuch[i - 1]] > 1.0f)
            //{
            //    effectAudioTime[effectAudioTooMuch[i - 1]] = .0f;
            //    Debug.Log("解鎖 " + effectAudioTooMuch[i - 1]);
            //    effectAudioTooMuch.Remove(effectAudioTooMuch[i - 1]);

            //}
        }
    }

    public void PlayEffectAudio(string name, float value) {
        if (effectAudioDic.ContainsKey(name))
        {
            if (effectAudioPlayNum[name] == 0) effectAudioTooMuch.Add(name);
            effectAudioPlayNum[name]++;
            if (effectAudioPlayNum[name] <= 5) {
                effectAudio.PlayOneShot(effectAudioDic[name], value);
            }
            //else Debug.Log("連續重複 " + name + " 五次");
            
            //if (effectAudioTooMuch.Contains(name)) return;
            //if (effectAudioPlayNum[name] >= 5 && effectAudioTime[name] < 0.5f)
            //{
            //    effectAudioTooMuch.Add(name);
            //    effectAudioPlayNum[name] = 0;
            //    Debug.Log("連續重複 " + name + " 五次");
            //}
        }
        else Debug.Log("沒有 " + name + " 這個音效");
    }
    public void StopAllEffectAudio() {
        effectAudio.Stop();
    }
    public void PauseAllEffectAudio(bool p) {
        if (p) effectAudio.Pause();
        else {
            effectAudio.Play();
        } 
    }
    public void PauseAllEffectAudio(bool p, string name)
    {
        if (p) effectAudio.Stop();
        else
        {
            effectAudio.clip = effectAudioDic[name];
            effectAudio.Play();
        }
    }
    public void ChangeBGM(string name) {
        if (effectAudioDic.ContainsKey(name)) {
            BGMAudio.clip = effectAudioDic[name];
            BGMAudio.Play();
        }
    }
}

[System.Serializable]
public class EffectAudioClip
{
    public string name;
    public AudioClip clip;
}
