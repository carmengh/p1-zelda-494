using UnityEngine;
using UnityEngine.UI;

public class RupeeDisplayer : MonoBehaviour
{
    public Inventory inventory;
    Text text_component;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text_component = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inventory != null && text_component != null)
        {
            text_component.text = "Rupees: " + inventory.GetRupees().ToString();
        }
    }
}
