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

        Vector3 playerPosition = GameManager.gameManagerInstance.player.transform.position;
        Vector3 myPosition = transform.position;
        float differentX = Mathf.Abs(playerPosition.x - myPosition.x);
        float differentY = Mathf.Abs(playerPosition.y - myPosition.y);

        Vector3 playerDirection = GameManager.gameManagerInstance.player.player_InputVector;
        float directionX = playerDirection.x < 0 ? -1 : 1;
        float directionY = playerDirection.y < 0 ? -1 : 1;

        switch(transform.tag)
        {
            case "Ground":
                if(differentX > differentY)
                {
                    transform.Translate(Vector3.right * directionX * 40);//40->request fix naming
                }
                else if (differentX < differentY)
                {
                    transform.Translate(Vector3.up * directionY * 40);//40->request fix naming
                }
                    break;
            case "Enemy":
                if(_collider.enabled)
                {
                    transform.Translate(playerDirection * 20 + new Vector3(Random.Range(-3f,3f), Random.Range(-3f, 3f), 0f));
                }
                break;

        }
    }
}
