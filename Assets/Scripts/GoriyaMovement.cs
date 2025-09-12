using UnityEngine;

public class GoriyaMovement : MonoBehaviour
{
    public float speed = 4f;
    public float moveInterval = 1f;
    public bool can_move = true;

    private Rigidbody rb;
    private float moveTimer = 0f;
    private Vector2 currentDirection = Vector2.zero;

    public GetSprites goriyaSprites;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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

            rb.linearVelocity = new Vector3(currentDirection.x, currentDirection.y, 0f) * speed;
        }
    }

    void PickNewDirection()
    {
        int dir = Random.Range(0, 4);
        switch (dir)
        {
            case 0: currentDirection = Vector2.up;    
                SetSprite("up"); break;
            case 1: currentDirection = Vector2.down;  
                SetSprite("down"); break;
            case 2: currentDirection = Vector2.left;  
                SetSprite("left"); break;
            case 3: currentDirection = Vector2.right; 
                SetSprite("right"); break;
        }
    }

    void SetSprite(string direction)
    {
        if (goriyaSprites == null || goriyaSprites.sprites.Length < 4)
        {
            Debug.LogWarning("Goriya sprite data not assigned or incomplete.");
            return;
        }

        switch (direction)
        {
            case "down":  spriteRenderer.sprite = goriyaSprites.sprites[1]; break;
            case "left":  spriteRenderer.sprite = goriyaSprites.sprites[2]; 
                spriteRenderer.flipX = true;break;
            case "up":    spriteRenderer.sprite = goriyaSprites.sprites[0]; break;
            case "right": spriteRenderer.sprite = goriyaSprites.sprites[2];
                spriteRenderer.flipX = false; break;
        }
    }
}
