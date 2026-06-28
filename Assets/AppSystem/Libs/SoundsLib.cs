using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Globals;

public class SoundsLib : MonoBehaviour
{

    [SerializeField] public SoundData[] datas;

    public void Init()
    {
        foreach (var data in datas)
        {
            audioDatas.Add(data.type, data.clip);
        }
    }
    public Dictionary<AudiolClipType, AudioClip> AudioDatas
    {
        get { return audioDatas; }
    }

    public AudioClip GetAutioClip(AudiolClipType type)
    {
        return audioDatas[type];
    }

    [System.Serializable]
    public class SoundData
    {
        public AudiolClipType type;
        public AudioClip clip;

    }

    private Dictionary<AudiolClipType, AudioClip> audioDatas = new Dictionary<AudiolClipType, AudioClip>();
}
