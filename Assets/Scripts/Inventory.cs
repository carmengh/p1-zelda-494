using UnityEngine;

public class Inventory : MonoBehaviour
{
    int rupee_count = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddRupees(int num_rupees)
    {
        rupee_count += num_rupees;
        Debug.Log("rupees: " + rupee_count);
    }

    public int GetRupees()
    {
        return rupee_count;
    }
}
