using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition_TileMap : MonoBehaviour
{
    Collider2D _collider;
    void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))
            return;

        Vector3 playerPosition = GameManager.instance.player.transform.position;
        Vector3 myPosition = transform.position;


        switch (transform.tag)
        {
            case "Ground":
                //Difference in position between two objects
                float differentX = playerPosition.x - myPosition.x;
                float differentY = playerPosition.y - myPosition.y;
                float directionX = differentX < 0 ? -1 : 1;
                float directionY = differentY < 0 ? -1 : 1;
                differentX = Mathf.Abs(differentX);
                differentY = Mathf.Abs(differentY);

                if (differentX > differentY)
                {
                    transform.Translate(Vector3.right * directionX * 40);//40->request fix naming
                }
                else if (differentX < differentY)
                {
                    transform.Translate(Vector3.up * directionY * 40);//40->request fix naming
                }
                break;
            case "Enemy":
                if (_collider.enabled)
                {
                    Vector3 distance = playerPosition - myPosition;
                    Vector3 random = new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), 0);
                    transform.Translate(random + distance * 2);
                }
                break;

        }
    }
}
