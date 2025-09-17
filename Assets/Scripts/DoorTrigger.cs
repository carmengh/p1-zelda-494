using System;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public MoveCamera cameraController;

    public string type;
    private bool hasActivated = false;
    OpenDoor door;
    bool door_locked;

    private void Start()
    {
        door = GetComponent<OpenDoor>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (door == null)
        {
            door_locked = false;
        }
        else
        {
            door_locked = door.locked;
        }

        Debug.Log("has activated: " + hasActivated);
        if (!hasActivated && other.CompareTag("Player") && !door_locked)
        {
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
            hasActivated = false;
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        hasActivated = false;
    //    }
    //}
}