using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public bool locked = true;
    public Inventory inventory;
    public Sprite this_new_sprite;
    public Sprite other_new_sprite;
    public GameObject other_door;
    public AudioClip door_sound;

    private GameObject this_door;
    private SpriteRenderer old_sprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this_door = GetComponent<GameObject>();
        old_sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject player = collision.gameObject;

        if (player.tag == "Player" && inventory.key_count > 0)
        {
            OpenDoor other_door_locked = other_door.GetComponent<OpenDoor>();
            SpriteRenderer other_old_sprite = other_door.GetComponent<SpriteRenderer>();

            if (locked) inventory.key_count--;
            locked = false;
            other_door_locked.locked = false;
            GetComponent<BoxCollider>().enabled = false;
            other_door.GetComponent<BoxCollider>().enabled = false;
            old_sprite.sprite = this_new_sprite;
            other_old_sprite.sprite = other_new_sprite;

            AudioSource.PlayClipAtPoint(door_sound, Camera.main.transform.position);
        }
    }
}
