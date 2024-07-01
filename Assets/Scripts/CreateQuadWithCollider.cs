using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateQuadWithCollider : MonoBehaviour
{
    public Vector3[] cornerPoints = new Vector3[4];  // 四边形的四个顶点

    void Start()
    {
        CreateQuad();
    }

    void CreateQuad()
    {
        // 创建一个空GameObject来存放四边形
        GameObject quadObject = new GameObject("Quad");

        // 添加MeshFilter组件
        MeshFilter meshFilter = quadObject.AddComponent<MeshFilter>();

        // 创建四边形的顶点
        Vector3[] vertices = new Vector3[]
        {
            cornerPoints[0],  // 第一个顶点
            cornerPoints[1],  // 第二个顶点
            cornerPoints[2],  // 第三个顶点
            cornerPoints[3]   // 第四个顶点
        };

        // 定义四边形的三角形面
        int[] triangles = new int[]
        {
            0, 1, 2,  // 第一个三角形
            0, 2, 3   // 第二个三角形
        };

        // 创建新的网格并赋值顶点和三角形
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        // 计算法线（重要用于渲染）
        mesh.RecalculateNormals();

        // 将网格赋给MeshFilter组件
        meshFilter.mesh = mesh;

        // 添加MeshRenderer组件
        MeshRenderer meshRenderer = quadObject.AddComponent<MeshRenderer>();
        meshRenderer.material = new Material(Shader.Find("Standard"));

        // 添加MeshCollider来给予四边形碰撞体积
        MeshCollider meshCollider = quadObject.AddComponent<MeshCollider>();
        meshCollider.sharedMesh = mesh;
    }

    void OnDrawGizmos()
    {
        // 绘制四边形的边界线
        Gizmos.color = Color.green;
        Gizmos.DrawLine(cornerPoints[0], cornerPoints[1]);
        Gizmos.DrawLine(cornerPoints[1], cornerPoints[2]);
        Gizmos.DrawLine(cornerPoints[2], cornerPoints[3]);
        Gizmos.DrawLine(cornerPoints[3], cornerPoints[0]);
    }
}