using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BankHandler : MonoBehaviour
{
    [SerializeField] int startingBalance = 150;
    [SerializeField] int currentBalance;

    BankUI bankUI;

    public int CurrentBalance { get { return currentBalance; }  }

    private void Awake()
    {
        currentBalance = startingBalance;
        bankUI = FindObjectOfType<BankUI>();
        Debug.Log(bankUI);
        bankUI.DisplayBankBalance(currentBalance);
    }

    private void Start()
    {
        
    }


    // Deposit to the bank
    public void Deposit(int amount)
    {
        currentBalance += Mathf.Abs(amount);
        bankUI.DisplayBankBalance(currentBalance);
    }

    // Withdraw from the bank
    public void Withdraw(int amount)
    {
        currentBalance -= Mathf.Abs(amount);
        bankUI.DisplayBankBalance(currentBalance);
        HandleEndGame();            
    }

    // Restart the scene when player runs out of gold
    private void HandleEndGame()
    {
        if (currentBalance < 0) SceneManager.LoadScene(0);
    }
}
