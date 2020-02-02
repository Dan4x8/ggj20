using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TreeController : MonoBehaviour
{
    public DataProvider dataProvider;

    [SerializeField]
    private Transform tree;

    [SerializeField]
    private float growth = 0;
    public float Growth { get { return growth; } }

    [SerializeField]
    private float water = 15;
    public float Water { get { return water; } }

    [SerializeField]
    private float waterLow = 10;
    [SerializeField]
    private float waterLowDead = 0;
    [SerializeField]
    private float waterHigh = 20;
    [SerializeField]
    private float waterHighDeath = 25;
    [SerializeField]
    private float waterBaseConsumption = 0.5f;
    private float waterGrowthConsumption = 0.5f;

    public float tempHigh = 40;
    public float tempHighDead = 50;
    public float tempLow = 15;
    public float tempLowDead = -100;
    public float tempWaterUsagePerDeg = .01f;
    public float temp;

    // Start is called before the first frame update
    void Start()
    {
        CreateTrunk();
        AssignMaterial(mat);
    }

    public void HeightenWaterResistanceHigh(float value)
    {
        waterHigh += value;
        waterHighDeath += value * 1.1f;
        waterBaseConsumption += value * 0.1f;
        waterLow += value * 0.11f;
        waterLowDead += value * 0.01f;
    }

    public void LowerWaterResistanceHigh(float value)
    {
        HeightenWaterResistanceHigh(value * -1);
    }

    public void HeightenWaterResistanceLow(float value)
    {
        waterLow -= value;
        waterLowDead -= value * 1.1f;
        waterBaseConsumption -= value * 0.095f;
        waterHigh -= value * 0.11f;
        waterHighDeath -= value * 0.01f;
    }

    public void LowerWaterResistanceLow(float value)
    {
        HeightenWaterResistanceLow(value * -1f);
    }

    public void HeightenTempHighResistance(float value)
    {
        tempHigh += value;
        tempHighDead += value * 1.1f;
        waterBaseConsumption += value * 0.1f;
        tempLow += value * 0.11f;
        tempLowDead += value * 0.01f;
    }

    public void HeightenGrowthPower(float value)
    {
        growth += value;
        waterBaseConsumption += value * 0.5f;
    }

    public void LowerGrowthPower(float value)
    {
        growth -= value;
        waterBaseConsumption -= value * 0.4f;
    }

    public void LowerTempHighResistance(float value)
    {
        HeightenTempHighResistance(value * -1f);  
    }

    public void HeightenTempLowResistance(float value)
    {
        tempLow -= value;
        tempLowDead -= value * 1.1f;
        waterBaseConsumption -= value * 0.095f;
        tempHigh -= value * 0.11f;
        tempHighDead -= value * 0.01f;
    }

    public void LowerTempLowResistance(float value)
    {
        HeightenTempLowResistance(value * -1f);
    }

    public Material mat;

    // Update is called once per frame
    void Update()
    {
        GRATE = GrowthRate();

        CauseDeath cd = CauseDeath.None;

        if (water <= waterLowDead)
            cd = CauseDeath.WaterLow;
        else if (water >= waterHighDeath)
            cd = CauseDeath.Waterhigh;
        else if (temp >= tempHighDead)
            cd = CauseDeath.TempHigh;
        else if (temp <= tempLowDead)
            cd = CauseDeath.TempLow;

        if (cd != CauseDeath.None)
            Die(cd);

        Grow(growth * GrowthRate() * Time.deltaTime, waterBaseConsumption * Time.deltaTime);
        if(temp >= 0)
            water -= tempWaterUsagePerDeg * Time.deltaTime * temp;
    }

    void Die(CauseDeath cd)
    {
        if (dataProvider == null)
            return;
        dataProvider.GetComponent<DataProvider>().cause = cd;
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
    }

    [SerializeField]
    private float GRATE;

    public float GrowthRate()
    {
        float avg = (waterLow + waterHigh) / 2f;
        return 1f - Mathf.Abs(water - avg) / (avg - waterLow);
    }

    void Grow(float rate, float baseConsumption)
    {
        water -= baseConsumption;
        if (rate <= 0)
            return;
        GrowTrunk(rate);
        water -= rate * waterGrowthConsumption;
    }

    public void SetTemp(float kelvin)
    {
        temp = kelvin - 273.15f;
    }
    public void AddWater(float amount)
    {
        water += amount;
    }

    //ProcStuff
    private int segments = 8;
    private int segment_height = 1;
    private float radius =1 ;
    private float length = 1;

    private Mesh trunk;
    private MeshFilter filter;
    private int vCols;
    private int vRows;

    public void AssignMaterial(Material mat)
    {
        MeshRenderer mr = gameObject.GetComponent<MeshRenderer>();
        mr.sharedMaterial = mat;
    }

    public void CreateTrunk()
    {
        trunk = new Mesh();
        trunk.name = "basetree";
        filter = gameObject.GetComponent<MeshFilter>();
        filter.mesh = trunk;

        this.vCols = segments + 1;
        vRows = segment_height + 1;
        var vCols = segments + 1;

        int nVerts = vCols * vRows;
        int numUVs = nVerts;
        int numSideTris = segments * segment_height * 2;
        int numCapTris = segments - 2;
        int trisArrayLength = (numSideTris + numCapTris * 2) * 3;

        Vector3[] verts = new Vector3[nVerts];
        Vector2[] uvs = new Vector2[numUVs];
        int[] tris = new int[trisArrayLength];

        float heightStep = length / segment_height;
        float angleStep = 2 * Mathf.PI / segments;
        float uvStepH = 1.0f / segments;
        float uvStepV = 1.0f / segment_height;

        for (int j = 0; j < vRows; j++)
        {
            for (int i = 0; i < vCols; i++)
            {
                float angle = i * angleStep;

                if (i == vCols - 1)
                {
                    angle = 0;
                }
                verts[j * vCols + i] = new Vector3(radius * Mathf.Cos(angle), j * heightStep, radius * Mathf.Sin(angle));

                uvs[j * vCols + i] = new Vector2(i * uvStepH, j * uvStepV);
                if (j == 0 || i >= vCols - 1)
                {
                    continue;
                }
                else
                {
                    int baseIndex = numCapTris * 3 + (j - 1) * segments * 6 + i * 6;

                    tris[baseIndex + 0] = j * vCols + i;
                    tris[baseIndex + 1] = j * vCols + i + 1;
                    tris[baseIndex + 2] = (j - 1) * vCols + i;

                    tris[baseIndex + 3] = (j - 1) * vCols + i;
                    tris[baseIndex + 4] = j * vCols + i + 1;
                    tris[baseIndex + 5] = (j - 1) * vCols + i + 1;
                }
            }
        }

        bool leftSided = true;
        int leftIndex = 0;
        int rightIndex = 0;
        int middleIndex = 0;
        int topCapVertexOffset = nVerts - vCols;
        for (int i = 0; i < numCapTris; i++)
        {
            int bottomCapBaseIndex = i * 3;
            int topCapBaseIndex = (numCapTris + numSideTris) * 3 + i * 3;

            if (i == 0)
            {
                middleIndex = 0;
                leftIndex = 1;
                rightIndex = vCols - 2;
                leftSided = true;
            }
            else if (leftSided)
            {
                middleIndex = rightIndex;
                rightIndex--;
            }
            else
            {
                middleIndex = leftIndex;
                leftIndex++;
            }
            leftSided = !leftSided;

            tris[bottomCapBaseIndex + 0] = rightIndex;
            tris[bottomCapBaseIndex + 1] = middleIndex;
            tris[bottomCapBaseIndex + 2] = leftIndex;

            tris[topCapBaseIndex + 0] = topCapVertexOffset + leftIndex;
            tris[topCapBaseIndex + 1] = topCapVertexOffset + middleIndex;
            tris[topCapBaseIndex + 2] = topCapVertexOffset + rightIndex;
        }

        trunk.vertices = verts;
        trunk.uv = uvs;
        trunk.triangles = tris;

        trunk.RecalculateNormals();
        trunk.RecalculateBounds();
        trunk.RecalculateTangents();
    }

    Vector2 segDir = new Vector2(0f,0f);
    public void GrowTrunk(float rate)
    {
        if (trunk == null)
            return;

        if(segTimer<= 0)
        {
            segTimer = Random.Range(0.2f, 0.5f);
            AddSegment();
            return;
        }
        var v = trunk.vertices;
        for(int i = v.Length-1; i >= v.Length - segments - 1;i--)
        {
            v[i] = new Vector3(v[i].x + segDir.x, v[i].y + rate, v[i].z + segDir.y);
        }
        trunk.SetVertices(v);
        trunk.RecalculateBounds();
        trunk.RecalculateNormals();
        segTimer -= rate;
    }

    private float segTimer = .4f;
    private int segs = 1;

    public void AddSegment()
    {
        segDir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        segDir *= 0.0005f;
        vRows++;
        var v = trunk.vertices;
        var tri = trunk.triangles;

        List<Vector3> vs = new List<Vector3>(v);
        
        for(int i = v.Length - segments -1; i < v.Length;i++)
        {
            vs.Add(new Vector3(v[i].x, v[i].y+.000001f, v[i].z));
        };

        int numSideTris = segments * ++segment_height * 2;
        int numCapTris = segments - 2;
        int trisArrayLength = (numSideTris + numCapTris * 2) * 3;

        int[] tris = new int[trisArrayLength];

        for (int j = 0; j < vRows; j++)
        {
            for (int i = 0; i < vCols; i++)
            {
                if (j == 0 || i >= vCols - 1)
                {
                    continue;
                }
                else
                {
                    int baseIndex = numCapTris * 3 + (j - 1) * segments * 6 + i * 6;

                    tris[baseIndex + 0] = j * vCols + i;
                    tris[baseIndex + 1] = j * vCols + i + 1;
                    tris[baseIndex + 2] = (j - 1) * vCols + i;

                    tris[baseIndex + 3] = (j - 1) * vCols + i;
                    tris[baseIndex + 4] = j * vCols + i + 1;
                    tris[baseIndex + 5] = (j - 1) * vCols + i + 1;
                }
            }
        }

        trunk.SetVertices(vs);
        trunk.SetTriangles(tris,0);
        trunk.RecalculateNormals();
        trunk.RecalculateBounds();
        trunk.RecalculateTangents();
    }
}
