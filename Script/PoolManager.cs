using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    //variable 1 = list 1 variable 2 = list 2 ...

    //variable
    public GameObject[] prefabs;

    //list
    List<GameObject>[] pools;

    void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        for (int index = 0; index < pools.Length; index++)
        {
            pools[index] = new List<GameObject>();
        }
    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        // selected pools access (unActive)gameobject
        foreach(GameObject item in pools[index])
        {
            //if find input select 
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }
        //else if not find
        if (select == null)
        {
            //new.. input select
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }
        return select;
    }
}
