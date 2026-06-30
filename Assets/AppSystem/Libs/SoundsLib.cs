using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Globals;

public class SoundsLib : MonoBehaviour
{

    public SoundData[] datas;

    public void Init()
    {
        foreach (var data in datas)
        {
            audioDatas.Add(data.type, data.clip);
        }
    }
    public Dictionary<AudioClipType, AudioClip> AudioDatas
    {
        get { return audioDatas; }
    }

    public AudioClip GetAutioClip(AudioClipType type)
    {
        return audioDatas[type];
    }

    private Dictionary<AudioClipType, AudioClip> audioDatas = new Dictionary<AudioClipType, AudioClip>();

}

[System.Serializable]
public class SoundData
{
    public AudioClipType type;
    public AudioClip clip;
}
