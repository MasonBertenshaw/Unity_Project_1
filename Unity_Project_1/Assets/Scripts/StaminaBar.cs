using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    // Start is called before the first frame update

    //drag your Stamina bar UI in this
    public Image slider;

    public void SetStamina(int stamina)
    {
        slider.fillAmount = (float)stamina / 100;
    }
}