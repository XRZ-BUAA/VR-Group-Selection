using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTriangleWithCollider : MonoBehaviour
{
    public Vector3 pointA;
    public Vector3 pointB;
    public Vector3 pointC;

    void Start()
    {
        // Create a mesh filter and renderer to visualize the triangle
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.material = new Material(Shader.Find("Standard"));

        // Create vertices for the triangle
        Vector3[] vertices = new Vector3[] { pointA, pointB, pointC };

        // Create triangles (indices)
        int[] triangles = new int[] { 0, 1, 2 };

        // Create a new mesh and assign vertices and triangles
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        // Calculate normals (important for rendering)
        mesh.RecalculateNormals();

        // Assign the mesh to the mesh filter
        meshFilter.mesh = mesh;

        // Add a MeshCollider to give the triangle collision volume
        MeshCollider meshCollider = gameObject.AddComponent<MeshCollider>();
        meshCollider.sharedMesh = mesh;
    }

    void OnDrawGizmos()
    {
        // Draw lines to visualize the triangle in the scene view
        Gizmos.color = Color.red;
        Gizmos.DrawLine(pointA, pointB);
        Gizmos.DrawLine(pointB, pointC);
        Gizmos.DrawLine(pointC, pointA);
    }
}