using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectMode : MonoBehaviour
{

    public TextMeshProUGUI debugText;
    public bool isSelecting;

    public void SwitchMode()
    {
        if (isSelecting)
        {
            isSelecting = false;
        }
        else
        {
            isSelecting = true; 
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        isSelecting = false;

    }

    // Update is called once per frame
    void Update()
    {
        //debugText.text = "Selection State: " + isSelecting.ToString();
    }
}
