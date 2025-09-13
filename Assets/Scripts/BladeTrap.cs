using System.Collections;
using UnityEngine;

public class BladeTrap : MonoBehaviour
{
    Rigidbody rb;
    Vector3 origin;
    Ray ray;
    Vector3 direction;
    public float ray_distance = 7f;
    public float move_distance = 3f;
    public float speed = 5f;
    bool moving = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        origin = rb.position;
        Debug.Log("origin: " + origin);
        direction = transform.up;
        ray = new Ray(origin, direction);
        Debug.DrawRay(origin, direction);
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(ray, out RaycastHit hit, ray_distance) && (hit.collider.tag == "Player") && !moving)
        {
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
