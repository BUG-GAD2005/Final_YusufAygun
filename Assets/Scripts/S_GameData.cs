using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class S_GameData 
{
    public int Gold;
    public int Gem;

    public List<float[]> map = new List<float[]>();


    public void GetCuzdanData(int Gold2, int Gem2)
    {
        Gold = Gold2;
        Gem = Gem2;
    }

    public S_GameData(int Gold, int Gem, CellStruct[,] TheGrid)
    {
        this.Gold = Gold;
        this.Gem = Gem;

        foreach (CellStruct cell in TheGrid)
        {
            if (cell.OriginOfBuilding)
            {
                

                float[] Grid = new float[5];
                
                Grid[0] = (float)cell.pos.x;
                Grid[1] = (float)cell.pos.y;
                Grid[2] = cell.BuildingScript.CardInfo.SaveIdentity;
                Grid[3] = convertBuildingState(cell.BuildingScript.State);
                Grid[4] = cell.BuildingScript.CurrentTime;

                map.Add(Grid);
            }
        }
    }
     
    float convertBuildingState(BinaState a)
    {
        if (a == BinaState.Construction)
            return 1;
        else return 2;
    }

}
