using UnityEngine;

public class StalfosMovement : MonoBehaviour
{
    public float speed = 4f;                 // Movement speed
    public float moveInterval = 1f;          // Time between direction changes
    public bool can_move = true;

    private Rigidbody rb;
    private float moveTimer = 0f;
    private Vector2 currentDirection = Vector2.zero;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        PickNewDirection();
        moveTimer = moveInterval;
    }

    void Update()
    {
        moveTimer -= Time.deltaTime;

        // Pick a new direction
        if (can_move)
        {
            if (moveTimer <= 0f)
            {
                PickNewDirection();
                moveTimer = moveInterval;
            }

            // Apply grid movement logic
            float verticalDir = currentDirection.y;
            float horizontalDir = currentDirection.x;

            GridUtils.GridMovement(ref verticalDir, ref horizontalDir, ref rb);

            currentDirection = new Vector2(horizontalDir, verticalDir);

            // Apply movement
            rb.linearVelocity = new Vector3(currentDirection.x, currentDirection.y, 0f) * speed;
        }
    }

    void PickNewDirection()
    {
        int dir = Random.Range(0, 4);
        switch (dir)
        {
            case 0: currentDirection = Vector2.up; break;
            case 1: currentDirection = Vector2.down; break;
            case 2: currentDirection = Vector2.left; break;
            case 3: currentDirection = Vector2.right; break;
        }
    }
}