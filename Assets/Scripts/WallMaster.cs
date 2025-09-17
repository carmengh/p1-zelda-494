using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WallMaster : MonoBehaviour
{
    public GameObject player;
    public float move_horizontal = 2.5f;
    public float move_vertical = 4f;
    public bool right;
    public bool up;
    public bool start_on_side;  // indicate if wallmaster is on right/left walls or top/bottom walls

    Rigidbody rb;
    Vector3 wallmaster_spawn;
    Vector3 origin;
    Vector3 cam_start_pos;
    Vector3 spawn_point;
    float verticalDir;
    float horizontalDir;
    bool grabbed = false;
    bool moving = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        wallmaster_spawn = rb.position;
        origin = new Vector3(39.5f, 2.5f, 0);
        cam_start_pos = new Vector3(39.48f, 7.034f, -10f);
        spawn_point = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!moving)
        {
            moving = true;
            if (start_on_side)
            {
                StartCoroutine(EnterFromRightLeft());
            }
            else
            {
                StartCoroutine(EnterFromTopBottom());
            }
        }

        if (grabbed)
        {
            player.transform.position = transform.position;

            if (transform.position == spawn_point)
            {
                player.transform.position = origin;
                Camera.main.transform.position = cam_start_pos;
                grabbed = false;
                player.GetComponent<HasHealth>().health--;
                player.GetComponent<Movement>().canMove = true;
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject collided = other.gameObject;
        if (collided.tag == "Player" && !SetWindowedResolution.God_Mode)
        {
            grabbed = true;
            collided.GetComponent<Movement>().canMove = false;
        }
    }

    IEnumerator EnterFromRightLeft()
    {
        // enter room
        GetHorizontalDirection();
        Vector3 end_position = transform.position + new Vector3(horizontalDir, verticalDir);
        for (float t = 0; t < 1; t += Time.deltaTime)
        {
            rb.transform.position = Vector3.Lerp(wallmaster_spawn, end_position, t);
            yield return null;
        }

        // move within room
        GetVerticalDirection();
        Vector3 old_position = end_position;
        end_position = transform.position + new Vector3(horizontalDir, verticalDir);
        for (float t = 0; t < 1; t += Time.deltaTime)
        {
            rb.transform.position = Vector3.Lerp(old_position, end_position, t);
            yield return null;
        }

        // leave room
        GetHorizontalDirection();
        old_position = end_position;
        end_position = transform.position + new Vector3(-horizontalDir, verticalDir);
        for (float t = 0; t < 1; t += Time.deltaTime)
        {
            rb.transform.position = Vector3.Lerp(old_position, end_position, t);
            yield return null;
        }

        // reset wallmaster
        rb.transform.position = wallmaster_spawn;
        moving = false;
        if (!grabbed) gameObject.SetActive(false);
    }

    IEnumerator EnterFromTopBottom()
    {
        // enter room
        GetVerticalDirection();
        Vector3 end_position = transform.position + new Vector3(horizontalDir, verticalDir);
        for (float t = 0; t < 1; t += Time.deltaTime)
        {
            rb.transform.position = Vector3.Lerp(wallmaster_spawn, end_position, t);
            yield return null;
        }

        // move within room
        GetHorizontalDirection();
        Vector3 old_position = end_position;
        end_position = transform.position + new Vector3(horizontalDir, verticalDir);
        for (float t = 0; t < 1; t += Time.deltaTime)
        {
            rb.transform.position = Vector3.Lerp(old_position, end_position, t);
            yield return null;
        }

        // leave room
        GetVerticalDirection();
        old_position = end_position;
        end_position = transform.position + new Vector3(horizontalDir, -verticalDir);
        for (float t = 0; t < 1; t += Time.deltaTime)
        {
            rb.transform.position = Vector3.Lerp(old_position, end_position, t);
            yield return null;
        }

        // reset wallmaster
        rb.transform.position = wallmaster_spawn;
        moving = false;
        if (!grabbed) gameObject.SetActive(false);
    }

    void GetHorizontalDirection()
    {
        verticalDir = 0f;
        if (right)
        {
            horizontalDir = move_horizontal;
        }
        else
        {
            horizontalDir = move_horizontal * -1;
        }
    }

    void GetVerticalDirection()
    {
        horizontalDir = 0f;
        if (up)
        {
            verticalDir = move_vertical;
        }
        else
        {
            verticalDir = move_vertical * -1;
        }
    }
}
