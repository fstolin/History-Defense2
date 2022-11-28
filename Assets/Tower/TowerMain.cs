using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerMain : MonoBehaviour
{
    [SerializeField] int price = 75;

    BankHandler bank;

    public bool IsEnoughGoldForPlacement()
    {
        if (bank.CurrentBalance >= price)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool TowerPlacement(Transform position)
    {
        // Connect to bank
        bank = FindObjectOfType<BankHandler>();
        if (bank == null) return false;
        if (IsEnoughGoldForPlacement())
        {
            // Place tower
            Instantiate(this.gameObject, position.position, Quaternion.identity);
            // Withdraw gold
            bank.Withdraw(price);
            // Placement successful
            return true;
        }
        else
        {
            // Unsuccessful placement
            return false;
        }

    }
}
