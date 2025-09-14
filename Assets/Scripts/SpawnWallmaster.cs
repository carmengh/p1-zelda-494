using UnityEngine;

public class SpawnWallmaster : MonoBehaviour
{
    public GameObject wallmaster;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        GameObject collided = other.gameObject;
        if (collided.tag == "Player")
        {
            wallmaster.SetActive(true);
        }
    }
}
