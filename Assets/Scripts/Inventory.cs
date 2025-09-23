using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    public int rupee_count = 0;
    public int key_count = 0;
    public List<string> altWeapons = new List<string>();
    private int current_wep = 0;
    public Text altWeaponText;
    public int bomb_count = 0;
    public HasHealth player;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
            if (!SetWindowedResolution.God_Mode)
            {
                SetWindowedResolution.God_Mode=true;
                GodMode();
            }
            else
            {
                SetWindowedResolution.God_Mode = false;
            }
           
            
        }
    }
    public void UpdateAltWeaponUI()
    {
        if (altWeaponText != null)
        {
            altWeaponText.text = "Alt Weapon: " + altWeapons[current_wep];
        }
        else
        {
            altWeaponText.text = "";
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
            player.health = 3;
            rupee_count += 1000;
            bomb_count += 1000;
            key_count+= 1000;
            if (!altWeapons.Contains("Bow"))
            {
                altWeapons.Add("Bow");
                UpdateAltWeaponUI();
            }
            if (!altWeapons.Contains("Boomerang"))
            {
                altWeapons.Add("Boomerang");
                UpdateAltWeaponUI();
            }
            if (!altWeapons.Contains("Bomb"))
            {
                altWeapons.Add("Bomb");
                UpdateAltWeaponUI();
            }

            if (!altWeapons.Contains("Pull Orb"))
            {
                altWeapons.Add("Pull Orb");
                UpdateAltWeaponUI();
            }}
    }
    public string GetCurrentWeapon()
    {
        return altWeapons.Count > 0 ? altWeapons[current_wep] : "";
    }
}
