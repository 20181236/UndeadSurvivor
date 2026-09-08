using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public bool isLeft;
    public SpriteRenderer spriter;

    SpriteRenderer player;

    Vector3 rightPosition = new Vector3(0.35f, -0.15f, 0);
    Vector3 rightPositionRseverse = new Vector3(-0.15f, -0.15f, 0);
    Quaternion leftRotation = Quaternion.Euler(0, 0, -35);
    Quaternion leftRotationRseverse = Quaternion.Euler(0, 0, -135);

    void Awake()
    {
        player = GetComponentsInParent<SpriteRenderer>()[1];
    }

    void LateUpdate()
    {
        bool isReverse = player.flipX;

        if (isLeft)
        {
            transform.localRotation = isReverse ? leftRotationRseverse : leftRotation;
            spriter.flipY = isReverse;
            spriter.sortingOrder = isReverse ? 4 : 6;
        }
        else
        {
            transform.localPosition = isReverse ? rightPositionRseverse : rightPosition;
            spriter.flipX = isReverse;
            spriter.sortingOrder = isReverse ? 6 : 4;
        }
    }
}
