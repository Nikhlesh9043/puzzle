using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class CustomMeshGenerator : MonoBehaviour
{
    private static Mesh mesh;

    static Vector3[] meshVertices;
    static int[] meshTriangles;

    // Start is called before the first frame update
    void Awake()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        //UpdateMesh("small");
    }

    public static void CreateShape(string size)
    {
        if (size == "small")
        {
            meshVertices = new Vector3[] {
            new Vector3(-50,0,50),
            new Vector3(-5.5f,0,50),
            new Vector3(5.5f,0,50),
            new Vector3(50,0,50),

            new Vector3(-50,0,5.5f),
            new Vector3(-5.5f,0,5.5f),
            new Vector3(5.5f,0,5.5f),
            new Vector3(50,0,5.5f),

            new Vector3(-50,0,-5.5f),
            new Vector3(-5.5f,0,-5.5f),
            new Vector3(5.5f,0,-5.5f),
            new Vector3(50,0,-5.5f),

            new Vector3(-50,0,-50),
            new Vector3(-5.5f,0,-50),
            new Vector3(5.5f,0,-50),
            new Vector3(50,0,-50),

            new Vector3(-5.5f,-2,5.5f),
            new Vector3(5.5f,-2,5.5f),

            new Vector3(-5.5f,-2,5.5f),
            new Vector3(-5.5f,-2,-5.5f),
            };
        }
        else if (size == "large")
        {
            meshVertices = new Vector3[] {
            new Vector3(-50,0,50),
            new Vector3(-5.5f,0,50),
            new Vector3(5.5f,0,50),
            new Vector3(50,0,50),

            new Vector3(-50,0,8.5f),
            new Vector3(-5.5f,0,8.5f),
            new Vector3(5.5f,0,8.5f),
            new Vector3(50,0,8.5f),

            new Vector3(-50,0,-8.5f),
            new Vector3(-5.5f,0,-8.5f),
            new Vector3(5.5f,0,-8.5f),
            new Vector3(50,0,-8.5f),

            new Vector3(-50,0,-50),
            new Vector3(-5.5f,0,-50),
            new Vector3(5.5f,0,-50),
            new Vector3(50,0,-50),

            new Vector3(-5.5f,-2,8.5f),
            new Vector3(5.5f,-2,8.5f),

            new Vector3(-5.5f,-2,8.5f),
            new Vector3(-5.5f,-2,-8.5f),
            };
        }

        meshTriangles = new int[]
        {
            //Left Box
            0,1,13,
            0,13,12,
            //Right Box
            2,3,15,
            2,15,14,

            //Top Box
            1,2,6,
            6,5,1,
            //Bottom Box
            9,10,14,
            9,14,13,

            //Top Edge
            5,6,17,
            5,17,16,

            //Left Edge
            5,16,19,
            5,19,9
        };
    }

    public static void UpdateMesh(string size)
    {
        CreateShape(size);

        mesh.Clear();
        mesh.vertices = meshVertices;
        mesh.triangles = meshTriangles;

        mesh.RecalculateNormals();
    }

}
