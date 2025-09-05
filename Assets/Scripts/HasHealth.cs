using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HasHealth : MonoBehaviour
{
    public Movement movement;
    public StalfosMovement enemy_movement;
    public int health = 3;
    public float force = 4;
    Rigidbody rb;
    private bool can_hit = true;

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
        GameObject player = rb.gameObject;
        if (player.tag == "Player" && collided.tag == "enemy")
        {
            if (can_hit)
            {
                StartCoroutine(HitStun(collided));
            }
            
            if (health == 0)
            {
                StartCoroutine(Death(2));
            }
        }
        if (player.tag == "enemy" && collided.tag == "sword")
        {
            StartCoroutine(HitStun(collided));
            if (health == 0)
            {
                Destroy(player);
            }
        }
    }

    IEnumerator HitStun(GameObject collided)
    {
        health--;
        can_hit = false;

        // knockback
        Vector3 knockback = (rb.transform.position - collided.GetComponent<Rigidbody>().transform.position).normalized;
        Debug.Log(knockback);
        rb.AddForce(force * knockback, ForceMode.Impulse);

        // disable then reenable movement
        if (movement != null)
        {
            movement.canMove = false;
        }
        if (enemy_movement != null) {
            enemy_movement.can_move = false;
        }
        yield return new WaitForSeconds(1);
        if (movement != null)
        {
            movement.canMove = true;
        }
        if (enemy_movement != null) {
            enemy_movement.can_move = true;
        }
        can_hit = true;
    }

    IEnumerator Death(int sec_wait)
    {
        if (movement != null) {
            movement.canMove = false;
        }
        yield return new WaitForSeconds(sec_wait);
        SceneManager.LoadScene("Main");
    }
}
