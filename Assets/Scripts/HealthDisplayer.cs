using UnityEngine;
using UnityEngine.UI;

public class HealthDisplayer : MonoBehaviour
{
    public HasHealth health;
    Text health_text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health_text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health != null && health_text != null)
        {
            health_text.text = "Health: " + health.health.ToString();
        }
    }
}
