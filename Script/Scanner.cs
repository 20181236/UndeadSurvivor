using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public float scanRange;
    public LayerMask targetLayer;
    public RaycastHit2D[] targets;
    public Transform nearestTarget;

    void FixedUpdate()
    {
        targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, targetLayer);//<why CircleCastAll, Vector2.zero
        nearestTarget = GetNearest();
    }

    Transform GetNearest()
    {
        Transform ressult = null;
        float distance = 100;

        foreach (RaycastHit2D target in targets)
        {
            Vector3 myPosition = transform.position;
            Vector3 targetPosition = target.transform.position;
            float currentDistance = Vector3.Distance(myPosition, targetPosition);

            if (currentDistance < distance)
            {
                distance = currentDistance;
                ressult = target.transform;
            }

        }

        return ressult;
    }
}
