using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[ExecuteInEditMode]
public class MIniMap : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
      MeshFilter filter = GetComponent<MeshFilter>();
      NavMeshTriangulation triangles = NavMesh.CalculateTriangulation();

      Mesh mesh = new Mesh();

      mesh.vertices = triangles.vertices;
      mesh.triangles = triangles.indices;

      filter.mesh = mesh;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
