using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Inputs : MonoBehaviour
{
    Scr_DragDrop DragScript;
    void Start()
    {
        DragScript = GetComponent<Scr_DragDrop>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DragScript.TryToPick();
        }

        if (Input.GetMouseButtonUp(0))
        {
            DragScript.TryToDrop();
        }
    }
}
