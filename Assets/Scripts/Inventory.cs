using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int rupee_count = 0;
    public int key_count = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddInventory(ref int count_change, int num_added)
    {
        count_change += num_added;
    }

    public int GetRupees()
    {
        return rupee_count;
    }
}
