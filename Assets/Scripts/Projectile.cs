using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public bool stop = true;
    public bool projectile_made = false;
    public float speed = .125f;
    public GameObject prefab;
    public GameObject arrowPrefab;
    
    public GetSprites zelda;
    public GameObject projectile;
    public Sword sword;
    public Inventory inventory;
    
    Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void Update()
    {
        string currentWeapon = inventory.GetCurrentWeapon();

        if (Input.GetKeyDown(KeyCode.X))
        {
            HandleSwordAttack();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            HandleAltWeapon(currentWeapon);
        }
    }
    
    void HandleSwordAttack()
    {
        if (!projectile_made && CheckAttack() && rb.GetComponent<HasHealth>().health == rb.GetComponent<HasHealth>().max_health)
        {
            projectile_made = true;
            projectile = Instantiate(prefab, transform.position, transform.rotation);
            projectile.GetComponent<Sword>().is_projectile = true;
            Debug.Log("Firing Sword Beam");
            GetComponent<Movement>().canMove = false;

            // Replace these sprite indices with appropriate sword beam ones
            StartCoroutine(Shoot(38, 36, 39, 37, 119, 117, 120, 118));
        }
    }
    
    void HandleAltWeapon(string currentWeapon)
    {
        if (projectile_made || !CheckAttack()) return;

        if (currentWeapon == "Bow")
        {
            if (inventory.rupee_count < 1)
            {
                return;
            }

            inventory.rupee_count--;
            if (arrowPrefab == null)
            {
                Debug.LogError("Arrow prefab is missing!");
                return;
            }

            projectile_made = true;
            projectile = Instantiate(arrowPrefab, transform.position, transform.rotation);
            Debug.Log("Arrow created at: " + projectile.transform.position);
            Debug.Log("Firing Arrow");
            GetComponent<Movement>().canMove = false;

            StartCoroutine(ShootWithoutPlayerAnimation(121, 122, 130, 131));
        }
        else
        {
            Debug.Log("Alt weapon not supported yet: " + currentWeapon);
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
    
    IEnumerator ShootWithoutPlayerAnimation(int weapon_up, int weapon_down, int weapon_right, int weapon_left)
    {
        SpriteRenderer player_render = GetComponent<SpriteRenderer>();
        Sprite player_direction = player_render.sprite;

        if (player_direction == zelda.sprites[0])
        {
            projectile.GetComponent<SpriteRenderer>().sprite = zelda.sprites[weapon_down];
            Debug.Log("Arrow sprite set to: " + zelda.sprites[weapon_down].name);

            StartCoroutine(Move(new Vector3(0, -1, 0)));
        }
        else if (player_direction == zelda.sprites[1])
        {
            projectile.GetComponent<SpriteRenderer>().sprite = zelda.sprites[weapon_left];
            StartCoroutine(Move(new Vector3(-1, 0, 0)));
        }
        else if (player_direction == zelda.sprites[2])
        {
            projectile.GetComponent<SpriteRenderer>().sprite = zelda.sprites[weapon_up];
            StartCoroutine(Move(new Vector3(0, 1, 0)));
        }
        else if (player_direction == zelda.sprites[3])
        {
            projectile.GetComponent<SpriteRenderer>().sprite = zelda.sprites[weapon_right];
            StartCoroutine(Move(new Vector3(1, 0, 0)));
        }

        yield return null;
    }


    IEnumerator Move(Vector3 position)
    {
        Debug.Log("enter Move function");
        stop = false;
        yield return null;
        GetComponent<Movement>().canMove = true;

        while (!stop)
        {
            if (projectile == null)
            {
                projectile_made = false;
                yield break;
            }

            projectile.transform.position += (position * speed);
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
