using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ColorChangeOnOverlap : MonoBehaviour
{
    public TextMeshProUGUI debugText;

    public Color overlapColor = Color.red; // 重叠后的颜色

    public PXR_Hand hand;

    private Renderer renderer; // 获取物体的Renderer组件

    private GameObject[] ignoreOverlap;

    private bool flag = false;

    // Select State
    private GameObject selectionManager;
    private bool m_isSelecting;
    private bool selected = false;

    private int preColNum = 0;
    private int colNum = 0;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
        ignoreOverlap = GameObject.FindGameObjectsWithTag("ignore");
        if (ignoreOverlap == null )
        {
            debugText.text = "Can't build ignoreOverlap";
        }
        else
        {
            //debugText.text = ignoreOverlap.Length.ToString();
            //if (GameObject.Find("Item 1") == null) {
            //    debugText.text = "What the fuck?";
            //}
        }

        selectionManager = GameObject.Find("Selection Manager");
        if (selectionManager == null)
        {
            debugText.text = "Can't find Selection Manager";
        }
        m_isSelecting = selectionManager.GetComponent<SelectMode>().isSelecting;
    }

    private void Update()
    {
        m_isSelecting = selectionManager.GetComponent<SelectMode>().isSelecting;
        debugText.text = m_isSelecting.ToString();
        if (!m_isSelecting) {
            return;
        }
        // 检测是否与其他物体发生重叠
        Collider[] colliders = Physics.OverlapBox(transform.position, transform.localScale / 14);
        colNum = 0;
        foreach(Collider col in colliders)
        {
            if(col.gameObject.name == "DefaultRay")
            {
                continue;
            }
            colNum++;
        }

        if (colNum != preColNum)
        {
            foreach (Collider col in colliders)
            {
                // 如果发生重叠且不是自己
                if (col.gameObject != gameObject && !ignoreOverlap.Contains(col.gameObject) && !col.gameObject.name.Contains("Item"))
                {
                    if (col.gameObject.name == "HandInteractor" && !hand.Pinch)
                    {
                        continue;
                    }

                    // 改变自己的颜色
                    //renderer.material.color = overlapColor;
                    //Destroy(col);
                    selected = !selected;
                }
            }
        }
        if (selected)
        {
            renderer.material.color = overlapColor;
        }
        else
        {
            renderer.material.color = Color.white;
        }
        preColNum = colNum;
    }
}
