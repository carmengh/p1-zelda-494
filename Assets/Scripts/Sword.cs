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
            if (GetComponent<BoxCollider>().enabled) GetComponent<BoxCollider>().enabled = false;
        }

        Melee();  // check if you can melee instead of use projectile
        
        if (Input.GetKeyDown(KeyCode.X) && !swinging && can_attack)
        {
            Debug.Log("pressed x");
            AudioClip sword_sound = Resources.Load<AudioClip>("Zelda/Audio/Sound Effect (6)");
            AudioSource.PlayClipAtPoint(sword_sound, Camera.main.transform.position);
            if (!is_projectile)
            {
                Debug.Log("not projectile");
                swinging = true;
                GetComponent<BoxCollider>().enabled = true;
                player.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
                player.GetComponent<Movement>().canMove = false;
                StartCoroutine(SwingDirection(1));
            }
        }
    }

    IEnumerator SwingDirection(float swing_time)
    {
        SpriteRenderer player_render = player.GetComponent<SpriteRenderer>();
        Sprite player_direction = player_render.sprite;
        Vector3 swing_direction;
        Vector3 old_position = transform.position;

        // make swing direction based on which way link sprite is facing
        if (player_direction == zelda.sprites[0] || player_direction == zelda.sprites[12] || player_direction == zelda.sprites[24])
        {
            // down
            swing_direction = transform.up * -1;
            player_render.sprite = zelda.sprites[36];
            rb.transform.position = old_position + swing_direction; // move sword sprite down
            yield return new WaitForSeconds(swing_time);
            rb.transform.position = old_position; // move sword back to player
            player_render.sprite = zelda.sprites[0];
        }
        if (player_direction == zelda.sprites[1] || player_direction == zelda.sprites[13] || player_direction == zelda.sprites[25])
        {
            // left
            swing_direction = transform.right * -1;
            player_render.sprite = zelda.sprites[37];
            rb.transform.position = old_position + swing_direction; // move sword sprite left
            yield return new WaitForSeconds(swing_time);
            rb.transform.position = old_position; // move sword back to player
            player_render.sprite = zelda.sprites[1];
        }
        if (player_direction == zelda.sprites[2] || player_direction == zelda.sprites[14] || player_direction == zelda.sprites[26])
        {
            // up
            swing_direction = transform.up;
            player_render.sprite = zelda.sprites[38];
            rb.transform.position = old_position + swing_direction;  // move sword sprite up
            yield return new WaitForSeconds(swing_time);
            rb.transform.position = old_position; // move sword back to player
            player_render.sprite = zelda.sprites[2];
        }
        if (player_direction == zelda.sprites[3] || player_direction == zelda.sprites[15] || player_direction == zelda.sprites[27])
        {
            // right
            swing_direction = transform.right;
            player_render.sprite = zelda.sprites[39];
            rb.transform.position = old_position + swing_direction;  // move sword sprite right
            yield return new WaitForSeconds(swing_time);
            rb.transform.position = old_position; // move sword back to player
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
            can_attack = true;
        }
        else
        {
            can_attack = false;
        }
    }
}
