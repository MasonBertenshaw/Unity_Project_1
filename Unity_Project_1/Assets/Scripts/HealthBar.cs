using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image slider;

    public void SetHealth(int Health)
    {
        slider.fillAmount = (float)Health / 100;
    }
}
