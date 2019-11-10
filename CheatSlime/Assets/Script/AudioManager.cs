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


    // Start is called before the first frame update
    private void Awake()
    {
        audioManager = this;
        BGMAudio = transform.Find("BGMAudio").GetComponent<AudioSource>();
        effectAudio = transform.Find("EffectAudio").GetComponent<AudioSource>();
        effectAudioDic = new Dictionary<string, AudioClip>();


        for (int i = 0; i < effectClips.Length; i++) {
            effectAudioDic.Add(effectClips[i].name, effectClips[i].clip);
            Debug.Log(effectAudioDic[effectClips[i].name]);
        }

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayEffectAudio(string name, float value) {
        if (effectAudioDic.ContainsKey(name))
        {
            effectAudio.PlayOneShot(effectAudioDic[name], value);
        }
        else Debug.Log("沒有 " + name + " 這個音效");
    }

}

[System.Serializable]
public class EffectAudioClip
{
    public string name;
    public AudioClip clip;
}
