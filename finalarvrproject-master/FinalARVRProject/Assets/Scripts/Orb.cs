using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Citation: Adapted from implementation of tutorial project 
public class Orb : MonoBehaviour
{
    public Vector3[] Vertices;
    public Color[] colors;
    public int[] indices;
    public List<Vector3> circVertices;
    public List<int> listIndices;
    public List<Color> colorsList;
    public Mesh selfMesh;
    // Start is called before the first frame update

    void MeshSetup()
    {
        float convphi = Mathf.PI / 100;
        float convtheta = 2 * Mathf.PI / 100;
        int i = 0;
        /*          x = cos θ sin φ
                    y = sin θ sin φ
                    z = cos φ*/

        circVertices = new List<Vector3>();
        listIndices = new List<int>();
        circVertices.Add(new Vector3(0, 0, 0));
        colorsList.Add(Color.green);
        for (float phi = 0; phi < Mathf.PI; phi += convphi)
        {
            for (float theta = 0; theta < 2 * Mathf.PI; theta += convtheta)
            {
                float x = Mathf.Cos(theta) * Mathf.Sin(phi);
                float y = Mathf.Sin(theta) * Mathf.Sin(phi);
                float z = Mathf.Cos(phi);
                circVertices.Add(new Vector3(x, y, z));
                listIndices.Add(i);
                colorsList.Add(Color.green);
                i++;
            }
        }
       //selfMesh.colors = colorsList.ToArray();
        Vertices = circVertices.ToArray();
        indices = listIndices.ToArray();
        colors = colorsList.ToArray();
    }
    void UpdateShape()
    {
        selfMesh.Clear();
        selfMesh.vertices = Vertices;
        selfMesh.colors = colors;
        selfMesh.SetIndices(indices, MeshTopology.Points, 0);
    }
    public void Start()
    {
        selfMesh = new Mesh();
        GetComponent<MeshFilter>().mesh = selfMesh;
        MeshSetup();
        UpdateShape();

    }

}
