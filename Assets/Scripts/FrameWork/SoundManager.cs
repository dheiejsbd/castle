using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;


public class SoundManager:MonoBehaviour
{
    public void Awake()
    {
        instance = this;
        Initialize();
    }

    static public SoundManager instance { private set; get; }

    public Dictionary<int, AudioSource> Loop = new Dictionary<int, AudioSource>();
    int id = 0;
    AudioSource AudioEffect;
    GameObject SoundObj;

    #region singleton
    private void Initialize()
    {
        SoundObj = new GameObject("SoundManager");

        AudioEffect = SoundObj.AddComponent<AudioSource>();
        AudioEffect.playOnAwake = false;
        AudioEffect.loop = false;
    }
    #endregion singleton

    #region Sound
    public void PlayEffect(AudioClip clip, float volume = 1)
    {
        Debug.Log("TryPlayEffect" + clip.name);
        AudioEffect.PlayOneShot(clip, volume);
    }

    public int PlayLoopEffect(AudioClip clip, float volume = 1)
    {
        id++;
        Loop.Add(id, SoundObj.AddComponent<AudioSource>());
        Loop[id].clip = clip;
        Loop[id].volume = volume;
        Loop[id].loop = true;
        Loop[id].Play();
        return id;
    }
    public void StopLoopEffect(int ID)
    {
        if (!Loop.ContainsKey(ID)) return;
        Destroy(Loop[ID]);
    }

    #endregion Sound

    public void StopAll()
    {
        AudioEffect.Stop();
        foreach (var key in Loop.Keys)
        {
            Destroy(Loop[key]);
        }
    }
}