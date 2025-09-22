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
            }

            other.transform.position = targetPosition;
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