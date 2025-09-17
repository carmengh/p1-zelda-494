using System.Collections;
using UnityEngine;

public class GoriyaAttack : MonoBehaviour
{
    [Header("Boomerang Settings")]
    public GameObject boomerangPrefab;
    public Sprite[] boomerangSpinSprites;
    public float attackInterval = 4f;
    public Transform boomerangSpawnPoint;

    private float attackTimer = 0f;
    private bool boomerangOut = false;

    private EnemyMovement enemyMovement;

    void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        attackTimer = attackInterval;
    }

    void Update()
    {
        if (enemyMovement == null || !enemyMovement.can_move || boomerangOut)
            return;

        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0f)
        {
            FireBoomerang();
            attackTimer = attackInterval;
        }
    }

    void FireBoomerang()
    {
        if (boomerangPrefab == null || boomerangSpinSprites == null || boomerangSpinSprites.Length == 0)
        {
            Debug.LogWarning("Boomerang prefab or spin sprites not assigned.");
            return;
        }

        Vector3 direction = enemyMovement.lastDirection;
        if (direction == Vector3.zero) return;

        Vector3 spawnPosition = (boomerangSpawnPoint != null) ? boomerangSpawnPoint.position : transform.position;

        GameObject boomerangInstance = Instantiate(boomerangPrefab, spawnPosition, Quaternion.identity);

        Boomerang boomerangScript = boomerangInstance.GetComponent<Boomerang>();
        if (boomerangScript != null)
        {
            boomerangScript.Initialize(transform, direction, boomerangSpinSprites);
            boomerangOut = true;

            // Disable Goriya movement while boomerang is active
            enemyMovement.can_move = false;

            // Wait until boomerang returns
            StartCoroutine(WaitForBoomerangReturn(boomerangInstance));
        }
        else
        {
            Debug.LogError("Boomerang prefab is missing the Boomerang script!");
        }
    }

    private IEnumerator WaitForBoomerangReturn(GameObject boomerang)
    {
        while (boomerang != null)
        {
            yield return null;
        }

        // Boomerang has returned or been destroyed
        boomerangOut = false;
        enemyMovement.can_move = true;
    }
}
