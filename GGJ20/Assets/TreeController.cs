using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TreeController : MonoBehaviour
{
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

    // Start is called before the first frame update
    void Start()
    {
        CreateTrunk();
        AssignMaterial(mat);
    }

    public Material mat;

    // Update is called once per frame
    void Update()
    {
        GRATE = GrowthRate();
        Grow(growth * GrowthRate() * Time.deltaTime, waterBaseConsumption * Time.deltaTime);

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
        GrowTrunk(rate);
        return;
        water -= baseConsumption;
        if (rate <= 0)
            return;
        tree.transform.position = new Vector3(tree.position.x, tree.position.y + rate, tree.position.z);
        water -= rate * waterGrowthConsumption;
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
        var vCols = segments + 1;  //+1 for welding

        //calculate sizes
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

            //assign bottom tris
            tris[bottomCapBaseIndex + 0] = rightIndex;
            tris[bottomCapBaseIndex + 1] = middleIndex;
            tris[bottomCapBaseIndex + 2] = leftIndex;

            //assign top tris
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
    bool done = false;
    public void GrowTrunk(float rate)
    {
        if (trunk == null ||done)
            return;

        if(segTimer<= 0)
        {
            //segTimer = 5f;
            done = true;
            AddSegment();
            return;
        }
        var v = trunk.vertices;
        for(int i = v.Length-1; i >= v.Length - segments - 1;i--)
        {
            v[i] = new Vector3(v[i].x, v[i].y + rate, v[i].z);
        }
        trunk.SetVertices(v);
        trunk.RecalculateBounds();
        segTimer -= rate;
    }

    private float segTimer = 1f;

    public void AddSegment()
    {
        var v = trunk.vertices;
        //        var uv = trunk.uv;
        var tri = trunk.triangles;

        List<Vector3> vs = new List<Vector3>(v);
        for (int i = v.Length - 1; i >= v.Length - segments - 1; i--)
        {
            vs.Add(new Vector3(v[i].x, v[i].y + .01f, v[i].z));
        };

        List<int> tris = new List<int>(tri);
        for(int i = 0; i < vCols;i++)
        {
            tris.Add(0);
        }
        for(int j = 1; j<=1;j++)
        {
            for (int i = 0; i < vCols; i++)
            {
                int k = j + v.Length / 18;

                if (k == v.Length/18 || i >= vCols - 1 - 3)
                {
                    continue;
                }
                else
                {
                    int baseIndex = (k - 1) * segments * 6 + i * 6;


                    Debug.Log(baseIndex + 5 + " of " + tris.Count);

                    tris[baseIndex + 0] = k * vCols + i;
                    tris[baseIndex + 1] = k * vCols + i + 1;
                    tris[baseIndex + 2] = (k - 1) * vCols + i;

                    tris[baseIndex + 3] = (k - 1) * vCols + i;
                    tris[baseIndex + 4] = k * vCols + i + 1;
                    tris[baseIndex + 5] = (k - 1) * vCols + i + 1;
                }
            }
        }




        trunk.SetVertices(vs);
        trunk.SetTriangles(tri,0);
        trunk.RecalculateNormals();
        trunk.RecalculateBounds();
        trunk.RecalculateTangents();
    }
}
