using UnityEngine;

public class CreateCircleWithCollider : MonoBehaviour
{
    public Vector3 centerPoint;  // 圆形的圆心
    public Vector3 pointOnCircle;  // 圆形上的一点

    void Start()
    {
        CreateCircle();
    }

    void CreateCircle()
    {
        // 计算圆的半径
        float radius = Vector3.Distance(centerPoint, pointOnCircle);

        // 创建一个空GameObject来存放圆形
        GameObject circleObject = new GameObject("Circle");

        // 添加MeshFilter组件
        MeshFilter meshFilter = circleObject.AddComponent<MeshFilter>();

        // 创建圆形的顶点
        int segments = 32;  // 圆形的分段数，可以根据需要调整
        Vector3[] vertices = new Vector3[segments];

        for (int i = 0; i < segments; i++)
        {
            float angle = Mathf.PI * 2f * i / segments;
            vertices[i] = new Vector3(Mathf.Cos(angle) * radius, 0f, Mathf.Sin(angle) * radius) + centerPoint;
        }

        // 创建三角形面
        int[] triangles = new int[(segments - 2) * 3];

        for (int i = 0; i < segments - 2; i++)
        {
            triangles[i * 3] = 0;
            triangles[i * 3 + 1] = i + 1;
            triangles[i * 3 + 2] = i + 2;
        }

        // 创建新的网格并赋值顶点和三角形
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        // 计算法线（重要用于渲染）
        mesh.RecalculateNormals();

        // 将网格赋给MeshFilter组件
        meshFilter.mesh = mesh;

        // 添加MeshRenderer组件
        MeshRenderer meshRenderer = circleObject.AddComponent<MeshRenderer>();
        meshRenderer.material = new Material(Shader.Find("Standard"));

        // 添加MeshCollider来给予圆形碰撞体积
        MeshCollider meshCollider = circleObject.AddComponent<MeshCollider>();
        meshCollider.sharedMesh = mesh;
    }

    void OnDrawGizmos()
    {
        // 绘制圆形的边界线
        Gizmos.color = Color.green;
        float radius = Vector3.Distance(centerPoint, pointOnCircle);
        Vector3 direction = (pointOnCircle - centerPoint).normalized;
        Gizmos.DrawWireSphere(centerPoint, radius);
        Gizmos.DrawLine(centerPoint, centerPoint + direction * radius);
    }
}
