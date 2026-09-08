using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    RectTransform rect;
    Item[] items;
    void Awake()
    {
        rect = GetComponent<RectTransform>();
        items = GetComponentsInChildren<Item>(true);
    }

    public void Show()
    {
        Next();
        rect.localScale = Vector3.one;
        GameManager.instance.Stop();
    }

    public void Hide()
    {
        rect.localScale = Vector3.zero;
        GameManager.instance.Resume();
    }

    public void Select(int index)
    {
        items[index].OnClick();
    }

    void Next()
    {
        // 1. All Item unActive
        foreach (Item item in items)
        {
            item.gameObject.SetActive(false);
        }

        // 2. Random Item 3 Acitive
        int[] random = new int[3];
        while(true)//<<abunai
        {
            random[0] = Random.Range(0, items.Length);
            random[1] = Random.Range(0, items.Length);
            random[2] = Random.Range(0, items.Length);
            if (random[0] != random[1] && random[1] != random[2] && random[0] != random[2])
                break;
        }

     
        for(int index=0; index < random.Length; index++)
        {
            Item randomItem = items[random[index]];

            // 3. Full Level Item X -> useItem
            if(randomItem.level == randomItem.data.damages.Length)
            {
                items[4].gameObject.SetActive(true);
                //items[andom.Range(4, 7)].gameObject.SetActive(true);
            }
            else
                randomItem.gameObject.SetActive(true);
        }

        }
    }
