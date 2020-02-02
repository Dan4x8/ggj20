using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class TempController : MonoBehaviour
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
		treeController.SetTemp((float)Weather.Current.Data.main.temp);
	}
}
