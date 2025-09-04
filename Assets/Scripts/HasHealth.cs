using UnityEngine;
using UnityEngine.SceneManagement;

public class HasHealth : MonoBehaviour
{
    public int health = 3;
    Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject collided = collision.gameObject;
        if (collided.tag == "enemy")
        {
            HitStun();
            health--;
            if (health == 0)
            {
                SceneManager.LoadScene("Main");
            }
        }
    }

    void HitStun()
    {
        
    }
}
