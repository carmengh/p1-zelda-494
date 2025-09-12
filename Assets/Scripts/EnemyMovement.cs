using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 4f;
    public float moveInterval = 2f;
    public bool can_move = true;
    public string enemyType;

    private Rigidbody rb;
    private float moveTimer = 0f;
    private Vector2 currentDirection = Vector2.zero;

    public GoriyaSprite goriyaSprites;
    private SpriteRenderer sR;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sR = GetComponent<SpriteRenderer>();

        if (enemyType == "goriya")
        {
            if (goriyaSprites == null)
            {
                Debug.LogWarning("GoriyaSprites reference not assigned on " + gameObject.name);
            }
            else if (goriyaSprites.sprites == null || goriyaSprites.sprites.Length < 4)
            {
                Debug.LogWarning("Goriya sprites not loaded or incomplete on " + gameObject.name);
            }
        }

        PickNewDirection();
        moveTimer = moveInterval;
    }

    void Update()
    {
        moveTimer -= Time.deltaTime;

        if (can_move)
        {
            if (moveTimer <= 0f)
            {
                PickNewDirection();
                moveTimer = moveInterval;
            }

            float verticalDir = currentDirection.y;
            float horizontalDir = currentDirection.x;

            GridUtils.GridMovement(ref verticalDir, ref horizontalDir, ref rb);

            currentDirection = new Vector2(horizontalDir, verticalDir);

            rb.linearVelocity = currentDirection * speed;
        }
    }

    void PickNewDirection()
    {
        int dir = Random.Range(0, 4);
        switch (dir)
        {
            case 0:
                currentDirection = Vector2.up;
                if (enemyType == "goriya") SetSprite("up");
                break;
            case 1:
                currentDirection = Vector2.down;
                if (enemyType == "goriya") SetSprite("down");
                break;
            case 2:
                currentDirection = Vector2.left;
                if (enemyType == "goriya") SetSprite("left");
                break;
            case 3:
                currentDirection = Vector2.right;
                if (enemyType == "goriya") SetSprite("right");
                break;
        }
    }

    void SetSprite(string direction)
    {
        if (goriyaSprites == null || goriyaSprites.sprites == null || goriyaSprites.sprites.Length < 4)
        {
            Debug.LogWarning("Goriya sprite data not assigned or incomplete.");
            return;
        }

        switch (direction)
        {
            case "down":
                sR.sprite = goriyaSprites.sprites[0];
                break;
            case "left":
                sR.sprite = goriyaSprites.sprites[2];
                sR.flipX = true;
                break;
            case "up":
                sR.sprite = goriyaSprites.sprites[1];
                break;
            case "right":
                sR.sprite = goriyaSprites.sprites[2];
                sR.flipX = false;
                break;
        }
    }
}
