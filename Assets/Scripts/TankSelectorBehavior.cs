using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;

public class TankSelectorBehavior : MonoBehaviour
{
    public Transform holder;
    public void SelectEntered()
    {

    }
    public void SelectExited()
    {
        ResetTank();
        //Debug.Log(gameObject.name + ": SelectExited");
    }
    public void ResetTank()
    {
        transform.parent = holder;
    }
}
