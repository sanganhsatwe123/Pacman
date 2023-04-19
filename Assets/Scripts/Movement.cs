using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    public float speed = 8.0f;
    public float speedMultiplier = 1.0f;
    public Vector2 initialDirection;
    public LayerMask obstacaleLayer; 
    public new Rigidbody2D rigidbody {get; private set;}
    public Vector2 direction { get; private set;}
    public Vector2 nextDirection { get; private set;}
    public Vector3 startingPosition { get; private set;}

    private void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();
        this.startingPosition = this.transform.position;
    }

    private void Start()
    {
        StartCoroutine(Stop());
    }

    IEnumerator Stop()
    {
        float tempDirx;
        float tempDiry;
        tempDirx = initialDirection.x;
        tempDiry = initialDirection.y;
        initialDirection.x = 0;
        initialDirection.y = 0;
        yield return new WaitForSeconds(5);
        initialDirection.x = tempDirx;
        initialDirection.y = tempDiry;
        ResetState();
    }

    public void ResetState()
    {
        this.speedMultiplier = 1.0f;
        this.direction = this.initialDirection;
        this.nextDirection = Vector2.zero;
        this.transform.position = this.startingPosition;
        this.rigidbody.isKinematic = false;
        this.enabled = true;
    }

    private void Update()
    {
        if(this.nextDirection != Vector2.zero)
        {
            SetDirection(this.nextDirection);
        }
    }

    private void FixedUpdate()
    {
        Vector2 pos = this.rigidbody.position;
        Vector2 trans = this.direction * this.speed * this.speedMultiplier * Time.fixedDeltaTime;
        this.rigidbody.MovePosition(pos + trans);
    }

    public void SetDirection(Vector2 direction, bool forced = false)
    {
        if (forced ||!Occupied(direction))
        {
            this.direction = direction;
            this.nextDirection = Vector2.zero;
        }
        else
        {
            this.nextDirection = direction;
        }
    }

    public bool Occupied(Vector2 direction)
    {
        RaycastHit2D hit2D = Physics2D.BoxCast(this.transform.position, Vector2.one * 0.75f, 0.0f, direction, 1.5f, this.obstacaleLayer);
        return hit2D.collider != null;
    }
}
