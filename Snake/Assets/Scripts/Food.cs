using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public BoxCollider2D grid;

    public string playerTag = "Player";

    public void MoveToRandomPosition()
    {
        float randomX = Random.Range(grid.bounds.min.x, grid.bounds.max.x);
        float randomY = Random.Range(grid.bounds.min.y, grid.bounds.max.y);

        transform.position = new Vector3(Mathf.Round(randomX), Mathf.Round(randomY), 0);
    }

    private void Start()
    {
        MoveToRandomPosition();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            MoveToRandomPosition();
        }
    }
}