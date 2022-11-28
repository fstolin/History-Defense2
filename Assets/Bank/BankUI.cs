using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BankUI : MonoBehaviour
{
    BankHandler bank;
    TMP_Text textMesh;

    void Start()
    {
        bank = FindObjectOfType<BankHandler>();
    }

    public void DisplayBankBalance(int value)
    {
        textMesh = GetComponent<TMP_Text>();
        textMesh.text = "Gold: " + value.ToString();
    }

}
