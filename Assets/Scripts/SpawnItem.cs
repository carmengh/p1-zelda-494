using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject item;

    bool spawn = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!spawn)
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
                spawn = true;
            }
        }

        if (spawn) {
            AudioClip spawn_sound = Resources.Load<AudioClip>("Zelda/Audio/Sound Effect (19)");
            AudioSource.PlayClipAtPoint(spawn_sound, Camera.main.transform.position);
            if (item.tag == "PlayerBoomerang") item.layer = 0;
            Instantiate(item, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
