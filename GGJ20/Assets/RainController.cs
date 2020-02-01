using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class RainController : MonoBehaviour
{
    VisualEffect vfx;
    public TreeController treeController;
    // Start is called before the first frame update
    void Start()
    {
        vfx = GetComponent<VisualEffect>();
    }

    // Update is called once per frame
    void Update()
    {
        vfx.SetFloat("RainIntensity", (float)Weather.Current.Data.rain._1h);
        treeController.AddWater(Time.deltaTime * (float)Weather.Current.Data.rain._1h);
    }
}
