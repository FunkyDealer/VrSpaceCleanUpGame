using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeStation : MonoBehaviour
{
    [SerializeField]
    private PlayerController player;

    [SerializeField]
    private List<(int, int)> oxygenPriceEAmmount;
    [SerializeField]
    private List<(int, int)> backPackPriceEAmmount;

    private int maxLevel = 6;


    //Text meshes
    [Header("Upgrades")]
    [Header("Label Text meshes")]
    [SerializeField]
    private TMP_Text oxygenPriceLabel;
    [SerializeField]
    private TMP_Text backPackPriceLabel;

    [Header("Button Text meshes")]
    [SerializeField]
    private TMP_Text oxygenUpgradeButtonText;
    [SerializeField]
    private TMP_Text backpackUpgradeButtonText;

    [Header("Repair")]
    [Header("Label text meshes")]
    [SerializeField]
    private TMP_Text HealthRepairPriceLabel;

    [Header("Button text meshes")]
    [SerializeField]
    private TMP_Text HealthRepairButton;

    [SerializeField]
    AudioSource clickSound;

    void Awake()
    {      
        //Upgrades
        initiateBackPackPriceList();
        initiateOxygenPriceList();

        UpdateEverything();
    }

    void OnEnable()
    {
        UpdateEverything();


    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayClick()
    {
        clickSound.Play();
    }

    public void UpdateUpgradeMenu()
    {     
        UpdateUpgradePrices();
        UpdateUpgradeButtons();
    }

    public void UpdateRepairMenu()
    {
        updateHealthButton();
        updateHealthLabelPrice();
    }

    public void UpdateEverything()
    {
        UpdateRepairMenu();
        UpdateUpgradeMenu();
    }

    //oxygen and backpack upgrades
    #region Upgrades
    public void UpgradeOxygen()
    {
        if (player.getOxygenUpgradeLevel < 6)
        {
            (int, int) priceAndAmmount = oxygenPriceEAmmount[player.getOxygenUpgradeLevel - 1];

            if (player.CurrentMoney >= priceAndAmmount.Item1)
            {
                player.PayMoney(priceAndAmmount.Item1);
                player.UpgradeOxygen(priceAndAmmount.Item2, player.getOxygenUpgradeLevel + 1);
            }
            else
            {

            }
        }

        UpdateUpgradeMenu();
    }
    
    public void UpgradeBackPackSpace()
    {
        if (player.getBackPackSpaceUpgradeLevel < 6)
        {
            (int, int) priceAndAmmount = backPackPriceEAmmount[player.getBackPackSpaceUpgradeLevel - 1];

            if (player.CurrentMoney >= priceAndAmmount.Item1)
            {
                player.PayMoney(priceAndAmmount.Item1);
                player.UpgradeBackPack(priceAndAmmount.Item2, player.getBackPackSpaceUpgradeLevel + 1);


                UpdateUpgradePrices();
            }
            else
            {

            }
        }

        UpdateUpgradePrices();
        UpdateUpgradeButtons();
    }

    //initiate the oxygen upgrade prices and list
    private void initiateOxygenPriceList()
    {
        oxygenPriceEAmmount = new List<(int, int)>();

        //Price || ammount
        oxygenPriceEAmmount.Add((100, 120)); //level 2
        oxygenPriceEAmmount.Add((200, 160)); //level 3
        oxygenPriceEAmmount.Add((400, 200)); //level 4
        oxygenPriceEAmmount.Add((700, 250)); //level 5
        oxygenPriceEAmmount.Add((1000, 300)); //level 6

        //for test purposes, money cost is at 0
        //oxygenPriceEAmmount.Add((0, 120)); //level 2
        //oxygenPriceEAmmount.Add((0, 160)); //level 3
        //oxygenPriceEAmmount.Add((0, 200)); //level 4
        //oxygenPriceEAmmount.Add((0, 250)); //level 5
        //oxygenPriceEAmmount.Add((0, 300)); //level 6
    }

    //initiate the back pack upgrade prices and ammount list
    private void initiateBackPackPriceList()
    { 
        backPackPriceEAmmount = new List<(int, int)>();

        //Price || Ammount 
        backPackPriceEAmmount.Add((60, 15)); //level 2
        backPackPriceEAmmount.Add((100, 20)); //level 3
        backPackPriceEAmmount.Add((150, 25)); //level 4
        backPackPriceEAmmount.Add((200, 30)); //level 5
        backPackPriceEAmmount.Add((250, 40)); //level 6       

        //for test Purposes, money cost is at 0
        //backPackPriceEAmmount.Add((0, 15)); //level 2
        //backPackPriceEAmmount.Add((0, 20)); //level 3
        //backPackPriceEAmmount.Add((0, 25)); //level 4
        //backPackPriceEAmmount.Add((0, 30)); //level 5
        //backPackPriceEAmmount.Add((0, 40)); //level 6   
    }

    //update the upgrade button's text display
    private void UpdateUpgradeButtons()
    {
        int playerOxygenLevel = player.getOxygenUpgradeLevel;
        int playerbackPackLevel = player.getBackPackSpaceUpgradeLevel;
        int playerMoney = player.CurrentMoney;

        if (playerOxygenLevel < 6)
        {
            if (playerMoney >= oxygenPriceEAmmount[playerOxygenLevel -1].Item1)
            {
                oxygenUpgradeButtonText.text = "Upgrade";
            } 
            else
            {
                oxygenUpgradeButtonText.text = "Insuficient funds";
            }
        }
        else
        {
            oxygenUpgradeButtonText.text = "Max Level Reached!";
        }


        if (playerbackPackLevel < 6)
        {
            if (playerMoney >= backPackPriceEAmmount[playerbackPackLevel - 1].Item1)
            {
                backpackUpgradeButtonText.text = "Upgrade";
            }
            else
            {
                backpackUpgradeButtonText.text = "Insuficient funds";
            }
        }
        else
        {
            backpackUpgradeButtonText .text = "Max Level!";
        }

    }

    //update the prices Display
    private void UpdateUpgradePrices()
    {
        int playerOxygenLevel = player.getOxygenUpgradeLevel;
        int playerbackPackLevel = player.getBackPackSpaceUpgradeLevel;

        if (playerOxygenLevel < 6) //if player hasn't reached max oxygen upgrade level
        {
            oxygenPriceLabel.text = oxygenPriceEAmmount[playerOxygenLevel - 1].Item1.ToString() + "$";
        }
        else
        {
            oxygenPriceLabel.text = "";
        }


        if (playerbackPackLevel < 6) //if player hasn't reached max backpack upgrade level
        {
            backPackPriceLabel.text = backPackPriceEAmmount[playerbackPackLevel - 1].Item1.ToString() + "$";
        }
        else
        {
            backPackPriceLabel.text = "";
        }
    }
    #endregion


    #region Repair
    public void repairHealth()
    {        
        int currentMoney = player.CurrentMoney;
        int price = calculateHealthRepairPrice();        

        if (price <= currentMoney)
        {
            Debug.Log($"Paid {price}");
            if (price > 0)
            {
                player.PayMoney(price);
                player.RecoverHealth();
            }

            UpdateRepairMenu();
        }
        else
        {
        }

    }

    int calculateHealthRepairPrice()
    {
        int currentHealth = (int)player.CurrentHealth;
        int maxHealth = player.MaxHealth;
        int price = 0;

        if (currentHealth < maxHealth)
        {
            int healthDifference = maxHealth - currentHealth;

            price = healthDifference / 2;
        }

        return price;
    }

    void updateHealthLabelPrice()
    {
        int price = calculateHealthRepairPrice();

        if (price < 0) //if player hasn't reached max oxygen upgrade level
        {
            HealthRepairPriceLabel.text = price + "$";
        }
        else
        {
            HealthRepairPriceLabel.text = "";
        }
    }

    void updateHealthButton()
    {
        int price = calculateHealthRepairPrice();

        if (price > 0)
        {
            if (price > player.CurrentMoney)
            {
                HealthRepairButton.text = "Insuficient funds";
            }
            else
            {
                HealthRepairButton.text = "Repair";
            }
        }
        else
        {
            HealthRepairButton.text = "Health full";
        }

    }

    #endregion



}
