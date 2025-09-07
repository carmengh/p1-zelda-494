using UnityEngine;

public class Arrow : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Arrow hit: " + other.name);
        // Destroy this arrow when it hits something
        Destroy(gameObject);
    }
}