using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_DragDrop : MonoBehaviour
{
    bool draging;
    GameObject DragObject;
    GameObject Canvas;
    void Start()
    {
        Canvas = GameObject.FindWithTag("Canvas");
    }

    // Update is called once per frame
    void Update()
    {
        if (draging)
            Draging();
    }


    void Draging()
    {

        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = DragObject.transform.position.z;

        DragObject.transform.position = pos;

    }

    public void TryToPick()
    {
        FindObjectUnderMouse();
    }

    public void TryToDrop()
    {
        draging = false;
        
    }

    void FindObjectUnderMouse()
    {
        Vector2 ray = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        RaycastHit2D hit = Physics2D.Raycast(ray, ray);
        if (hit.collider != null)
        {
            
            if (hit.collider.gameObject.GetComponent<ScrCard>())
            {               
                StartDrag(hit.collider.gameObject.GetComponent<ScrCard>().GetDragActor());
            }
            
        }
    }


    void StartDrag(GameObject a)
    {
        DragObject = a;
        //DragObject.transform.SetParent(Canvas.transform);
        draging = true;
    }
}
