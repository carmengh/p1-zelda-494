using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HasHealth : MonoBehaviour
{
    public Movement movement;
    public EnemyMovement enemy_movement;

    public int health = 3;
    public int max_health = 3;

    public GetSprites zelda;
    public float force = 4;
    public bool knocked_back = false;

    private Rigidbody rb;
    private bool can_hit = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // You can add visual indicators for health here if needed
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject collided = collision.gameObject;

        if (CompareTag("Player") && collided.CompareTag("enemy"))
        {
            // Only apply damage if God Mode is off
            if (can_hit && !SetWindowedResolution.God_Mode)
            {
                StartCoroutine(HitStun(collided));

                if (health <= 0)
                {
                    StartCoroutine(Death(2));
                }
            }
            else if (SetWindowedResolution.God_Mode)
            {
                Debug.Log("Player is in God Mode – no damage taken.");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject collided = other.gameObject;

        bool isSword = collided.CompareTag("sword");

        // Projectiles (like arrows) do NOT use Sword.cs
        //bool isProjectile = collided.GetComponent<Sword>() != null && collided.GetComponent<Sword>().is_projectile;
        bool isProjectile = (collided.layer == 7);
        bool isSwingingSword = collided.GetComponent<Sword>() != null && collided.GetComponent<Sword>().swinging;

        bool shouldTakeDamage = (isProjectile || isSwingingSword) && isSword;

        if ((CompareTag("enemy") && shouldTakeDamage) || (CompareTag("enemy") && collided.tag == "PlayerBoomerang"))
        {
            AudioClip hit_sound = Resources.Load<AudioClip>("Zelda/Audio/Sound Effect (8)");
            AudioSource.PlayClipAtPoint(hit_sound, Camera.main.transform.position);
            StartCoroutine(HitStun(collided));

            if (health <= 0)
            {
                Destroy(gameObject);
                GetComponent<DropItem>()?.Drop();
            }

            // Destroy projectile after hit
            if (isProjectile && (collided.GetComponent<Sword>() != null && collided.GetComponent<Sword>().is_projectile))
            {
                Destroy(collided);
            }
        }

        if (CompareTag("enemy") && collided.tag == "explosion")
        {
            health = 0;
            Destroy(gameObject);
            GetComponent<DropItem>()?.Drop();
        }

        bool isEnemyProjectile = (collided.layer == 12);
        // Optional: prevent arrows from damaging the player
        if (CompareTag("Player") && isEnemyProjectile && !SetWindowedResolution.God_Mode)
        {
            AudioClip hit_sound = Resources.Load<AudioClip>("Zelda/Audio/Sound Effect (10)");
            AudioSource.PlayClipAtPoint(hit_sound, Camera.main.transform.position);
            StartCoroutine(HitStun(collided));

            if (health <= 0)
            {
                StartCoroutine(Death(2));
            }

            Destroy(collided);
        }
    }

    IEnumerator HitStun(GameObject collided)
    {
        StartCoroutine(ChangeSprite());
        health--;
        can_hit = false;

        // Knockback
        Vector3 direction = (rb.position - collided.transform.position);
        
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            direction = new Vector3(Mathf.Sign(direction.x), 0, 0);
        }
        else
        {
            direction = new Vector3(0, Mathf.Sign(direction.y), 0);
        }

        // Apply knockback
        rb.linearVelocity = Vector3.zero; // Reset current velocity
        rb.AddForce(force * direction, ForceMode.Impulse);
        knocked_back = true;

        // Disable movement temporarily
        if (movement != null) movement.canMove = false;
        if (enemy_movement != null) enemy_movement.can_move = false;
        if (GetComponent<KeeseMovement>() != null) GetComponent<KeeseMovement>().can_move = false;

        yield return new WaitForSeconds(1f);

        // Re-enable movement
        if (movement != null) movement.canMove = true;
        if (enemy_movement != null) enemy_movement.can_move = true;
        if (GetComponent<KeeseMovement>() != null) GetComponent<KeeseMovement>().can_move = true;

        knocked_back = false;
        StartCoroutine(ChangeSprite());
        can_hit = true;
    }

    IEnumerator Death(int sec_wait)
    {
        AudioClip death_sound = Resources.Load<AudioClip>("Zelda/Audio/Sound Effect (22)");
        AudioSource.PlayClipAtPoint(death_sound, Camera.main.transform.position);
        if (movement != null)
        {
            movement.canMove = false;
        }

        yield return new WaitForSeconds(sec_wait);
        SceneManager.LoadScene("Main");
    }

    IEnumerator ChangeSprite()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null || zelda == null || zelda.sprites.Length < 8)
        {
            Debug.LogWarning("Missing SpriteRenderer or Zelda sprites on " + gameObject.name);
            yield return null;
        }

        Debug.Log("sprite color: " + spriteRenderer.color);
        Debug.Log("white: " + Color.white);
        if (spriteRenderer.color == Color.white)
        {
            Debug.Log("change to red");
            spriteRenderer.color = Color.red;
        }
        else if (spriteRenderer.color == Color.red)
        {
            Debug.Log("change to white");
            spriteRenderer.color = Color.white;
        }

        yield return new WaitForSeconds(1);

        //Sprite current = spriteRenderer.sprite;

        //// Change to "hit" sprite
        //if (current == zelda.sprites[0]) spriteRenderer.sprite = zelda.sprites[4]; // down
        //else if (current == zelda.sprites[1]) spriteRenderer.sprite = zelda.sprites[5]; // left
        //else if (current == zelda.sprites[2]) spriteRenderer.sprite = zelda.sprites[6]; // up
        //else if (current == zelda.sprites[3]) spriteRenderer.sprite = zelda.sprites[7]; // right

        //// Restore after hit
        //else if (current == zelda.sprites[4]) spriteRenderer.sprite = zelda.sprites[0];
        //else if (current == zelda.sprites[5]) spriteRenderer.sprite = zelda.sprites[1];
        //else if (current == zelda.sprites[6]) spriteRenderer.sprite = zelda.sprites[2];
        //else if (current == zelda.sprites[7]) spriteRenderer.sprite = zelda.sprites[3];
    }

}
