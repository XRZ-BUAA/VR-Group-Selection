using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;


public class GraphicRenderer : MonoBehaviour
{
    // Pass in Debug Text
    public TextMeshProUGUI debugText;

    // The Material for Render
    public Material mat;

    // The empty to render
    //public GameObject toDraw;

    // the position of 2 pinch point
    private Vector3 leftPoint;
    private Vector3 rightPoint;
    //private Vector3 upPoint;

    private Toggle[] toggles;

    private GameObject selectionManager;
    private GraphicSelectionStart graphicSelectionStart;
    //private CreateLineSegmentWithCollider lineRender;

    private void Awake()
    {
        toggles = transform.GetComponentsInChildren<Toggle>();
        for (int i = 0; i < toggles.Length; i++)
        {

            Toggle toggle = toggles[i];

            //toggle.onValueChanged.AddListener((bool value) => OnToggleClick(toggle, value));
            toggles[i].onValueChanged.AddListener((bool value) => OnToggleClick(toggle, value));

            //debugText.text += toggle.gameObject.name + " ";
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        selectionManager = GameObject.Find("Selection Manager");
        if (selectionManager == null)
        {
            //debugText.text = "Can't find Selection Manager";
        }
        else
        {
            graphicSelectionStart = selectionManager.GetComponent<GraphicSelectionStart>();
            leftPoint = graphicSelectionStart.leftPoint;
            rightPoint = graphicSelectionStart.rightPoint;
            //upPoint = graphicSelectionStart.upPoint;
            //debugText.text = leftPoint.ToString() + " " + rightPoint.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (selectionManager == null)
        {
            //debugText.text = "Can't find Selection Manager";
        }
        else
        {
            leftPoint = graphicSelectionStart.leftPoint;
            rightPoint = graphicSelectionStart.rightPoint;
            //debugText.text = leftPoint.ToString() + " " + rightPoint.ToString();
        }
    }
    public void OnToggleClick(Toggle toggle, bool value)
    {   if (!value)
            return;

        //debugText.text = "On Toggle Click!";

        switch (int.Parse(toggle.gameObject.name[5].ToString())) { 
        case 1:
                // Line
                //debugText.text = "Draw Line" + "\n";
                //debugText.text += leftPoint.ToString() + "\n";
                //debugText.text += rightPoint.ToString();
                DestroyMesh();
                DrawLine();
                //Invoke("DrawLine", 0.5f);
                break;
        case 2:
                // Rectangle
                //debugText.text = "Draw Rectangle";
                //debugText.text += leftPoint.ToString() + "\n";
                //debugText.text += rightPoint.ToString();
                DestroyMesh();
                Rectangle();
                //Invoke("Rectangle", 0.5f);
                break;
        case 3:
                // Triangle
                //debugText.text = "Draw Triangle";
                //debugText.text += leftPoint.ToString() + "\n";
                //debugText.text += rightPoint.ToString();
                DestroyMesh();
                DrawTriangle();
                //Invoke("DrawTriangle", 0.5f);
                break;
        case 4:
                // Circle
                //debugText.text = "Draw Circle";
                //debugText.text += leftPoint.ToString() + "\n";
                //debugText.text += rightPoint.ToString();
                DestroyMesh();
                DrawCircle();
                //Invoke("DrawCircle", 0.5f);
                break;
        default:
                break;
        }
    }

    public void DestroyMesh()
    {
        GameObject lineObject = GameObject.Find("LineSegment");
        GameObject.Destroy(lineObject);
        GameObject quadObject = GameObject.Find("Quad");
        GameObject.Destroy(quadObject);
        GameObject triangleObject = GameObject.Find("Triangle");
        GameObject.Destroy(triangleObject);
        GameObject circleObject = GameObject.Find("Circle");
        GameObject.Destroy(circleObject);
    }
    
    private void DrawLine()
    {
        // 计算线段的长度
        float length = Vector3.Distance(leftPoint, rightPoint);

        // 创建一个空GameObject来存放线段
        GameObject lineObject = new GameObject("LineSegment");

        // 添加LineRenderer组件
        LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();

        // 设置线段的位置和宽度
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, leftPoint);
        lineRenderer.SetPosition(1, rightPoint);
        lineRenderer.startWidth = 0.001f;
        lineRenderer.endWidth = 0.001f;

        // 设置线段的材质和颜色（这里使用默认的白色材质）
        //lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.material = mat;
        lineRenderer.startColor = Color.gray;
        lineRenderer.endColor = Color.gray;

        // 添加BoxCollider来给予线段碰撞体积
        BoxCollider boxCollider = lineObject.AddComponent<BoxCollider>();

        // 计算盒子碰撞器的尺寸和中心点
        boxCollider.size = new Vector3(length, 0.0001f, 0.0001f);  // 根据实际需要调整高度和宽度
        boxCollider.center = (leftPoint + rightPoint) / 2f;

        // 计算盒子碰撞器的旋转（使其与线段方向一致）
        Vector3 direction = rightPoint - leftPoint;
        boxCollider.transform.rotation = Quaternion.LookRotation(direction, Vector3.up);

        //debugText.text = "DrawLine() is over" + "\n";
    }

    private void Rectangle()
    {
        // 创建一个空GameObject来存放四边形
        GameObject quadObject = new GameObject("Quad");

        // 添加MeshFilter组件
        MeshFilter meshFilter = quadObject.AddComponent<MeshFilter>();

        float y0 = leftPoint.y > rightPoint.y ? leftPoint.y : rightPoint.y;
        float y1 = leftPoint.y <= rightPoint.y ? leftPoint.y : rightPoint.y;
        float z = leftPoint.z > rightPoint.z ? leftPoint.z : rightPoint.z;

        // 创建四边形的顶点
        Vector3[] vertices = new Vector3[]
        {
            new Vector3(leftPoint.x, y0, z),
            new Vector3(rightPoint.x, y0, z),
            new Vector3(rightPoint.x, y1, z),
            new Vector3(leftPoint.x, y1, z)
        };

        // 定义四边形的三角形面
        int[] triangles = new int[]
        {
            0, 1, 2,
            0, 2, 3
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
        //meshRenderer.material = new Material(Shader.Find("Standard"));
        meshRenderer.material = mat;

        // 添加MeshCollider来给予四边形碰撞体积
        MeshCollider meshCollider = quadObject.AddComponent<MeshCollider>();
        meshCollider.sharedMesh = mesh;


        //BoxCollider boxCollider = quadObject.AddComponent<BoxCollider>();
        //boxCollider.size = new Vector3(Mathf.Abs(leftPoint.x - rightPoint.x), Mathf.Abs(leftPoint.y - rightPoint.y), 0.0001f);  // 根据实际需要调整高度和宽度
        //boxCollider.center = new Vector3((leftPoint.x + rightPoint.x) / 2f, (leftPoint.y + rightPoint.y) / 2f, z);
    }

    private void DrawTriangle()
    {
        //debugText.text = "DrawTriangle is called";

        float radius = Vector3.Distance(leftPoint, rightPoint);

        // 创建一个空GameObject来存放圆形
        GameObject triangleObject = new GameObject("Triangle");

        // 添加MeshFilter组件
        MeshFilter meshFilter = triangleObject.AddComponent<MeshFilter>();

        // 创建圆形的顶点
        int segments = 3;  // 圆形的分段数，可以根据需要调整
        Vector3[] vertices = new Vector3[segments];

        for (int i = 0; i < segments; i++)
        {
            float angle = Mathf.PI * 2f * i / segments;
            vertices[i] = new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0f) + leftPoint;
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
        MeshRenderer meshRenderer = triangleObject.AddComponent<MeshRenderer>();
        //meshRenderer.material = new Material(Shader.Find("Standard"));
        meshRenderer.material = mat;

        // 添加MeshCollider来给予圆形碰撞体积
        MeshCollider meshCollider = triangleObject.AddComponent<MeshCollider>();
        meshCollider.sharedMesh = mesh;
    }

    private void DrawCircle()
    {
        //debugText.text = "DrawCircle() is called";
        float radius = Vector3.Distance(leftPoint, rightPoint);

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
            vertices[i] = new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0f) + leftPoint;
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
        //meshRenderer.material = new Material(Shader.Find("Standard"));
        meshRenderer.material = mat;

        // 添加MeshCollider来给予圆形碰撞体积
        MeshCollider meshCollider = circleObject.AddComponent<MeshCollider>();
        meshCollider.sharedMesh = mesh;
    }


}
