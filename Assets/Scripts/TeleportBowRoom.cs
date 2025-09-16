using UnityEngine;

public class TeleportBowRoom : MonoBehaviour
{
    public bool teleport_to = false;
    public bool teleport_from = false;
    Vector3 camera_start = new Vector3(23.7f, 62f, -10f);
    Vector3 camera_bow_room = new Vector3(119.5f, 27f, -10f);
    Vector3 return_player = new Vector3(22f, 59.5f, 0f);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject player = other.gameObject;
        if (player.tag == "Player")
        {
            if (teleport_to)
            {
                player.GetComponent<Rigidbody>().transform.position = new Vector3(114.62f, 30f, 0f);
                Camera.main.transform.position = camera_bow_room;
            }
            if (teleport_from)
            {
                player.GetComponent<Rigidbody>().transform.position = return_player;
                Camera.main.transform.position = camera_start;
            }
        }
    }
}
