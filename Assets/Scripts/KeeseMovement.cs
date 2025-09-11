using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class KeeseMovement : MonoBehaviour
{
    public float speed = 2f;
    public float move_interval = 0.5f;
    public float pause_interval = 10f;
    public bool can_move = true;

    Rigidbody rb;
    float move_timer = 0f;
    float pause_timer = 0f;
    float curr_speed = 0f;
    private Vector2 currentDirection = Vector2.zero;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        move_timer = move_interval;
        pause_timer = pause_interval;
        curr_speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        move_timer -= Time.deltaTime;
        pause_timer -= Time.deltaTime;

        if (pause_timer <= 0f) {
            can_move = false;
            StartCoroutine(Pause());
        }

        if (can_move) {
            if (move_timer <= 0f)
            {
                PickNewDirection();
                move_timer = move_interval;
            }

            ChangeSpeed();
            rb.linearVelocity = currentDirection * curr_speed;
        }
    }
    void PickNewDirection()
    {
        int dir = Random.Range(0, 4);
        switch (dir)
        {
            case 0: currentDirection = Vector2.up; break;
            case 1: currentDirection = Vector2.down; break;
            case 2: currentDirection = Vector2.left; break;
            case 3: currentDirection = Vector2.right; break;
        }
    }

    IEnumerator Pause()
    {
        yield return new WaitForSeconds(2);
        pause_timer = pause_interval;
        curr_speed = speed;
        can_move = true;
    }

    void ChangeSpeed()
    {
        if (pause_timer <= (pause_interval / 2))
        {
            // slow down keese
            curr_speed -= 0.05f;
        }
        else
        {
            // increase keese speed
            curr_speed += 0.025f;
        }
    }
}
