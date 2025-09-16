using UnityEngine;

public class PushableBlock : MonoBehaviour
{
    public float requiredPushTime = 1f;
    public Sprite newSprite;
    public Sprite otherNewSprite;
    public AudioClip swapSound;

    private float pushTimer = 0f;
    private bool hasSwapped = false;
    private Vector3 lastPushDirection;
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (hasSwapped || !collision.gameObject.CompareTag("Player"))
            return;

        Vector3 pushDirection = (transform.position - collision.transform.position).normalized;

        // Flatten direction to dominant axis (X or Y)
        if (Mathf.Abs(pushDirection.x) > Mathf.Abs(pushDirection.y))
            pushDirection = new Vector3(Mathf.Sign(pushDirection.x), 0f, 0f);
        else
            pushDirection = new Vector3(0f, Mathf.Sign(pushDirection.y), 0f);

        if (pushDirection != lastPushDirection)
        {
            pushTimer = 0f;
            lastPushDirection = pushDirection;
        }

        pushTimer += Time.deltaTime;

        if (pushTimer >= requiredPushTime)
        {
            TriggerSwap(pushDirection);
            hasSwapped = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            pushTimer = 0f;
            lastPushDirection = Vector3.zero;
        }
    }

    private void TriggerSwap(Vector3 pushDirection)
    {
        Debug.Log($"TriggerSwap called. Push direction: {pushDirection}");

        if (newSprite != null)
            sr.sprite = newSprite;

        Collider col = GetComponent<Collider>();
        if (col != null)
            col.enabled = false;

        GameObject otherBlock = GetBlockInDirection(pushDirection);

        if (otherBlock != null)
        {
            SpriteRenderer otherSR = otherBlock.GetComponent<SpriteRenderer>();
            if (otherNewSprite != null && otherSR != null)
            {
                otherSR.sprite = otherNewSprite;
                Debug.Log($"Swapped neighbor block '{otherBlock.name}' sprite.");
            }

            Collider otherCol = otherBlock.GetComponent<Collider>();
            if (otherCol != null)
            {
                otherCol.enabled = true;
                Debug.Log($"Enabled collider on neighbor block '{otherBlock.name}'.");
            }
        }
        else
        {
            Debug.LogWarning("No other block found in push direction.");
        }

        if (swapSound != null)
            AudioSource.PlayClipAtPoint(swapSound, Camera.main.transform.position);
    }

    private GameObject GetBlockInDirection(Vector3 direction)
    {
        Transform parent = transform.parent;
        if (parent == null)
        {
            Debug.LogWarning("No parent found for block.");
            return null;
        }

        Vector3 localPos = transform.localPosition;
        Vector3 offset = Vector3.zero;

        if (direction == Vector3.right)
            offset = Vector3.right;
        else if (direction == Vector3.left)
            offset = Vector3.left;
        else if (direction == Vector3.up)
            offset = Vector3.up;
        else if (direction == Vector3.down)
            offset = Vector3.down;
        else
        {
            Debug.LogWarning($"Invalid push direction: {direction}");
            return null;
        }

        Vector3 checkLocalPos = localPos + offset;

        foreach (Transform child in parent)
        {
            if (child == transform) continue;

            if (Vector3.Distance(child.localPosition, checkLocalPos) < 0.1f)
            {
                Debug.Log($"Found neighbor block '{child.name}' at local position {checkLocalPos}");
                return child.gameObject;
            }
        }

        Debug.Log("No neighbor block found at local position " + checkLocalPos);
        return null;
    }
}
