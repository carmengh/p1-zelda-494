using UnityEngine;
using UnityEngine.InputSystem;

public class DropItem : MonoBehaviour
{
    public GameObject heart;
    public GameObject rupee;
    public GameObject key;
    public bool has_key = false;
    public float drop_rate = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Drop()
    {
        if (has_key)
        {
            Instantiate(key, transform.position, transform.rotation);
            return;
        }

        bool drop_heart = true;

        // choose between dropping heart or rupee
        float choose_item = Random.Range(0.0f, 0.99f);
        if (choose_item < 0.5)
        {
            drop_heart = false;
        }

        // random drop item
        float randomized = Random.Range(0.0f, 0.99f);
        if (randomized < drop_rate)
        {
            if (drop_heart)
            {
                Instantiate(heart, transform.position, transform.rotation);
            }
            else
            {
                Instantiate(rupee, transform.position, transform.rotation);
            }
        }
    }
}
