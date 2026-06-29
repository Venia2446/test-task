using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Globals;

public class AudioSystem : MonoBehaviour
{
    public AudioSource audioSourceGame;

    public void Init(SoundsLib soundsLib)
    {
        Datas = soundsLib.AudioDatas;
        audioSourceGame.Play();
    }

    public void Terminate()
    {
        audioSourceGame?.Stop();
    }

    public void PlayOneShot(AudioClipType type)
    {
        audioSourceGame.PlayOneShot(Datas[type]);
    }

    private Dictionary<AudioClipType, AudioClip> Datas { set; get; }

}
