using System.Collections;
using UnityEngine;

public class AquamentusMovement : MonoBehaviour
{
    public float speed = 2f;
    public float moveDistance = 4f;
    public float pauseDuration = 1f;

    public GameObject projectilePrefab;
    public Transform shootPoint;
    public float projectileSpeed = 5f;
    public float spreadAngle = 15f;

    private Vector3 direction = Vector3.left;
    private float distanceTraveled = 0f;
    private bool isPaused = false;

    void Start()
    {
        direction = Vector3.left;
    }

    void Update()
    {
        if (isPaused) return;

        float moveStep = speed * Time.deltaTime;
        transform.position += direction * moveStep;
        distanceTraveled += moveStep;

        if (distanceTraveled >= moveDistance)
        {
            StartCoroutine(PauseAndFire());
        }
    }

    IEnumerator PauseAndFire()
    {
        isPaused = true;
        distanceTraveled = 0f;

        FireTripleShotLeft();

        yield return new WaitForSeconds(pauseDuration);

        direction = -direction;
        isPaused = false;
    }

    void FireTripleShotLeft()
    {
        if (projectilePrefab == null || shootPoint == null)
            return;

        Vector3 left = Vector3.left;
        Vector3 upAngle = Quaternion.Euler(0f, 0f, spreadAngle) * left;
        Vector3 downAngle = Quaternion.Euler(0f, 0f, -spreadAngle) * left;

        FireSingleShot(left);
        FireSingleShot(upAngle);
        FireSingleShot(downAngle);
    }

    void FireSingleShot(Vector3 dir)
    {
        Vector3 spawnPos = shootPoint.position + new Vector3(-1f, 0.5f, 0f);
        GameObject proj = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);

        Rigidbody rb = proj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = dir.normalized * projectileSpeed;
        }

        Collider projCol = proj.GetComponent<Collider>();
        Collider selfCol = GetComponent<Collider>();

        if (projCol != null && selfCol != null)
        {
            Physics.IgnoreCollision(projCol, selfCol);
        }
    }
}
