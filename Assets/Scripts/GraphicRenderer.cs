using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class GraphicRenderer : MonoBehaviour
{
    // Pass in Debug Text
    public TextMeshProUGUI debugText;

    // The Material for Render
    public Material mat;

    // The empty to render
    public GameObject toDraw;

    // the position of 2 pinch point
    private Vector3 leftPoint;
    private Vector3 rightPoint;

    private Toggle[] toggles;

    private GameObject selectionManager;
    private GraphicSelectionStart graphicSelectionStart;

    private void Awake()
    {
        toggles = transform.GetComponentsInChildren<Toggle>();
        for (int i = 0; i < toggles.Length; i++)
        {

            Toggle toggle = toggles[i];

            //toggle.onValueChanged.AddListener((bool value) => OnToggleClick(toggle, value));
            toggles[i].onValueChanged.AddListener((bool value) => OnToggleClick(toggle, value));

            debugText.text += toggle.gameObject.name + " ";
        }
        //debugText.text = toggles.Length.ToString();
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
                // rectangle
                debugText.text = "Draw Rectangle" + "\n";
                debugText.text += leftPoint.ToString() + "\n";
                debugText.text += rightPoint.ToString();
                DrawRectangle();
                break;
        case 2:
                // Circle
                debugText.text = "Draw Circle";
                debugText.text += leftPoint.ToString() + "\n";
                debugText.text += rightPoint.ToString();
                break;
        case 3:
                // Triangle
                debugText.text = "Draw Triangle";
                debugText.text += leftPoint.ToString() + "\n";
                debugText.text += rightPoint.ToString();
                DrawTriangle();
                break;
        case 4:
                // Hexagon
                debugText.text = "Draw Hexagon";
                debugText.text += leftPoint.ToString() + "\n";
                debugText.text += rightPoint.ToString();
                break;
        default:
                break;
        }
    }

    public void DrawRectangle()
    {
        toDraw.GetComponent<MeshRenderer>().material = mat;
        Mesh mesh = toDraw.GetComponent<MeshFilter>().mesh;
        mesh.Clear();

        Vector3[] vertices = {
            new Vector3(leftPoint.x, leftPoint.y, leftPoint.z),
            new Vector3(rightPoint.x, leftPoint.y, leftPoint.z),
            new Vector3(leftPoint.x, rightPoint.y, rightPoint.z),
            new Vector3(rightPoint.x, rightPoint.y, rightPoint.z)
        };

        int[] indices =
        {
            2, 0, 3,
            0, 1, 3
        };


        mesh.vertices = vertices;
        mesh.triangles = indices;
    }

    public void DrawTriangle()
    {
        toDraw.GetComponent<MeshRenderer>().material = mat;
        Mesh mesh = toDraw.GetComponent<MeshFilter>().mesh;
        if (mat == null || mesh == null) {
            debugText.text = "What can I say";
        }
        //mesh.Clear();
        Vector2[] newUV = { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 0) };
        debugText.text = "God bless me";
        //设置顶点
        mesh.vertices = new Vector3[] { new Vector3(0, 3.11f, 0.81f), new Vector3(0, 2.615f, 0.81f), new Vector3(0.275f, 3.11f, 0) };
        //设置三角形顶点顺序，顺时针设置
        mesh.triangles = new int[] { 0, 1, 2 };
        mesh.uv = newUV;
    }
}
