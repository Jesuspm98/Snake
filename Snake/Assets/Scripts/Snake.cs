using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector3 currentDirection = Vector3.right;

    public Transform segmentPrefab;

    private List<Transform> segments;

    private string foodTag = "Food";

    private string damageTag = "Damage";

    private void Start()
    {
        StartSegments();
    }

    private void StartSegments()
    {
        segments = new List<Transform>();
        segments.Add(transform);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && currentDirection != Vector3.down)
        {
            currentDirection = Vector3.up;
        }
        else if (Input.GetKeyDown(KeyCode.A) && currentDirection != Vector3.right)
        {
            currentDirection = Vector3.left;
        }
        else if (Input.GetKeyDown(KeyCode.S) && currentDirection != Vector3.up)
        {
            currentDirection = Vector3.down;
        }
        else if (Input.GetKeyDown(KeyCode.D) && currentDirection != Vector3.left)
        {
            currentDirection = Vector3.right;
        }
    }

    private void FixedUpdate()
    {
        MoveSegments();
        transform.position = NextSnakePosition();
    }

    public Vector3 NextSnakePosition()
    {
        float x = Mathf.Round(transform.position.x + currentDirection.x);
        float y = Mathf.Round(transform.position.y + currentDirection.y);

        return new Vector3(x, y, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(foodTag))
        {
            Transform segment = Instantiate(segmentPrefab);
            segment.position = segments[segments.Count - 1].position;
            segments.Add(segment);
        }
        else if (collision.gameObject.CompareTag(damageTag))
        {
            Restart();
        }
    }

    private void Restart()
    {
        transform.position = Vector3.zero;
        for (int i = 1; i < segments.Count; i++)
        {
            Destroy(segments[i].gameObject);
        }

        segments.Clear();

        StartSegments();
    }

    private void MoveSegments()
    {
        for (int i = segments.Count - 1; i > 0; i--)
        {
            segments[i].position = segments[i - 1].position;
        }
    }
}