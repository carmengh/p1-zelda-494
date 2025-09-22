using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Projectile : MonoBehaviour
{
    public bool stop = true;
    public bool projectile_made = false;
    public float speed = .125f;
    public GameObject prefab;
    public GameObject arrowPrefab;
    public GameObject boomerangPrefab;
    public GameObject bombPrefab;
    public GameObject explosionPrefab;
    public GameObject pullOrbPrefab;

    
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
            AudioClip sword_sound = Resources.Load<AudioClip>("Zelda/Audio/Sound Effect (13)");
            AudioSource.PlayClipAtPoint(sword_sound, Camera.main.transform.position);
            StartCoroutine(Shoot(38, 36, 39, 37, 119, 117, 120, 118));
        }
    }
    
    void HandleAltWeapon(string currentWeapon)
    {
        if (projectile == null)
        {
            projectile_made = false;
        }
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
        else if (currentWeapon == "Boomerang")
        {
            Vector3 dir = GetFacingDirection();
            projectile = Instantiate(boomerangPrefab, transform.position, Quaternion.identity);
            projectile_made = true;

            Sprite[] boomerangSpin = new Sprite[]
            {
               zelda.boomerangSprites[0] , zelda.boomerangSprites[1], zelda.boomerangSprites[2]
            };

            projectile.GetComponent<Boomerang>().Initialize(transform, dir, boomerangSpin);
        }
        else if (currentWeapon == "Bomb")
        {
            StartCoroutine(DropBomb());
        }
        else if (currentWeapon == "Pull Orb")
        {
            if (pullOrbPrefab == null)
            {
                Debug.LogError("Pull Orb prefab not assigned!");
                return;
            }

            projectile_made = true;

            Vector3 direction = GetFacingDirection();
            Vector3 spawnPosition = transform.position + direction;

            projectile = Instantiate(pullOrbPrefab, spawnPosition, Quaternion.identity);

            Debug.Log("Pull Orb created at: " + projectile.transform.position);
            GetComponent<Movement>().canMove = false;

            StartCoroutine(Move(direction));
        }
        else
        {
            Debug.Log("Alt weapon not supported yet: " + currentWeapon);
        }
        
    }

    IEnumerator DropBomb()
    {
        Vector3 spawn = transform.position;
        GameObject bomb = Instantiate(bombPrefab, spawn, Quaternion.identity);
        yield return new WaitForSeconds(2);
        Destroy(bomb);
        GameObject explosion = Instantiate(explosionPrefab, spawn, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        AudioClip explode_sound = Resources.Load<AudioClip>("Zelda/Audio/Sound Effect (10)");
        AudioSource.PlayClipAtPoint(explode_sound, Camera.main.transform.position);
        Destroy(explosion);
        yield return null;
    }

    // takes in index to use for 'zelda' sprite array; look through Assets/Resources/Zelda/link_sprites to find sprite index
    // Ex: 'link_sprites_0' has index of 0, 'link_sprites_117' has index of 117
    IEnumerator Shoot(int player_up, int player_down, int player_right, int player_left, int weapon_up, int weapon_down, int weapon_right, int weapon_left)
    {
        SpriteRenderer player_render = GetComponent<SpriteRenderer>();
        Sprite player_direction = player_render.sprite;
        if (player_direction == zelda.sprites[0]||player_direction == zelda.sprites[12]||player_direction == zelda.sprites[24])
        {
            // down
            player_render.sprite = zelda.sprites[player_down];
            projectile.GetComponent<SpriteRenderer>().sprite = zelda.sprites[weapon_down];
            StartCoroutine(Move(new Vector3(0, -1, 0)));
            player_render.sprite = zelda.sprites[0];
        }
        else if (player_direction == zelda.sprites[1]||player_direction == zelda.sprites[13]||player_direction == zelda.sprites[25])
        {
            // left
            player_render.sprite = zelda.sprites[player_left];
            projectile.GetComponent<SpriteRenderer>().sprite = zelda.sprites[weapon_left];
            StartCoroutine(Move(new Vector3(-1, 0, 0)));
            player_render.sprite = zelda.sprites[1];
        }
        else if (player_direction == zelda.sprites[2]||player_direction == zelda.sprites[14]||player_direction == zelda.sprites[26])
        {
            // up
            player_render.sprite = zelda.sprites[player_up];
            projectile.GetComponent<SpriteRenderer>().sprite = zelda.sprites[weapon_up];
            StartCoroutine(Move(new Vector3(0, 1, 0)));
            player_render.sprite = zelda.sprites[2];
        }
        else if (player_direction == zelda.sprites[3]||player_direction == zelda.sprites[15]||player_direction == zelda.sprites[27])
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

        if (player_direction == zelda.sprites[0]||player_direction == zelda.sprites[12]||player_direction == zelda.sprites[24])
        {
            projectile.GetComponent<SpriteRenderer>().sprite = zelda.sprites[weapon_down];

            StartCoroutine(Move(new Vector3(0, -1, 0)));
        }
        else if (player_direction == zelda.sprites[1]||player_direction == zelda.sprites[13]||player_direction == zelda.sprites[25])
        {
            projectile.GetComponent<SpriteRenderer>().sprite = zelda.sprites[weapon_left];
            StartCoroutine(Move(new Vector3(-1, 0, 0)));
        }
        else if (player_direction == zelda.sprites[2]||player_direction == zelda.sprites[14]||player_direction == zelda.sprites[26])
        {
            projectile.GetComponent<SpriteRenderer>().sprite = zelda.sprites[weapon_up];
            StartCoroutine(Move(new Vector3(0, 1, 0)));
        }
        else if (player_direction == zelda.sprites[3]||player_direction == zelda.sprites[15]||player_direction == zelda.sprites[27])
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
    

    Vector3 GetFacingDirection()
    {
        Sprite current = GetComponent<SpriteRenderer>().sprite;

        if (current == zelda.sprites[0] || current == zelda.sprites[12] || current == zelda.sprites[24])
            return Vector3.down;

        if (current == zelda.sprites[1] || current == zelda.sprites[13] || current == zelda.sprites[25])
            return Vector3.left;

        if (current == zelda.sprites[2] || current == zelda.sprites[14] || current == zelda.sprites[26])
            return Vector3.up;

        if (current == zelda.sprites[3] || current == zelda.sprites[15] || current == zelda.sprites[27])
            return Vector3.right;

        return Vector3.down;
    }

}
