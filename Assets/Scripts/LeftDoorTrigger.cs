using UnityEngine;

public class LeftDoorTrigger : MonoBehaviour
{
    public MoveCamera cameraController;

    private bool hasActivated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasActivated && other.CompareTag("Player"))
        {
            hasActivated = true;
            cameraController.StartCameraTransitionLeft();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        new WaitForSeconds(2f);

        if (other.CompareTag("Player"))
        {
            hasActivated = false;
        }
    }
}