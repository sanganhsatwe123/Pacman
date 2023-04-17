using UnityEngine;

public class GhostChase : GhostBehavior
{
    private void OnDisable()
    {
        ghost.scatter.Enable();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Nodes nodes = collision.GetComponent<Nodes>();

        if (nodes != null && this.enabled && !this.ghost.frightened.enabled)
        {
            Vector2 dir = Vector2.zero;
            float minDistance = float.MaxValue;

            foreach (Vector2 availableDirection in nodes.availableDirections)
            {
                Vector3 newPos = this.transform.position + new Vector3(availableDirection.x,availableDirection.y, 0.0f);
                float distance = (this.ghost.target.position - newPos).sqrMagnitude;
                if (distance < minDistance)
                {
                    dir = availableDirection;
                    minDistance = distance;
                }
            }

            this.ghost.movement.SetDirection(dir);
        }
    }
}
