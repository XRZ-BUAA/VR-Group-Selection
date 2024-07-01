using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Globalization;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class HandInteractor : MonoBehaviour
{
    // Start is called before the first frame update
    //public Text text;
    public PXR_Hand m_Hand;


    //private TextMeshProUGUI text;

    private TankSelectorBehavior m_Tank;
    private bool m_IsTankInHand = false;
    private bool Pinch = false;
    void Start()
    {
        //text = GameObject.Find("Canvas/Text(TMP)").GetComponent<TextMeshProUGUI>();
        //text.text = "hello text";
    }

    // Update is called once per frame
    void Update()
    {
        Pinch = m_Hand.Pinch || m_Hand.PinchStrength > 0;
        if (!Pinch && m_IsTankInHand) {
            //text.text = "Release Tank!";
            if (m_Tank != null)
            {
                m_IsTankInHand = false;
                m_Tank.SelectExited();
            }
        }


        //SphereCollider col = gameObject.AddComponent<SphereCollider>();
        //col.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        m_Tank = other.GetComponent<TankSelectorBehavior>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (m_Hand != null)
        {
            if (m_Hand.Pinch && !m_IsTankInHand)
            {
                m_Tank = other.GetComponent<TankSelectorBehavior>();
                if (m_Tank != null)
                {
                    //text.text = "Grab Tank!";
                    switch (m_Tank.name) {
                        case "Cube":
                            m_Tank.GetComponent<MeshRenderer>().material.color = Color.red;
                            break;
                        case "Capsule":
                            m_Tank.GetComponent<MeshRenderer>().material.color = Color.green;
                            break;
                        case "Sphere":
                            m_Tank.GetComponent<MeshRenderer>().material.color = Color.yellow;
                            break;
                        default:
                            break;
                    }

                    //m_Tank.GetComponent<MeshRenderer>().material.color = Color.red;
                    m_IsTankInHand = true;
                    m_Tank.SelectEntered();
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        m_Tank.SelectExited();
        m_Tank.GetComponent<MeshRenderer>().material.color = Color.white;
        m_IsTankInHand = false;
        m_Tank = null;
    }
}
