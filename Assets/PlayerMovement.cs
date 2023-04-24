using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 velocity;
    private Vector3 direction;
    public Tilemap fogTileMap;
    private bool hasMoved;

    private void Start() {
        UpdateFog();
    }

    void Update()
    {
        if(velocity.x == 0)
        {
            hasMoved = false;
        } 
        else if(velocity.x != 0 && !hasMoved)
        {
            hasMoved = true;
            MoveByDirection();
        }

        velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    private void MoveByDirection(){
        if(velocity.x < 0)//Move Left
        {
            if(velocity.y > 0) // Move upper left
            {
                direction = new Vector3(-0.5f, 0.5f);
            } 
            else if(velocity.y < 0) // Move bottom left
            {
                direction = new Vector3(-0.5f, -0.5f);
            }
            else // Move left
            {
                direction = new Vector3(-1,0);
            }
        }
        else if(velocity.x > 0)
        {
            if(velocity.y > 0) // Move upper right
            {
                direction = new Vector3(0.5f, 0.5f);
            } 
            else if(velocity.y < 0) // Move bottom right
            {
                direction = new Vector3(0.5f, -0.5f);
            }
            else // Move right
            {
                direction = new Vector3(1,0);
            }
        }

        transform.position += direction;
        UpdateFog();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        transform.position -= direction;
    }



    private void UpdateFog()
    {
        Vector3Int currentPlayerPosition = fogTileMap.WorldToCell(transform.position); //Cell position converts to world position
        for(int i = -3; i <=3; i++)
        {
            for(int j = -3; j <=3; j++)
            {
                fogTileMap.SetTile(currentPlayerPosition + new Vector3Int(i, j, 0),null);
            }
        }
    }
}
