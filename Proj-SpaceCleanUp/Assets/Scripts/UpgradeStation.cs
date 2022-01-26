using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeStation : MonoBehaviour
{
    [SerializeField]
    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   

    public void setStarterValues()
    {

    }

    public void UpgradeOxygen()
    {
        (int, int) priceAndAmmount = getUpgradeOxygenPriceAndAmmount(player.getOxygenUpgradeLevel);

        if (player.getCurrentMoney > priceAndAmmount.Item1)
        {
            player.PayMoney(priceAndAmmount.Item1);
            player.UpgradeOxygen(priceAndAmmount.Item2, player.getOxygenUpgradeLevel + 1);


        }

        Debug.Log("Oxygen upgraded!");

    }
    
    public void UpgradeBackPackSpace()
    {
        (int, int) priceAndAmmount = getUpgradeBackPackPriceAndAmmount(player.getBackPackSpaceUpgradeLevel);

        if (player.getCurrentMoney > priceAndAmmount.Item1)
        {
            player.PayMoney(priceAndAmmount.Item1);
            player.UpgradeBackPack(priceAndAmmount.Item1, player.getBackPackSpaceUpgradeLevel + 1);


        }

        Debug.Log("BackPackUpgraded!");

    }


    private (int,int) getUpgradeOxygenPriceAndAmmount(int currentLevel)
    {
        switch (currentLevel)
        {
            case 1:
                return (100, 130);
            case 2:
                return (200, 160);
            case 3:
                return (400, 200);
            case 4:
                return (700, 250);
            case 5:
                return (1000, 300);
            default:
                return (99999,99999);
        }
    }

    private (int, int) getUpgradeBackPackPriceAndAmmount(int currentLevel)
    {
        switch (currentLevel)
        {
            case 1:
                return (60, 15);
            case 2:
                return (100, 20);
            case 3:
                return (150, 25);
            case 4:
                return (200, 30);
            case 5:
                return (250, 40);
            default:
                return (99999, 99999);
        }
    }


}
