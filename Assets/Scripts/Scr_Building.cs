using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


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
    //public Vector2Int Origin›nMap;
    public ScrCardInfo CardInfo;

    [SerializeField] GameObject FlyOriginalObject;
    [SerializeField] float FlyTextPosAdd;

    public BinaState State=BinaState.Nothing;

    Scr_Grid GridScript;

    Color Renkred ;
    Color RenkWhite;
    Color RenkGreen;
    Color RenkGorunmez;

    public Image image;

    [SerializeField] GameObject Bar;
    [SerializeField] Image ProgresBar;
    [SerializeField] TMP_Text CountDown;

    public Vector2Int GridPlace;
    void Start()
    {
        
        GridScript=GameObject.FindWithTag("LogicRunner").GetComponent<Scr_Grid>();

        Debug.Log(S_Money.Bank.Gold + "Money ");
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
                int x = Convert.ToInt32(BuildingTime) - Convert.ToInt32(CurrentTime);
                CountDown.text = x.ToString();
            }
        }
        else if (State == BinaState.Working)
        {
            CurrentTime += Time.deltaTime;
            if (CurrentTime > RevanueTime)
            {
                WorkCompleated();
            }
            else
            {
                SetProgresBarFloat(CurrentTime / RevanueTime);
                int x = Convert.ToInt32(RevanueTime) - Convert.ToInt32(CurrentTime) + 1;
                CountDown.text = x.ToString();

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

    void WorkCompleated()
    {
        CurrentTime = 0;
        CreateFlyingText() ;
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

        CardInfo=cardinfos2;
    }
    
    public void GridImageChange()
    {
        GridScript = GameObject.FindWithTag("LogicRunner").GetComponent<Scr_Grid>();
        if (State == BinaState.Working)
        {
            foreach (Vector2Int block in GridSize)      //changes the images on the grid map
            {
                Vector2Int Konum = new Vector2Int(block.x + GridPlace.x, block.y + GridPlace.y);
                GridScript.TheGrid[Konum.x, Konum.y].CellScript.TurnImageBuildingCompleated();
                GridScript.TheGrid[Konum.x, Konum.y].IsEmpty = false;
            }

        }
        else
        {
            foreach (Vector2Int block in GridSize)      //changes the images on the grid map
            {
                Vector2Int Konum = new Vector2Int(block.x + GridPlace.x, block.y + GridPlace.y);
                GridScript.TheGrid[Konum.x, Konum.y].CellScript.TurnImageConstruct();
                GridScript.TheGrid[Konum.x, Konum.y].IsEmpty = false;
            }
            ColorChangeDisapear();
        }
    }

    public void CreateFlyingText()
    {
       
        //S_FlyingText FlyTextScript = FlyText.GetComponent<S_FlyingText>(); 

        if(State== BinaState.Construction)
        {
            if (GoldPrice != 0)
                CreateFlyText(true).SetGold(-GoldPrice);

            if (GemPrice != 0)
                CreateFlyText(false).SetGem(-GemPrice);

        }
        else if (State == BinaState.Working)
        {
            if (GoldRevanueForTime != 0)
                CreateFlyText(true).SetGold(GoldRevanueForTime);

            if (GemRevanueForTime != 0)
                CreateFlyText(false).SetGem(GemRevanueForTime);

        }
    }

    S_FlyingText CreateFlyText(bool IsGold)
    {
        GameObject FlyText = Instantiate(FlyOriginalObject);
        FlyText.transform.SetParent(transform.parent.transform);
        Vector3 pos = transform.localPosition;
        if (IsGold)
            pos.x -= FlyTextPosAdd;
        else
            pos.x += FlyTextPosAdd;
        FlyText.transform.localPosition = pos;
        FlyText.transform.localScale = new Vector3(1, 1, 1);
        return FlyText.GetComponent<S_FlyingText>();
    }

    public void StarConstruction(Vector2Int Origin)
    {
        GridScript.TheGrid[Origin.x, Origin.y].OriginOfBuilding = true;
        GridScript.TheGrid[Origin.x, Origin.y].BuildingScript = this;
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

        CreateFlyingText();
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
