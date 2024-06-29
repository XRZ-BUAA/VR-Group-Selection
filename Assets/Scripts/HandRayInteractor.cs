using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class HandRayInteractor : MonoBehaviour
{
    // Pass in the hand
    public PXR_Hand hand;

    // Pass in the text for Debug
    public TextMeshProUGUI debugText;

    // Hide Menu but not immediately
    private bool m_hideMenu = false;
    private float m_counter = 0;
    private float m_timeRange = 2f;
    private GameObject m_selected = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_hideMenu)
        {
            m_counter += Time.deltaTime;
            if (m_counter >= m_timeRange)
            {
                GameObject menu = GameObject.FindGameObjectWithTag("menu");
                // Exit Selected State before the menu being hidden
                var pointer = new PointerEventData(EventSystem.current);
                ExecuteEvents.Execute(m_selected, pointer, ExecuteEvents.pointerExitHandler);
                var toggle = m_selected.GetComponent<Toggle>();
                toggle.isOn = false;

                menu.GetComponent<MenuInit>().Hide();


                m_counter = 0;
                m_hideMenu = false;
                m_selected = null;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log();
        //debugText.text = other.gameObject.name;


        var pointer = new PointerEventData(EventSystem.current);
        ExecuteEvents.Execute(other.gameObject, pointer, ExecuteEvents.pointerEnterHandler);
    }

    private void OnTriggerStay(Collider other)
    {
        if (hand.Pinch)
        {
            var pointer = new PointerEventData(EventSystem.current);
            ExecuteEvents.Execute(other.gameObject, pointer, ExecuteEvents.pointerClickHandler);
            var toggle = other.gameObject.GetComponent<Toggle>();
            toggle.isOn = true;
            m_selected = other.gameObject;

            m_hideMenu = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var pointer = new PointerEventData(EventSystem.current);
        ExecuteEvents.Execute(other.gameObject, pointer, ExecuteEvents.pointerExitHandler);
    }
}
