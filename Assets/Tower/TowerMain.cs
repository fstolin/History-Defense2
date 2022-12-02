using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerMain : MonoBehaviour
{
    [SerializeField] int price = 75;

    BankHandler bank;

    private void Start()
    {
        StartCoroutine(Build());
    }

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
            PlaceTower(position);
            // Placement successful
            return true;
        }
        else
        {
            // Unsuccessful placement
            return false;
        }

    }

    private void PlaceTower(Transform position)
    {
        // Place tower
        GameObject tower = Instantiate(this.gameObject, position.position, Quaternion.identity);

        // Withdraw gold
        bank.Withdraw(price);
    }

    IEnumerator Build()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
            foreach(Transform childd in child)
            {
                childd.gameObject.SetActive(false);
            }
        }

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);

            foreach (Transform childd in child)
            {
                childd.gameObject.SetActive(true);
            }
        }
    }
}
