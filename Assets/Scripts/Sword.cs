using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class Sword : MonoBehaviour
{
    public GameObject player;
    public GetSprites zelda;
    public float swing_time = 0.5f;
    public bool is_projectile = false;
    public bool swinging = false;
    
    bool can_attack = false;

    Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.transform.position = player.GetComponent<Rigidbody>().position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!swinging && !is_projectile)
        {
            rb.transform.position = player.GetComponent<Rigidbody>().position;
        }

        Melee();  // check if you can melee instead of use projectile
        
        if (Input.GetKeyDown(KeyCode.X) && !swinging && !is_projectile && can_attack)
        {
            Debug.Log("x pressed");
            swinging = true;
            player.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            player.GetComponent<Movement>().canMove = false;
            StartCoroutine(SwingDirection(1));
        }
    }

    IEnumerator SwingDirection(float swing_time)
    {
        SpriteRenderer player_render = player.GetComponent<SpriteRenderer>();
        Sprite player_direction = player_render.sprite;

        // make swing direction based on which way link sprite is facing
        if (player_direction == zelda.sprites[0])
        {
            // down
            player_render.sprite = zelda.sprites[36];
            rb.linearVelocity = new Vector2(0, -1); // move sword sprite down
            yield return new WaitForSeconds(swing_time);
            rb.linearVelocity = new Vector2(0, 1); // move sword back to player
            yield return new WaitForSeconds(swing_time);
            player_render.sprite = zelda.sprites[0];
        }
        if (player_direction == zelda.sprites[1])
        {
            // left
            player_render.sprite = zelda.sprites[37];
            rb.linearVelocity = new Vector2(-1, 0); // move sword sprite left
            yield return new WaitForSeconds(swing_time);
            rb.linearVelocity = new Vector2(1, 0); // move sword back to player
            yield return new WaitForSeconds(swing_time);
            player_render.sprite = zelda.sprites[1];
        }
        if (player_direction == zelda.sprites[2])
        {
            // up
            player_render.sprite = zelda.sprites[38];
            rb.linearVelocity = new Vector2(0, 1);  // move sword sprite up
            yield return new WaitForSeconds(swing_time);
            rb.linearVelocity = new Vector2(0, -1); // move sword back to player
            yield return new WaitForSeconds(swing_time);
            player_render.sprite = zelda.sprites[2];
        }
        if (player_direction == zelda.sprites[3])
        {
            // right
            player_render.sprite = zelda.sprites[39];
            rb.linearVelocity = new Vector2(1, 0);  // move sword sprite right
            yield return new WaitForSeconds(swing_time);
            rb.linearVelocity = new Vector2(-1, 0); // move sword back to player
            yield return new WaitForSeconds(swing_time);
            player_render.sprite = zelda.sprites[3];
        }

        swinging = false;
        player.GetComponent<Movement>().canMove = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collided with " + other.tag);
        if (is_projectile)
        {
            Debug.Log("destroy projectile");
            player.GetComponent<Projectile>().stop = true;
            Destroy(player.GetComponent<Projectile>().projectile);
            player.GetComponent<Projectile>().projectile_made = false;
            Debug.Log("projectile_made: " + player.GetComponent<Projectile>().projectile_made);
        }
    }

    void Melee()
    {
        if (player.GetComponent<Projectile>().projectile_made || (player.GetComponent<HasHealth>().health != player.GetComponent<HasHealth>().max_health))
        {
            Debug.Log("sword projectile_made: " + player.GetComponent<Projectile>().projectile_made);
            can_attack = true;
        }
        else
        {
            can_attack = false;
        }
    }
}
