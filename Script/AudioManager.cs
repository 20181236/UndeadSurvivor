using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;

    [Header("# BGM")]
    public AudioClip bgmClip;
    public float bgmVolume;
    AudioSource bgmPlayer;
    AudioHighPassFilter bgmEffect;


    [Header("# SFX")]
    public AudioClip[] sfxClips;
    public float sfxVolume;
    public int Channels;
    AudioSource[] sfxPlayers;
    int channelIndex;

    public enum SFX { Dead, Hit, LevelUp = 3, Lose, Melee, Range = 7, Select, Win }

    void Awake()
    {
        instance = this;
        Init();
    }

    void Init()
    {
        //BGM PLAYER INITIALIZING
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;
        bgmPlayer.clip = bgmClip;
        bgmEffect = Camera.main.GetComponent<AudioHighPassFilter>();

        //SFX PLAYER INITIALIZING
        GameObject sfxObject = new GameObject("SFXPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[Channels];

        for (int index = 0; index < sfxPlayers.Length; index++)
        {
            sfxPlayers[index] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[index].playOnAwake = false;
            sfxPlayers[index].bypassListenerEffects = true;
            sfxPlayers[index].volume = sfxVolume;
        }
    }

    public void PlayBgm(bool isPlay)
    {
        if(isPlay)
        {
            bgmPlayer.Play();
        }
        else
        {
            bgmPlayer.Stop();
        }
    }

    public void EffectBgm(bool isPlay)
    {
        bgmEffect.enabled = isPlay;
    }

    public void PlaySFX(SFX sfx)
    {
        for (int index = 0; index < sfxPlayers.Length; index++)
        {
            int loopIndex = (index + channelIndex) % sfxPlayers.Length;//채널 개수만큼 순회하도록 채널인덱스 변수 활용

            if (sfxPlayers[loopIndex].isPlaying)
                continue;

            int randomIndex = 0;
            if (sfx == SFX.Hit || sfx==SFX.Melee)
            {
                randomIndex = UnityEngine.Random.Range(0, 2);
            }

            channelIndex = loopIndex;
            sfxPlayers[loopIndex].clip = sfxClips[(int)sfx];
            sfxPlayers[loopIndex].Play();
            break;
        }
    }
}
