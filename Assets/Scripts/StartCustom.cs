using UnityEngine;

public class StartCustom : MonoBehaviour
{
    public GameObject player;
    Vector3 player_old_position;
    Vector3 camera_old_position;
    Vector3 custom_level_position = new Vector3(80f, 123f, 0f);
    Vector3 camera_custom_level_position = new Vector3(84f, 126.5f, -10f);
    bool in_custom = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (!in_custom)
            {
                player_old_position = player.transform.position;
                camera_old_position = Camera.main.transform.position;
                player.transform.position = custom_level_position;
                Camera.main.transform.position = camera_custom_level_position;
                in_custom = true;
            }
            else
            {
                player.transform.position = player_old_position;
                Camera.main.transform.position = camera_old_position;
                in_custom = false;
            }
        }
    }
}
