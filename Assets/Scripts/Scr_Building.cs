using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Scr_Building : MonoBehaviour
{
    public int GoldPrice;
    public int GemPrice;
    public Vector2Int[] GridSize;
    public int GoldRevanueForTime;
    public int GemRevanueForTime;
    public float RevanueTime;
    public Vector2Int Origin›nMap;

    Color Renkred ;
    Color RenkWhite;
    Color RenkGreen;

    public Image image;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetValues(ScrCardInfo cardinfos2)
    {
        GoldPrice = Convert.ToInt32(cardinfos2.GoldPrice);
        GemPrice = Convert.ToInt32(cardinfos2.GemPrice);
        GridSize=cardinfos2.GridSize;
        GoldRevanueForTime = cardinfos2.GoldRevanueForTime;
        GemRevanueForTime=cardinfos2.GemRevanueForTime;
        RevanueTime = cardinfos2.RevanueTime;

        image.sprite = cardinfos2.BuildingImage;

        Vector3 ScaleImage = image.transform.localScale;
        ScaleImage.x *= cardinfos2.XSizeMultiply;
        ScaleImage.y *= cardinfos2.YSizeMultiply;

        image.transform.localScale = ScaleImage;
        SetColors();
        
        
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
    void SetColors() 
    {
        Renkred = Color.red;
        Renkred.a = 0.5f;

        RenkGreen = Color.green;
        RenkGreen.a = 0.5f;

        RenkWhite = Color.white;
    }
}
