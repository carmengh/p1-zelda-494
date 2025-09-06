using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public bool stop = true;
    public bool projectile_made = false;
    public float speed = .25f;
    public GameObject prefab;
    public GetSprites zelda;
    public GameObject projectile;
    public Sword sword;

    Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.GetComponent<HasHealth>().health == rb.GetComponent<HasHealth>().max_health)
        {
            bool can_shoot = CheckAttack();
            if (Input.GetKeyDown(KeyCode.X) && !projectile_made && can_shoot)
            {
                projectile_made = true;
                Debug.Log("can shoot: " + can_shoot);
                projectile = Instantiate(prefab, transform.position, transform.rotation);
                projectile.GetComponent<Sword>().is_projectile = true;  // need to make if statements on which weapon component to grab based on current primary/alt weapon
                Debug.Log("create projectile");
                GetComponent<Movement>().canMove = false;
                StartCoroutine(Shoot(38, 36, 39, 37, 119, 117, 120, 118)); // also need to make if statements to specify different sprites for each weapons
            }
        }
    }

    // takes in index to use for 'zelda' sprite array; look through Assets/Resources/Zelda/link_sprites to find sprite index
    // Ex: 'link_sprites_0' has index of 0, 'link_sprites_117' has index of 117
    IEnumerator Shoot(int player_up, int player_down, int player_right, int player_left, int weapon_up, int weapon_down, int weapon_right, int weapon_left)
    {
        SpriteRenderer player_render = GetComponent<SpriteRenderer>();
        Sprite player_direction = player_render.sprite;
        if (player_direction == zelda.sprites[0])
        {
            // down
            player_render.sprite = zelda.sprites[player_down];
            projectile.GetComponent<SpriteRenderer>().sprite = zelda.sprites[weapon_down];
            StartCoroutine(Move(new Vector3(0, -1, 0)));
            player_render.sprite = zelda.sprites[0];
        }
        if (player_direction == zelda.sprites[1])
        {
            // left
            player_render.sprite = zelda.sprites[player_left];
            projectile.GetComponent<SpriteRenderer>().sprite = zelda.sprites[weapon_left];
            StartCoroutine(Move(new Vector3(-1, 0, 0)));
            player_render.sprite = zelda.sprites[1];
        }
        if (player_direction == zelda.sprites[2])
        {
            // up
            player_render.sprite = zelda.sprites[player_up];
            projectile.GetComponent<SpriteRenderer>().sprite = zelda.sprites[weapon_up];
            StartCoroutine(Move(new Vector3(0, 1, 0)));
            player_render.sprite = zelda.sprites[2];
        }
        if (player_direction == zelda.sprites[3])
        {
            // right
            player_render.sprite = zelda.sprites[player_right];
            projectile.GetComponent<SpriteRenderer>().sprite = zelda.sprites[weapon_right];
            StartCoroutine(Move(new Vector3(1, 0, 0)));
            player_render.sprite = zelda.sprites[3];
        }

        yield return null;
    }

    IEnumerator Move(Vector3 position)
    {
        Debug.Log("enter Move function");
        stop = false;
        yield return null;
        GetComponent<Movement>().canMove = true;
        while (!stop)   // projectile flies until it hits something; ontriggerfunction set in weapon's script
        {
            projectile.transform.position = projectile.transform.position + (position * speed);
            yield return null;
        }
    }

    bool CheckAttack()  // very specific for sword... will have for now
    {
        if (sword.swinging)
        {
            return false;
        }
        return true;
    }
}
