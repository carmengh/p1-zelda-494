using UnityEngine;

public class AquamentusMovement : MonoBehaviour
{
    public float speed = 2f;
    public float moveDistance = 4f;

    private Vector3 startPos;
    private Vector3 direction = Vector3.left;
    private float distanceTraveled = 0f;

    void Start()
    {
        startPos = transform.position;
        direction = Vector3.left;
    }

    void Update()
    {
        float moveStep = speed * Time.deltaTime;
        transform.position += direction * moveStep;
        distanceTraveled += moveStep;

        if (distanceTraveled >= moveDistance)
        {
            direction = -direction;
            distanceTraveled = 0f;
        }
    }
}