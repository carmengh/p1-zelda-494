using System.Collections;
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

    public GameObject boomerangPrefab;
    public Sprite[] spinSprites;
    public float fireInterval = 3f;
    public float cooldownAfterReturn = 1.5f;

    private bool boomerangActive = false;
    private bool waitingToFire = false;
    private float fireTimer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sR = GetComponent<SpriteRenderer>();

        PickNewDirection();
        moveTimer = moveInterval;
        fireTimer = fireInterval;
    }

    void Update()
    {
        if (can_move)
        {
            moveTimer -= Time.deltaTime;

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
        else
        {
            rb.linearVelocity = Vector3.zero;
        }

        if (enemyType == "goriya" && !boomerangActive && !waitingToFire)
        {
            fireTimer -= Time.deltaTime;

            if (fireTimer <= 0f)
            {
                FireBoomerang();
                fireTimer = fireInterval;
            }
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
                sR.flipX = false;
                break;
            case "left":
                sR.sprite = goriyaSprites.sprites[2];
                sR.flipX = true;
                break;
            case "up":
                sR.sprite = goriyaSprites.sprites[1];
                sR.flipX = false;
                break;
            case "right":
                sR.sprite = goriyaSprites.sprites[2];
                sR.flipX = false;
                break;
        }
    }

    void FireBoomerang()
    {
        if (boomerangPrefab == null || spinSprites == null || spinSprites.Length == 0)
        {
            Debug.LogWarning("Boomerang prefab or spin sprites not assigned.");
            return;
        }

        Vector3 dir;

        if (sR.sprite == goriyaSprites.sprites[1]) // up sprite
        {
            dir = Vector3.up;
        }
        else if (sR.sprite == goriyaSprites.sprites[0]) // down sprite
        {
            dir = Vector3.down;
        }
        else if (sR.sprite == goriyaSprites.sprites[2]) // side sprite
        {
            dir = sR.flipX ? Vector3.left : Vector3.right;
        }
        else
        {
            dir = currentDirection != Vector2.zero ? new Vector3(currentDirection.x, currentDirection.y, 0).normalized : Vector3.right;
        }

        can_move = false;
        boomerangActive = true;

        GameObject boom = Instantiate(boomerangPrefab, transform.position, Quaternion.identity);
        Boomerang boomScript = boom.GetComponent<Boomerang>();
        boomScript.Initialize(transform, dir, spinSprites);
    }

    public void OnBoomerangReturn()
    {
        can_move = true;
        boomerangActive = false;
        StartCoroutine(FireCooldown());
    }

    IEnumerator FireCooldown()
    {
        waitingToFire = true;
        yield return new WaitForSeconds(cooldownAfterReturn);
        waitingToFire = false;
    }
}
