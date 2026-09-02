using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;

    float timer;

    Player player;

    void Awake()
    {
        player = GameManager.instance.player;
    }

    void Update()
    {
        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;
            default:
                timer += Time.deltaTime;
                
                if(timer>speed)
                {
                    timer = 0f;
                    Fire();
                }
                break;
        }

        // test
        if (Input.GetButtonDown("Jump"))
        {
            LevelUp(10, 1);
        }
    }

    public void Init(ItemData data)
    {
        //Basic Set
        name = "Weapon " + data.itemID;
        transform.parent = player.transform;//자식오브젝트로 등록
        transform.localPosition = Vector3.zero;

        //Property Set
        id = data.itemID;
        damage = data.baseDamage;
        count = data.baseCount;

        for (int index = 0; index < GameManager.instance.pool.prefabs.Length; index++)
        {
            if(data.projectile == GameManager.instance.pool.prefabs[index])
            {
                prefabId = index;//프리팹 아이디는 풀인 매니저 변수에서 찾아와서 초기화
                break;
            }
        }

        switch (id)
        {
            case 0:
                speed = 150;
                Batch();
                break;
            default:
                speed = 0.3f;
                break;
        }

        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }

    public void LevelUp(float damage, int count)
    {
        this.damage = damage;
        this.count += count;

        if (id == 0)
            Batch();

        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);//어플라이 기어가 발생하는 타이밍을 정리해두면 좋을듯
    }

    void Batch() // request fix naming
    {
        for (int index = 0; index < count; index++)
        {
            Transform bullet;

            if (index < transform.childCount)//childcount check
            {
                bullet = transform.GetChild(index);//recycle
            }
            else
            {
                bullet = GameManager.instance.pool.Get(prefabId).transform;
                bullet.parent = transform;//transform... change To parents
            }

            bullet.localPosition = Vector3.zero; // playertransform->child->zero
            bullet.localRotation = Quaternion.identity;

            Vector3 rotationVector = Vector3.forward * 360 * index / count;
            bullet.Rotate(rotationVector);
            bullet.Translate(bullet.up * 1.5f, Space.World);

            bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero); // -1 is Infinity Per.
        }
    }

    void Fire()
    {
        if (!player.scanner.nearestTarget)
            return;

        Vector3 targetPosition = player.scanner.nearestTarget.position;
        Vector3 dirrection = targetPosition - transform.position;
        dirrection = dirrection.normalized;

        Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dirrection);
        bullet.GetComponent<Bullet>().Init(damage, count, dirrection);

    }
}
