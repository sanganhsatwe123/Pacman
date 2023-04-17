using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacman : MonoBehaviour
{
    public Movement movement { get; private set; }
    public Vector2 input;
    private void Awake()
    {
        this.movement = GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        if (input.y > 0)
        {
            this.movement.SetDirection(Vector2.up);
        }
        else
        {
            if (input.y < 0)
            {
                movement.SetDirection(Vector2.down);
            }
            else
            {
                if(input.x < 0)
                {
                    movement.SetDirection(Vector2.left);
                }
                else
                {
                    if (input.x > 0)
                    {
                        movement.SetDirection(Vector2.right);
                    }
                }
            }
        }
        float angle = Mathf.Atan2(this.movement.direction.y, this.movement.direction.x);
        this.transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);

    }

    public void ResetState()
    {
        this.gameObject.SetActive(true);
        this.movement.ResetState();
    }
}
