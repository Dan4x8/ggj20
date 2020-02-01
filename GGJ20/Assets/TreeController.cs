using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    [SerializeField]
    private Transform tree;

    [SerializeField]
    private float growth = 0;
    public float Growth { get; }

    [SerializeField]
    private float water = 15;
    public float Water { get; }

    [SerializeField]
    private float waterLow = 10;
    [SerializeField]
    private float waterLowDead = 0;
    [SerializeField]
    private float waterHigh = 20;
    [SerializeField]
    private float waterHighDeath = 25;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        GRATE = GrowthRate();
        Grow(growth * Time.deltaTime);

    }

    [SerializeField]
    private float GRATE;

    public float GrowthRate()
    {
        float avg = (waterLow + waterHigh) / 2f;
        return 1f - Mathf.Abs(water - avg) / (avg - waterLow);
    }

    void Grow(float rate)
    {
        tree.transform.position = new Vector3(tree.position.x, tree.position.y + rate, tree.position.z);
    }
}
