using System.Collections;
using System.Collections.Generic;
using UnityEngine;


struct CellStruct
{
    public Vector2Int pos;
    public bool IsEmpty;
    public GameObject CellActor;
}

public class Scr_Grid : MonoBehaviour
{
    [SerializeField] GameObject GridParent;
    [SerializeField] GameObject OriginalCellObject;

    CellStruct[,] TheGrid;

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

                TheGrid[i, k].CellActor.transform.localPosition = new Vector3(StartPos.x + (CellGap * i), StartPos.y + (CellGap * k), StartPos.z);
                TheGrid[i, k].CellActor.transform.SetParent(GridParent.transform);
                //TheGrid[i, k].CellActor.transform.parent=this.transform;   
                TheGrid[i, k].CellActor.transform.localScale = OriginalCellObject.transform.localScale;
                //TheGrid[i, k].CellActor.GetComponent<GridCellScript>().CellNum = new Vector2Int(k, i);
            }
        }
    }
}