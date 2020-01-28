﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text Health;
    public Text HungerLevel;


    public HealthSystem HealthSystem;
    public HungerSystem HungerSystem;


    public InventoryUI InventoryUI;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Health.text = "Health : "+HealthSystem.GetHealth().ToString();
        HungerLevel.text = "Hunger Level : " + HungerSystem.GetHungerLevel().ToString();
    }

    public void ToggleInventory()
    {
        InventoryUI.Toggle();
    }
}
