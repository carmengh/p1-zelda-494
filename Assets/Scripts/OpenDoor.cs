using System;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public bool locked = true;
    public Inventory inventory;
    public Sprite this_new_sprite;
    public Sprite other_new_sprite;
    public GameObject other_door;
    public AudioClip door_sound;
    public bool key_can_open = true;

    private Collider door_collider;
    private Collider other_door_collider;
    private GameObject this_door;
    private SpriteRenderer old_sprite;
    OpenDoor other_door_locked;
    SpriteRenderer other_old_sprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this_door = GetComponent<GameObject>();
        old_sprite = GetComponent<SpriteRenderer>();
        door_collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        GameObject player = other.gameObject;
        bool player_check = player.tag == "Player" && inventory.key_count > 0 && key_can_open;

        if (player_check || SetWindowedResolution.God_Mode)
        {
            if (locked)
            {
                Open();
            }
        }
    }

    public void Open()
    {
        if (!SetWindowedResolution.God_Mode && key_can_open)
        {
            inventory.key_count--;
        }

        locked = false;
        
        old_sprite.sprite = this_new_sprite;
        if (door_collider != null)
            door_collider.enabled = false;
        
        if (other_door != null)
        {
            other_door_locked = other_door.GetComponent<OpenDoor>();
            other_old_sprite = other_door.GetComponent<SpriteRenderer>();
            other_door_collider = other_door.GetComponent<Collider>();

            if (other_door_locked != null)
                other_door_locked.locked = false;

            if (other_old_sprite != null)
                other_old_sprite.sprite = other_new_sprite;

            if (other_door_collider != null)
                other_door_collider.enabled = false;
        }

        AudioSource.PlayClipAtPoint(door_sound, Camera.main.transform.position);
    }
}
