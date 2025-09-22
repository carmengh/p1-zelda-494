using UnityEngine;

public class PullableTrigger : MonoBehaviour
{
    public Vector3 triggerPosition;
    public float triggerRadius = 0.1f;
    public bool triggerOnce = true;

    public GameObject[] targetBlocks;
    public Sprite newSprite;

    private bool hasTriggered = false;

    void Update()
    {
        if (hasTriggered && triggerOnce) return;

        if (Vector3.Distance(transform.position, triggerPosition) <= triggerRadius)
        {
            TriggerEvent();
            hasTriggered = true;
        }
    }

    void TriggerEvent()
    {
        foreach (GameObject obj in targetBlocks)
        {
            SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
            if (sr != null && newSprite != null)
            {
                sr.sprite = newSprite;
            }
            
            Collider col = obj.GetComponent<Collider>();
            if (col != null)
            {
                col.enabled = false;
            }
        }

        Debug.Log("Trigger activated by pullable block.");
    }
}