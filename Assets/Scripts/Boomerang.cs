using System.Collections;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    public float speed = 0.1f;
    public float maxDistance = 4f;
    public float spinFrameRate = 0.1f;

    private Transform originTransform;
    private Vector3 startPos;
    private Vector3 direction;
    private bool returning = false;

    private SpriteRenderer sr;
    private Sprite[] spinSprites;
    private int currentFrame = 0;

    public void Initialize(Transform origin, Vector3 dir, Sprite[] sprites)
    {
        originTransform = origin;
        direction = dir.normalized;
        startPos = transform.position;

        spinSprites = sprites;
        sr = GetComponent<SpriteRenderer>();

        SetStartingFrame(direction);
        StartCoroutine(Spin());
    }

    void Update()
    {
        if (originTransform == null) return;

        if (!returning && Vector3.Distance(transform.position, startPos) >= maxDistance)
        {
            returning = true;
        }

        if (returning)
        {
            Vector3 toOrigin = (originTransform.position - transform.position).normalized;
            transform.position += toOrigin * speed;

            if (Vector3.Distance(transform.position, originTransform.position) < 0.3f)
            {
                // Notify Goriya
                var enemy = originTransform.GetComponent<EnemyMovement>();
                if (enemy != null)
                {
                    enemy.OnBoomerangReturn();
                }

                // Notify player
                var proj = originTransform.GetComponent<Projectile>();
                if (proj != null)
                {
                    proj.projectile_made = false;
                }

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
        AudioClip spin_sound = Resources.Load<AudioClip>("Zelda/Audio/Sound Effect (18)");
        AudioSource.PlayClipAtPoint(spin_sound, Camera.main.transform.position);
        while (true)
        {
            yield return new WaitForSeconds(spinFrameRate);
            currentFrame = (currentFrame + 1) % spinSprites.Length;
            sr.sprite = spinSprites[currentFrame];
            Debug.Log("boomerang sprite: " + sr.sprite);
        }
    }
}
