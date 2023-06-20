using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public enum BinaState { Nothing, Construction, Working};
public class Scr_Building : MonoBehaviour
{
    public int GoldPrice;
    public int GemPrice;
    public Vector2Int[] GridSize;
    public int GoldRevanueForTime;
    public int GemRevanueForTime;
    public float RevanueTime;
    public float BuildingTime;
    public float CurrentTime;
    public Vector2Int Origin›nMap;

    BinaState State=BinaState.Nothing;

    Scr_Grid GridScript;

    Color Renkred ;
    Color RenkWhite;
    Color RenkGreen;
    Color RenkGorunmez;

    public Image image;

    [SerializeField] GameObject Bar;
    [SerializeField] Image ProgresBar;

    Vector2Int GridPlace;
    void Start()
    {
        
        GridScript=GameObject.FindWithTag("LogicRunner").GetComponent<Scr_Grid>();

        Debug.Log(S_Money.Bank.Gold);
    }

    // Update is called once per frame
    void Update()
    {
        if (State == BinaState.Construction)
        {
            CurrentTime += Time.deltaTime;
            if(CurrentTime> BuildingTime)
            {
                ConstructionCompleated();
            }
            else
            {
                SetProgresBarFloat(CurrentTime / BuildingTime);
            }
        }
    }

    void ConstructionCompleated()
    {
        State = BinaState.Working;
        CurrentTime = 0;
        ColorChangeToWhite();

        foreach (Vector2Int block in GridSize)      //changes the images on the grid map
        {
            Vector2Int Konum = new Vector2Int(block.x + GridPlace.x, block.y + GridPlace.y);
            GridScript.TheGrid[Konum.x, Konum.y].CellScript.TurnImageBuildingCompleated();
        }

    }
    void SetProgresBarFloat(float value)
    {
        ProgresBar.fillAmount = value;
    }
    public void SetValues(ScrCardInfo cardinfos2)
    {
        GoldPrice = Convert.ToInt32(cardinfos2.GoldPrice);
        GemPrice = Convert.ToInt32(cardinfos2.GemPrice);
        GridSize=cardinfos2.GridSize;
        GoldRevanueForTime = cardinfos2.GoldRevanueForTime;
        GemRevanueForTime=cardinfos2.GemRevanueForTime;
        RevanueTime = cardinfos2.RevanueTime;
        BuildingTime = cardinfos2.BuildingTime;

        image.sprite = cardinfos2.BuildingImage;

        Vector3 ScaleImage = image.transform.localScale;
        ScaleImage.x *= cardinfos2.XSizeMultiply;
        ScaleImage.y *= cardinfos2.YSizeMultiply;

        image.transform.localScale = ScaleImage;
        SetColors();

        
    }

    public void StarConstruction(Vector2Int Origin)
    {
        Debug.Log("Aaaaaa");
        GridPlace = Origin;

        foreach (Vector2Int block in GridSize)      //changes the images on the grid map
        {
            Vector2Int Konum= new Vector2Int(block.x+Origin.x,block.y+Origin.y);
            GridScript.TheGrid[Konum.x, Konum.y].CellScript.TurnImageConstruct();
            GridScript.TheGrid[Konum.x, Konum.y].IsEmpty = false;
        }
        
        ColorChangeDisapear();

        Bar.SetActive(true);
        SetProgresBarFloat(0);

        State = BinaState.Construction;

    }

    public void ColorChangeToRed()
    {
        image.color = Renkred;
    }

    public void ColorChangeToWhite()
    {
        image.color = RenkWhite;
    }
    public void ColorChangeToGreen()
    {
        image.color = RenkGreen;
    }
    public void ColorChangeDisapear()
    {
        image.color = RenkGorunmez;
    }
    void SetColors() 
    {
        Renkred = Color.red;
        Renkred.a = 0.5f;

        RenkGreen = Color.green;
        RenkGreen.a = 0.5f;

        RenkWhite = Color.white;

        RenkGorunmez = Color.white;
        RenkGorunmez.a = 0f;
    }
}
