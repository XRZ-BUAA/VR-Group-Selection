using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLineSegmentWithCollider : MonoBehaviour
{
    public Vector3 startPoint;  // 线段的起点
    public Vector3 endPoint;    // 线段的终点

    void Start()
    {
        CreateLineSegment();
    }

    void CreateLineSegment()
    {
        // 计算线段的长度
        float length = Vector3.Distance(startPoint, endPoint);

        // 创建一个空GameObject来存放线段
        GameObject lineObject = new GameObject("LineSegment");

        // 添加LineRenderer组件
        LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();

        // 设置线段的位置和宽度
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, startPoint);
        lineRenderer.SetPosition(1, endPoint);
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;

        // 设置线段的材质和颜色（这里使用默认的白色材质）
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.white;

        // 添加BoxCollider来给予线段碰撞体积
        BoxCollider boxCollider = lineObject.AddComponent<BoxCollider>();

        // 计算盒子碰撞器的尺寸和中心点
        boxCollider.size = new Vector3(length, 0.1f, 0.1f);  // 根据实际需要调整高度和宽度
        boxCollider.center = (startPoint + endPoint) / 2f;

        // 计算盒子碰撞器的旋转（使其与线段方向一致）
        Vector3 direction = endPoint - startPoint;
        boxCollider.transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
    }

    void OnDrawGizmos()
    {
        // 绘制线段
        Gizmos.color = Color.green;
        Gizmos.DrawLine(startPoint, endPoint);
    }
}
