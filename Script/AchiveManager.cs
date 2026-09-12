using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchiveManager : MonoBehaviour
{
    public GameObject[] lockCharacter;
    public GameObject[] unLockCharacter;
    public GameObject uiNotice;

    enum Achive { UnlockFamer2, UnlockFamer3, }
    Achive[] achives;

    WaitForSecondsRealtime wait;

    void Awake()
    {
        achives = (Achive[])Enum.GetValues(typeof(Achive));
        wait = new WaitForSecondsRealtime(5);

        if (!PlayerPrefs.HasKey("MyData"))
        {
            Init();
        }

    }

    void Init()
    {
        PlayerPrefs.SetInt("MyData", 1);

        foreach (Achive achive in achives)
        {
            PlayerPrefs.SetInt(achive.ToString(), 0);
        }
    }

    void Start()
    {
        UnLockCharacter();
    }

    void UnLockCharacter()
    {
        for (int index = 0; index < lockCharacter.Length; index++)
        {
            string achiveName = achives[index].ToString();
            bool isUnLock = PlayerPrefs.GetInt(achiveName) == 1;
            lockCharacter[index].SetActive(!isUnLock);
            unLockCharacter[index].SetActive(isUnLock);
        }
    }

    void LateUpdate()
    {
        foreach (Achive achive in achives)
        {
            CheckAchive(achive);
        }
    }

    void CheckAchive(Achive achive)
    {
        bool isAchive = false;

        switch (achive)
        {
            case Achive.UnlockFamer2:
                if (GameManager.instance.isLive)
                    isAchive = GameManager.instance.kill >= 5;
                break;
            case Achive.UnlockFamer3:
                isAchive = GameManager.instance.gameTime == GameManager.instance.maxGameTime;
                break;
        }

        if (isAchive && PlayerPrefs.GetInt(achive.ToString()) == 0)//UnLock
        {
            PlayerPrefs.SetInt(achive.ToString(), 1);//Set Lock

            for (int index = 0; index < uiNotice.transform.childCount; index++)
            {
                bool isActive = index == (int)achive;
                uiNotice.transform.GetChild(index).gameObject.SetActive(isActive);//자동완성과 비슷해보이는 이름때문에 오류가 발생했었음
            }

            StartCoroutine(NoticeRoutine());
        }
    }

    IEnumerator NoticeRoutine()
    {
        uiNotice.SetActive(true);
        AudioManager.instance.PlaySFX(AudioManager.SFX.LevelUp);

        yield return wait;

        uiNotice.SetActive(false);
    }
}
