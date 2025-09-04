using UnityEngine;

public class RightDoorTrigger : MonoBehaviour
{
    public MoveCamera cameraController;

    private bool hasActivated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasActivated && other.CompareTag("Player"))
        {
            hasActivated = true;
            cameraController.StartCameraTransitionRight();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        new WaitForSeconds(10f);
        if (other.CompareTag("Player"))
        {
            hasActivated = false;
        }
    }
}