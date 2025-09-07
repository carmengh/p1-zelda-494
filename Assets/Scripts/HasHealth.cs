using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HasHealth : MonoBehaviour
{
    public Movement movement;
    public StalfosMovement enemy_movement;
    public int health = 3;
    public int max_health = 3;
    public GetSprites zelda;
    public float force = 4;
    public bool knocked_back = false;
    Rigidbody rb;
    private bool can_hit = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject collided = collision.gameObject;
        GameObject player = rb.gameObject;
        if (player.tag == "Player" && collided.tag == "enemy")
        {
            if (can_hit)
            {
                StartCoroutine(HitStun(collided));
            }
            
            if (health == 0)
            {
                StartCoroutine(Death(2));
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject collided = other.gameObject;
        GameObject player = rb.gameObject;
        bool sword_hits = (!collided.GetComponent<Sword>().is_projectile && collided.GetComponent<Sword>().swinging) || (collided.GetComponent<Sword>().is_projectile);
        if (player.tag == "enemy" && collided.tag == "sword" && sword_hits)
        {
            StartCoroutine(HitStun(collided));
            if (health == 0)
            {
                Destroy(player);
                GetComponent<DropItem>().Drop();
            }
        }
    }

    IEnumerator HitStun(GameObject collided)
    {
        ChangeSprite();
        health--;
        can_hit = false;

        // knockback
        Vector3 knockback = (rb.transform.position - collided.GetComponent<Rigidbody>().transform.position).normalized;
        Debug.Log("knockback: " + knockback);
        knocked_back = true;
        rb.AddForce(force * knockback, ForceMode.Impulse);

        // disable then reenable movement
        if (movement != null)
        {
            movement.canMove = false;
        }
        if (enemy_movement != null) {
            enemy_movement.can_move = false;
        }
        yield return new WaitForSeconds(1);
        if (movement != null)
        {
            movement.canMove = true;
        }
        if (enemy_movement != null) {
            enemy_movement.can_move = true;
        }
        knocked_back = false;
        ChangeSprite();
        can_hit = true;
    }

    IEnumerator Death(int sec_wait)
    {
        if (movement != null) {
            movement.canMove = false;
        }
        yield return new WaitForSeconds(sec_wait);
        SceneManager.LoadScene("Main");
    }

    void ChangeSprite()
    {
        GameObject player = rb.gameObject;
        SpriteRenderer sprite = player.GetComponent<SpriteRenderer>();
        Sprite player_direction = sprite.sprite;

        // change to hit sprite
        if (player_direction == zelda.sprites[0])
        {
            // down
            sprite.sprite = zelda.sprites[4];
        }
        if (player_direction == zelda.sprites[1])
        {
            // left
            sprite.sprite = zelda.sprites[5];
        }
        if (player_direction == zelda.sprites[2])
        {
            // up
            sprite.sprite = zelda.sprites[6];
        }
        if (player_direction == zelda.sprites[3])
        {
            // right
            sprite.sprite = zelda.sprites[7];
        }

        // change back to regular sprite
        if (player_direction == zelda.sprites[4])
        {
            sprite.sprite = zelda.sprites[0];
        }
        if (player_direction == zelda.sprites[5])
        {
            sprite.sprite = zelda.sprites[1];
        }
        if (player_direction == zelda.sprites[6])
        {
            sprite.sprite = zelda.sprites[2];
        }
        if (player_direction == zelda.sprites[7])
        {
            sprite.sprite = zelda.sprites[3];
        }
    }
}
