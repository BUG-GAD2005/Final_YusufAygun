using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_DragDrop : MonoBehaviour
{
    bool draging;
    GameObject DragObject;
    GameObject Canvas;
    [SerializeField] Scr_Grid GridScript;

    Vector3 CellPos;// LocalPosition of a gridcell with the purpose of placing dragobject
    Vector2Int GridPos; // GridCells rank in the main grid
    Vector2Int[] GridSize;

    bool CanBePlacedOnCell;

    Scr_Building  BuildingScript;
    void Start()
    {
        Canvas = GameObject.FindWithTag("Canvas");
    }

    // Update is called once per frame
    void Update()
    {
        if (draging)
            DragFunc();
    }


    void DragFunc()
    {
        Vector3 pos = new Vector3();
        if (FindGridCellUnderMouse())
        {
            pos = CellPos;
            pos.z = DragObject.transform.position.z;
            DragObject.transform.position = pos;

            if (GridScript.CanPlace(GridPos, BuildingScript.GridSize))
            {
                BuildingScript.ColorChangeToGreen();
                CanBePlacedOnCell = true;
            }
            else
            {
                BuildingScript.ColorChangeToRed();
                CanBePlacedOnCell = false;
            }           
        
        }
        else
        {
            pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = DragObject.transform.position.z;
            DragObject.transform.position = pos;
            BuildingScript.ColorChangeToWhite();
            CanBePlacedOnCell= false;
        }

    }

    public void TryToPick()
    {
        FindObjectUnderMouse();
    }

    public void TryToDrop()
    {
        if (draging)
        {
            draging = false;
            if (CanBePlacedOnCell)
            {
                BuildingScript.StarConstruction(GridPos);
                DragObject = null;
                BuildingScript = null;
            }
            else
            {
                Destroy(DragObject);
            }

        }     
    }

    void FindObjectUnderMouse()
    {
        Vector2 ray = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        RaycastHit2D hit = Physics2D.Raycast(ray, ray);
        if (hit.collider != null)
        {
            
            if (hit.collider.gameObject.GetComponent<ScrCard>())
            {        
                if(hit.collider.gameObject.GetComponent<ScrCard>().HasMoney)
                    StartDrag(hit.collider.gameObject.GetComponent<ScrCard>().GetDragActor());
            }
            
        }
    }

    bool FindGridCellUnderMouse()
    {
        Vector2 ray = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        RaycastHit2D hit = Physics2D.Raycast(ray, ray);
        if (hit.collider != null)
        {

            if (hit.collider.gameObject.GetComponent<Scr_GridCell>())
            {
                CellPos = hit.collider.gameObject.transform.position;
                GridPos = hit.collider.gameObject.GetComponent<Scr_GridCell>().GridPos;
                return true;
            }
           
        }
        return false;
    }


    void StartDrag(GameObject a)
    {
        DragObject = a;

        BuildingScript = DragObject.GetComponent<Scr_Building>();
        GridSize =BuildingScript.GridSize;
        //DragObject.transform.SetParent(Canvas.transform);
        draging = true;
    }
}
