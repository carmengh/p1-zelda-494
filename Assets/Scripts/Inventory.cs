using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    public int rupee_count = 0;
    public int key_count = 0;
    List<string> altWeapons = new List<string>();
    private int current_wep = 0;
    public Text altWeaponText;
    public int bomb_count = 0;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        altWeapons.Add("Bow");
        UpdateAltWeaponUI();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (altWeapons.Count > 0)
            {
                current_wep = (current_wep + 1) % altWeapons.Count;
                UpdateAltWeaponUI();
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetWindowedResolution.God_Mode=true;
            GodMode();
        }
    }
    void UpdateAltWeaponUI()
    {
        if (altWeaponText != null)
        {
            altWeaponText.text = "Alt Weapon: " + altWeapons[current_wep];
        }
    }

    public void AddInventory(ref int count_change, int num_added)
    {
        count_change += num_added;
    }

    public int GetRupees()
    {
        return rupee_count;
    }

    void GodMode()
    {
        if (SetWindowedResolution.God_Mode)
        {
            rupee_count += 1000;
            bomb_count += 1000;
            if (!altWeapons.Contains("Boomerang"))
            {
                altWeapons.Add("Boomerang");

            }
        }
    }
    public string GetCurrentWeapon()
    {
        return altWeapons.Count > 0 ? altWeapons[current_wep] : "";
    }
}
