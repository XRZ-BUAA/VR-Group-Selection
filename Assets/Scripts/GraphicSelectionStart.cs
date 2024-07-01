using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GraphicSelectionStart : MonoBehaviour
{
    // Pass In
    // Pass in two hands
    public PXR_Hand leftHand;
    public PXR_Hand rightHand;

    // Expose
    // Expose the hit points
    public Vector3 leftPoint;
    public Vector3 rightPoint;
    //public Vector3 upPoint;

    // Pass in text for debug
    public TextMeshProUGUI debugText;

    // Select State
    private bool m_isSelecting;

    private GameObject leftRayPose;
    private GameObject rightRayPose;

    // Hand Ray
    private Ray m_leftRay;
    private Ray m_rightRay;
    // The information about hit
    private RaycastHit leftInfo;
    private RaycastHit rightInfo;

    private void SelectGraphic()
    {
        // enable the menu
        GameObject menu = GameObject.FindGameObjectWithTag("menu");
        if (menu == null)
        {
            //debugText.text = "Can't find your menu";
        }
        // show the menu
        menu.GetComponent<MenuInit>().Show();
        

    }

    // Start is called before the first frame update
    void Start()
    {
        m_isSelecting = gameObject.GetComponent<SelectMode>().isSelecting;

        leftRayPose = GameObject.FindGameObjectWithTag("Left Ray");
        rightRayPose = GameObject.FindGameObjectWithTag("Right Ray");

        if (leftPoint == null || rightRayPose == null) {
            //debugText.text = "Can't find your Ray Pose";
        }
        // Create hand ray
        m_leftRay = new Ray(leftRayPose.transform.position, leftRayPose.transform.forward);
        m_rightRay = new Ray(rightRayPose.transform.position, rightRayPose.transform.forward);
    }

    // Update is called once per frame
    void Update()
    {
        m_leftRay = new Ray(leftRayPose.transform.position, leftRayPose.transform.forward);
        m_rightRay = new Ray(rightRayPose.transform.position, rightRayPose.transform.forward);

        m_isSelecting = gameObject.GetComponent<SelectMode>().isSelecting;

        if (m_isSelecting && leftHand.Pinch && rightHand.Pinch)
        {
            //debugText.text = "Choose your Graphic";
            if (Physics.Raycast(m_leftRay, out leftInfo) && Physics.Raycast(m_rightRay, out rightInfo))
            {
                leftPoint = leftInfo.point;
                rightPoint = rightInfo.point;
                //debugText.text = leftPoint.ToString() + " " + rightPoint.ToString();    
            }

            SelectGraphic();
        }
        //else if (m_isSelecting && leftHand.Pinch)
        //{
        //    if (Physics.Raycast(m_leftRay, out leftInfo))
        //    {
        //        upPoint = leftInfo.point;
        //    }
        //}
    }
}
