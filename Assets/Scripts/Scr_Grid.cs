using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct CellStruct
{
    public Vector2Int pos;
    public bool IsEmpty;
    public GameObject CellActor;
    public Scr_GridCell CellScript;
    public bool OriginOfBuilding;
    public Scr_Building BuildingScript;
}

public class Scr_Grid : MonoBehaviour
{
    [SerializeField] GameObject GridParent;
    [SerializeField] GameObject OriginalCellObject;

    public CellStruct[,] TheGrid;

    int TotalRows = 10; 
    int TotalColumns = 10;
    Vector3 StartPos = new Vector3(0, 0, 0);
    public float GapBetweenCells = 1f;
    float CellGap = 1f;
    void Start()
    {
        TheGrid=new CellStruct[TotalRows,TotalColumns];
        /*StartPos.x = OriginalCellObject.transform.localPosition.x; ;
        StartPos.y = OriginalCellObject.transform.localPosition.y;*/
        StartPos=OriginalCellObject.transform.position;

        CreateGrid();
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("go");
    }

    public bool CanPlace(Vector2Int Orgin,Vector2Int[] SizeOfOther)
    {
        foreach(Vector2Int block in SizeOfOther)
        {
            if(!TheGrid[block.x + Orgin.x, block.y + Orgin.y].IsEmpty)
                return false;
        }
        return true;
    }
    bool IsEmpty(Vector2Int CheckPos)
    {
        if(CheckPos.x<0||CheckPos.x>TotalRows|| CheckPos.y < 0 || CheckPos.y > TotalColumns)
        {
            return false;
        }

        return TheGrid[CheckPos.x, CheckPos.y].IsEmpty;
    }

    void CreateGrid()
    {


        for (int i = 0; i < TotalRows; i++)
        {

            for (int k = 0; k < TotalColumns; k++)
            {
                TheGrid[i, k].pos=new Vector2Int(i,k);
                
                TheGrid[i, k].IsEmpty = true;
                TheGrid[i, k].CellActor = Instantiate(OriginalCellObject);
                TheGrid[i, k].CellActor.GetComponent<Scr_GridCell>().GridPos=new Vector2Int(i,k);

                TheGrid[i, k].CellActor.transform.localPosition = new Vector3(StartPos.x + (CellGap * i), StartPos.y + (CellGap * k), StartPos.z);
                TheGrid[i, k].CellActor.transform.SetParent(GridParent.transform);
                //TheGrid[i, k].CellActor.transform.parent=this.transform;   
                TheGrid[i, k].CellActor.transform.localScale = OriginalCellObject.transform.localScale;
                TheGrid[i, k].CellScript = TheGrid[i, k].CellActor.GetComponent<Scr_GridCell>();
                

                //TheGrid[i, k].CellActor.GetComponent<GridCellScript>().CellNum = new Vector2Int(k, i);

            }
        }

    }

    

    
}
