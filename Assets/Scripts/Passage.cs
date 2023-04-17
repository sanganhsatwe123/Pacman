using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passage : MonoBehaviour
{
    public Transform conn;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector3 pos = collision.transform.position;
        pos.x = this.conn.position.x;
        pos.y = this.conn.position.y;
        collision.transform.position = pos;
    }
}
