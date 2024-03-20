using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bank : MonoBehaviour
{
    [SerializeField] int startingCrystalBalance = 113;
    [SerializeField] int startingResourceBalance = 114;
    
    [SerializeField] int currentCrystalBalance;
    [SerializeField] int currentResourceBalance;

    [SerializeField] TextMeshProUGUI displayCrystalBalance;
    [SerializeField] TextMeshProUGUI displayResourceBalance;

    public int GetCurrentCrystalBalance { get {return currentCrystalBalance;}}
    public int GetCurrentResourceBalance { get {return currentResourceBalance;}}
    
    void Awake() {
        currentCrystalBalance = startingCrystalBalance;
        currentResourceBalance = startingResourceBalance;

        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        displayCrystalBalance.text = currentCrystalBalance.ToString();
        displayResourceBalance.text = currentResourceBalance.ToString();
    }

    public void Deposit(int crystalsReward, int resourceReward)
    {
        currentCrystalBalance += Mathf.Abs(crystalsReward);
        currentResourceBalance += Mathf.Abs(resourceReward);

        UpdateDisplay();
    }

        public void Withdraw(int crystalsAmount, int resourceAmount)
    {
        currentCrystalBalance -= Mathf.Abs(crystalsAmount);
        currentResourceBalance -= Mathf.Abs(resourceAmount);

        UpdateDisplay();
    }
}
