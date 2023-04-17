using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nodes : MonoBehaviour
{
    public List<Vector2> availableDirections { get; private set; }
    public LayerMask obstacaleLayer;

    private void Start()
    {
        this.availableDirections = new List<Vector2>();
        CheckAvailableDirections(Vector2.up);
        CheckAvailableDirections(Vector2.down);
        CheckAvailableDirections(Vector2.left);
        CheckAvailableDirections(Vector2.right);
    }

    private void CheckAvailableDirections(Vector2 direction)
    {
        RaycastHit2D hit2D = Physics2D.BoxCast(this.transform.position, Vector2.one * 0.5f, 0.0f, direction, 1.0f, this.obstacaleLayer);
        if(hit2D.collider == null)
        {
            this.availableDirections.Add(direction);
        }
    }
}
