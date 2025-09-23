using UnityEngine;

public class Collector : MonoBehaviour
{
    Inventory inventory;
    public AudioClip rupee_collection_sound;
    AudioClip collect_sound;
    AudioClip weapon_sound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        weapon_sound = Resources.Load<AudioClip>("Zelda/Audio/Sound Effect (16)");
        collect_sound = Resources.Load<AudioClip>("Zelda/Audio/Sound Effect (4)");
        inventory = GetComponent<Inventory>();
        if (inventory != null)
        {
            Debug.Log("inventory null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider coll)
    {
        GameObject object_collided_with = coll.gameObject;

        // get weapon
        if (object_collided_with.tag == "bow")
        {
            AudioClip bow_sound = Resources.Load<AudioClip>("Zelda/Audio/Sound Effect (4)");
            AudioSource.PlayClipAtPoint(bow_sound, Camera.main.transform.position);
            Destroy(object_collided_with);
            GetComponent<Inventory>().altWeapons.Add("Bow");
            GetComponent<Inventory>().UpdateAltWeaponUI();
        }
        if (object_collided_with.tag == "PlayerBoomerang")
        {
            AudioSource.PlayClipAtPoint(weapon_sound, Camera.main.transform.position);
            Destroy(object_collided_with);
            GetComponent<Inventory>().altWeapons.Add("Boomerang");
            GetComponent<Inventory>().UpdateAltWeaponUI();
        }
        if (object_collided_with.tag == "bomb")
        {
            AudioSource.PlayClipAtPoint(weapon_sound, Camera.main.transform.position);
            Destroy(object_collided_with);
            GetComponent<Inventory>().altWeapons.Add("Bomb");
            GetComponent<Inventory>().UpdateAltWeaponUI();
            GetComponent<Inventory>().bomb_count += 4;
        }
        if (object_collided_with.tag == "pull")
        {
            AudioSource.PlayClipAtPoint(weapon_sound, Camera.main.transform.position);
            Destroy(object_collided_with);
            GetComponent<Inventory>().altWeapons.Add("Pull Orb");
            GetComponent<Inventory>().UpdateAltWeaponUI();
        }

        // get item
        if (object_collided_with.tag == "rupee")
        {
            Debug.Log("collected rupee");
            if (inventory != null) {
                inventory.AddInventory(ref inventory.rupee_count, 1);
                Debug.Log("rupees: " +  inventory.rupee_count);
            }
            Destroy(object_collided_with);

            AudioSource.PlayClipAtPoint(rupee_collection_sound, Camera.main.transform.position);
        }
        if (object_collided_with.tag == "key")
        {
            Debug.Log("collected key");
            if (inventory != null)
            {
                inventory.AddInventory(ref inventory.key_count, 1);
                Debug.Log("keys: " + inventory.key_count);
            }
            Destroy(object_collided_with);

            AudioSource.PlayClipAtPoint(collect_sound, Camera.main.transform.position);
        }
        if (object_collided_with.tag == "heart")
        {
            if (GetComponent<HasHealth>().health != GetComponent<HasHealth>().max_health)
            {
                GetComponent<HasHealth>().health++;
            }
            Destroy(object_collided_with);
            AudioSource.PlayClipAtPoint(collect_sound, Camera.main.transform.position);
        }
    }
}
