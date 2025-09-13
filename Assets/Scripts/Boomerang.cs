using System.Collections;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    public float speed = 0.1f;
    public float maxDistance = 4f;
    public float spinFrameRate = 0.1f;

    private Transform playerTransform;
    private Vector3 startPos;
    private Vector3 direction;
    private bool returning = false;

    private SpriteRenderer sr;
    private Sprite[] spinSprites;
    private int currentFrame = 0;
    private Coroutine spinCoroutine;

    public void Initialize(Transform player, Vector3 dir, Sprite[] sprites)
    {
        Debug.Log("Boomerang fired");
        playerTransform = player;
        direction = dir.normalized;
        startPos = transform.position;

        spinSprites = sprites;
        sr = GetComponent<SpriteRenderer>();

        SetStartingFrame(direction);
        spinCoroutine = StartCoroutine(Spin());
    }

    void Update()
    {
        if (playerTransform == null) return;

        if (!returning && Vector3.Distance(transform.position, startPos) >= maxDistance)
        {
            returning = true;
        }

        if (returning)
        {
            Vector3 toPlayer = (playerTransform.position - transform.position).normalized;
            transform.position += toPlayer * speed;

            if (Vector3.Distance(transform.position, playerTransform.position) < 0.3f)
            {
                playerTransform.GetComponent<Projectile>().projectile_made = false;
                Destroy(gameObject);
            }
        }
        else
        {
            transform.position += direction * speed;
        }
    }

    private void SetStartingFrame(Vector3 dir)
    {
        if (dir == Vector3.up)
        {
            currentFrame = 2;
            sr.flipY = true;
        }
        
        else if (dir == Vector3.down)        
        {
            currentFrame = 2;
            sr.flipY = false;
        }
        else if (dir == Vector3.left)         
        {
            currentFrame = 0;
            sr.flipX = false;
        }
        else if (dir == Vector3.right)         
        {
            currentFrame = 0;
            sr.flipY = true;
        }

        sr.sprite = spinSprites[currentFrame];
    }

    IEnumerator Spin()
    {
        while (true)
        {
            yield return new WaitForSeconds(spinFrameRate);
            currentFrame = (currentFrame + 1) % spinSprites.Length;
            sr.sprite = spinSprites[currentFrame];
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemy"))
        {
            Debug.Log("Boomerang hit: " + other.name);
            // Add stun or other effects here
        }
    }
}
