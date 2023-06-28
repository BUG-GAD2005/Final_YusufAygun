using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class S_DataBuilt : MonoBehaviour
{
    [SerializeField] Scr_Grid GridScript;
    [SerializeField] ScrCardInfo[] InfoCards;
    [SerializeField] GameObject SpawnObject;
    GameObject Canvas;

    bool ShouldSave;
    void Start()
    {

        Canvas = GameObject.FindWithTag("Canvas");

        //LoadGame();
        /*Invoke("SaveGame", 10);   //Test the save system
        Invoke("LoadGame", 11);*/

        Invoke("LoadGame", 0.05f);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ShouldSave)
        {
            SaveGame();
        }
    }

    public void Restart()
    {
        ShouldSave = false;
        CellStruct[,] EmptyGrid = new CellStruct[1, 1];
        S_SaveSystem.SaveGame(S_Money.Bank.StartGold, S_Money.Bank.StartGem, EmptyGrid);
        SceneManager.LoadScene("SampleScene");
        
    }

    public void SaveGame()
    {
        S_SaveSystem.SaveGame(S_Money.Bank.Gold, S_Money.Bank.Gem, GridScript.TheGrid);
        Debug.Log("Saved");

        
    }

    public void LoadGame()
    {
        S_GameData data = S_SaveSystem.LoadGame();
        //Debug.Log(data.Gold + "   " + data.Gem);
        Debug.Log(data.map.Count);
        Debug.Log("Loaded");
        SpawnSavedBuildings(data);

        ShouldSave = true;

        S_Money.Bank.SetMoneyFromLoad(data.Gold, data.Gem);
    }
    void SpawnSavedBuildings(S_GameData dat)
    {
        List<float[]> binarymap = new List<float[]>();

        binarymap = dat.map;

        foreach (var bina in binarymap)
        {
            GameObject Buildingg = SpawnBuilding();

            Buildingg.GetComponent<Scr_Building>().SetValues(InfoCards[(int)bina[2]]);

            PlaceOnGrid(Buildingg, bina);
        
        }
    }

    GameObject SpawnBuilding()
    {
        GameObject Building = Instantiate(SpawnObject);
        Building.transform.SetParent(Canvas.transform);
        Building.transform.localScale = SpawnObject.transform.localScale;

        return Building;
    }

    void PlaceOnGrid(GameObject Building, float[] data)
    {
        Vector3 pos = GridScript.TheGrid[(int)data[0], (int)data[1]].CellActor.transform.position;

        Building.transform.position = pos;
        Scr_Building BineScript = Building.GetComponent<Scr_Building>();

        BineScript.GridPlace = new Vector2Int((int)data[0], (int)data[1]);
        if (data[3] == (float)1)
        {
            BineScript.State = BinaState.Construction;
        }
        else
        { BineScript.State = BinaState.Working; }

        BineScript.GridImageChange();

        BineScript.CurrentTime = data[4];

        GridScript.TheGrid[(int)data[0], (int)data[1]].OriginOfBuilding = true;

        GridScript.TheGrid[(int)data[0], (int)data[1]].BuildingScript = BineScript;




    }
}
