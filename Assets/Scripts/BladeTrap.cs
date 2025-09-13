using System.Collections;
using UnityEngine;

public class BladeTrap : MonoBehaviour
{
    Rigidbody rb;
    Vector3 origin;
    Ray up_ray;
    Ray down_ray;
    Ray left_ray;
    Ray right_ray;
    Vector3 direction;
    public float ray_distance = 7f;
    public float move_right = 5f;
    public float move_left = 5f;
    public float move_up = 3f;
    public float move_down = 3f;
    public float speed = 5f;
    bool moving = false;
    float move_distance = 3f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        origin = rb.position;
        Debug.Log("origin: " + origin);
        up_ray = new Ray(origin, transform.up);
        down_ray = new Ray(origin, -transform.up);
        left_ray = new Ray(origin, -transform.right);
        right_ray = new Ray(origin, transform.right);
        Debug.DrawRay(origin, direction);
    }

    // Update is called once per frame
    void Update()
    {
        // check up
        if (Physics.Raycast(up_ray, out RaycastHit hit_up, ray_distance) && (hit_up.collider.tag == "Player") && !moving)
        {
            move_distance = move_up;
            direction = transform.up;
            moving = true;
            StartCoroutine(MoveTrap());
        }

        // check down
        if (Physics.Raycast(down_ray, out RaycastHit hit_down, ray_distance) && (hit_down.collider.tag == "Player") && !moving)
        {
            move_distance = move_down;
            direction = -transform.up;
            moving = true;
            StartCoroutine(MoveTrap());
        }

        // check right
        if (Physics.Raycast(right_ray, out RaycastHit hit_right, ray_distance) && (hit_right.collider.tag == "Player") && !moving)
        {
            move_distance = move_right;
            direction = transform.right;
            moving = true;
            StartCoroutine(MoveTrap());
        }

        // check left
        if (Physics.Raycast(left_ray, out RaycastHit hit_left, ray_distance) && (hit_left.collider.tag == "Player") && !moving)
        {
            move_distance = move_left;
            direction = -transform.right;
            moving = true;
            StartCoroutine(MoveTrap());
        }
    }

    IEnumerator MoveTrap()
    {
        float duration = 1f;
        Vector3 end_position = origin + direction * move_distance;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            rb.transform.position = Vector3.Lerp(origin, end_position, t/duration);
            yield return null;
        }
        rb.transform.position = origin + direction * move_distance; // snap to destination

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            rb.transform.position = Vector3.Lerp(end_position, origin, t/duration);
            yield return null;
        }
        rb.transform.position = origin;

        moving = false;
        yield return null;
    }
}
