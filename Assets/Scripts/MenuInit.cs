using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuInit : MonoBehaviour
{
    // Pass in a debug text
    public TextMeshProUGUI debugText;
    // expose
    public float distanceInFront = 0.4f;


    private CanvasGroup m_canvasGroup;
    private Transform cameraOffset;
    private GameObject m_content;

    // Start is called before the first frame update
    void Start()
    {
        cameraOffset = GameObject.Find("Main Camera").transform;
        m_canvasGroup = gameObject.GetComponent<CanvasGroup>();
        if (m_canvasGroup == null)
        {
            //debugText.text = "Can't find CanvasGroup";
        }
        m_content = GameObject.FindGameObjectWithTag("All Toggle");
        if (m_content == null)
        {
            //debugText.text = "Can't find Content";
        }
        Hide();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hide()
    {
        m_canvasGroup.alpha = 0f;

        // Make it non-interactive
        m_canvasGroup.blocksRaycasts = false;

        // Disable all the toggle
        m_content.SetActive(false);
    }

    public void Show() {

        gameObject.transform.position = cameraOffset.position + cameraOffset.forward * distanceInFront;
        m_canvasGroup.alpha = 1.0f;

        // Make it non-interactive
        m_canvasGroup.blocksRaycasts = true;

        // Enable all toggles
        m_content.SetActive(true);
    }
}
