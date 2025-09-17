using System;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public MoveCamera cameraController;
    public bool lock_room = false;
    public GameObject[] enemies;
    public GameObject door_close;
    public Sprite close_sprite;

    public string type;
    private bool hasActivated = false;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (lock_room)
        {
            int count = 0;
            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i] == null)
                {
                    count++;
                }
            }

            if (count == enemies.Length)
            {
                door_close.GetComponent<OpenDoor>().Open();
                lock_room = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject player = other.gameObject;
        OpenDoor door = GetComponent<OpenDoor>();
        bool door_locked = true;
        if (door == null)
        {
            door_locked = false;
        }
        else
        {
            door_locked = door.locked;
        }

        Debug.Log("has activated: " + hasActivated);
        Debug.Log("other tag: " + player.tag);
        Debug.Log("door locked: " + door_locked);
        if (!hasActivated && player.CompareTag("Player") && !door_locked)
        {
            Debug.Log("trigger");
            hasActivated = true;
            if (type == "east")
            {
                cameraController.StartCameraTransitionRight();

            }
            else if (type == "west")
            {
                cameraController.StartCameraTransitionLeft();
            }
            else if (type == "north")
            {
                cameraController.StartCameraTransitionUp();
            }
            else if (type == "south")
            {
                cameraController.StartCameraTransitionDown();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hasActivated = false;
            if (lock_room)
            {
                door_close.GetComponent<OpenDoor>().locked = true;
                door_close.GetComponent<SpriteRenderer>().sprite = close_sprite;
            }
        }
    }
}