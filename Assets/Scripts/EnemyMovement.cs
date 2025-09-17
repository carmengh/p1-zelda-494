using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 4f;
    public float moveInterval = 2f;
    public bool can_move = true;

    private float moveTimer;
    private Vector2 currentDirection = Vector2.zero;
    private Rigidbody rb;
    private SpriteRenderer sR;

    public string enemyType;
    public GoriyaSprite goriyaSprites;

    public Vector3 lastDirection = Vector3.down;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sR = GetComponent<SpriteRenderer>();

        moveTimer = moveInterval;

        PickNewDirection();
    }

    void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (!can_move)
        {
            rb.linearVelocity = Vector3.zero;
            return;
        }

        moveTimer -= Time.deltaTime;

        if (moveTimer <= 0f)
        {
            PickNewDirection();
            moveTimer = moveInterval;
        }

        float vertical = currentDirection.y;
        float horizontal = currentDirection.x;

        GridUtils.GridMovement(ref vertical, ref horizontal, ref rb);
        currentDirection = new Vector2(horizontal, vertical);

        rb.linearVelocity = currentDirection * speed;
    }

    private void PickNewDirection()
    {
        int dir = Random.Range(0, 4);

        switch (dir)
        {
            case 0:
                currentDirection = Vector2.up;
                lastDirection = Vector3.up;
                break;
            case 1:
                currentDirection = Vector2.down;
                lastDirection = Vector3.down;
                break;
            case 2:
                currentDirection = Vector2.left;
                lastDirection = Vector3.left;
                break;
            case 3:
                currentDirection = Vector2.right;
                lastDirection = Vector3.right;
                break;
        }
        SetGoriyaSprite(lastDirection);
    }

    private void SetGoriyaSprite(Vector3 dir)
    {
        if (goriyaSprites == null || goriyaSprites.sprites == null || goriyaSprites.sprites.Length < 4)
        {
            Debug.LogWarning("Goriya sprite data not assigned or incomplete.");
            return;
        }

        if (dir == Vector3.down)
        {
            sR.sprite = goriyaSprites.sprites[0];
        }
        else if (dir == Vector3.up)
        {
            sR.sprite = goriyaSprites.sprites[1];
        }
        else if (dir == Vector3.left)
        {
            sR.sprite = goriyaSprites.sprites[2];
            sR.flipX = true;
        }
        else if (dir == Vector3.right)
        {
            sR.sprite = goriyaSprites.sprites[2];
            sR.flipX = false;
        }
    }
}