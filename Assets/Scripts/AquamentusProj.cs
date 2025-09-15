using UnityEngine;

public class AquamentusProj : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Aquamentus hit: " + other.name);
        // Destroy this arrow when it hits something
        Destroy(gameObject);
    }
}
