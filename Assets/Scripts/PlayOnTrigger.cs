//using System.Collections;
//using System.Collections.Generic;
//using TMPro;
//using UnityEngine;

//public class PlayOnTrigger : MonoBehaviour
//{
//    //public TextMeshProUGUI debugText;
//    // Start is called before the first frame update
//    void Start()
//    {
//        //debugText.text = "It is PlayOnTrigger";
//    }

//    // Update is called once per frame
//    void Update()
//    {

//    }

//    private void OnTriggerEnter(Collider other)
//    {
//        //debugText.text += other.gameObject.name;
//    }

//    private void OnTriggerStay(Collider other)
//    {
//        if (other.gameObject.name == "quadObject")
//        {
//            gameObject.GetComponent<Renderer>().material.color = Color.red;
//        }
//    }

//    private void OnTriggerExit(Collider other)
//    {
//        gameObject.GetComponent<Renderer>().material.color = Color.white;
//    }
//}