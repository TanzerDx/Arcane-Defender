using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{

    [SerializeField] Image baseHealthBar;
    [SerializeField] Image playerHealthBar;

    public void ChangeHealthbar(string healthBar, float healthAmount)
    {
        if(healthBar == "player")
        {
            playerHealthBar.fillAmount = healthAmount / 10f;
        }

        else if(healthBar == "base")
        {
            baseHealthBar.fillAmount = healthAmount / 20f;
        }
    }
}
