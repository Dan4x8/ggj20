using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bmanager : MonoBehaviour
{
    public TMPro.TMP_Text tmpText;
    void Awake()
    {

        tmpText.text = Weather.Current.Data.name + "; " + " " + Weather.Current.Data.sys.country + "\r\n"
            + "rain (mm/h): " + Weather.Current.Data.rain._1h;
    }
}
