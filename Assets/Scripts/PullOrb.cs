using UnityEngine;

public class PullOrb : MonoBehaviour
{
    public Transform casterTransform;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("pullable"))
        {
            Vector3 directionToPlayer = (casterTransform.position - other.transform.position).normalized;
            Vector3 moveDirection = GetOneUnitDirection(directionToPlayer);

            Vector3 targetPosition = other.transform.position + moveDirection;
            Collider[] colliders = Physics.OverlapBox(targetPosition, Vector3.one * 0.4f);

            foreach (Collider col in colliders)
            {
                if (col.gameObject.layer == LayerMask.NameToLayer("Water"))
                {
                    Debug.Log("Target tile is water — block will not move.");
                    Destroy(gameObject);
                    return;
                }
                if (col.CompareTag("Player"))
                {
                    Debug.Log("Target tile is occupied by the player — block will not move.");
                    Destroy(gameObject);
                    return;
                }
            }

            other.transform.position = targetPosition;
            Debug.Log(targetPosition);
        }
        if (other.CompareTag("enemy"))
        {
            Vector3 directionToPlayer = (casterTransform.position - other.transform.position).normalized;
            Vector3 pullDirection = GetOneUnitDirection(directionToPlayer);
            
            Vector3 pullTargetPosition = casterTransform.position - pullDirection;


            Collider[] colliders = Physics.OverlapBox(pullTargetPosition, Vector3.one * 0.4f);
            bool blocked = false;

            foreach (Collider col in colliders)
            {
                if (!col.isTrigger && col.gameObject != other.gameObject)
                {
                    blocked = true;
                    break;
                }
            }

            if (!blocked)
            {
                other.transform.position = pullTargetPosition;
                Debug.Log("Enemy pulled to: " + pullTargetPosition);
            }
            else
            {
                Debug.Log("Pull target blocked — enemy not moved.");
            }
        }


        Destroy(gameObject);
    }

    Vector3 GetOneUnitDirection(Vector3 input)
    {
        if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
            return new Vector3(Mathf.Sign(input.x), 0, 0);
        else
            return new Vector3(0, Mathf.Sign(input.y), 0);
    }
}