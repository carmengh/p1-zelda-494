using System;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public MoveCamera cameraController;

    public string type;
    private bool hasActivated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasActivated && other.CompareTag("Player"))
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
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hasActivated = false;
        }
    }
}