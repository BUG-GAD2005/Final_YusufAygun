using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ScrCard : MonoBehaviour
{
    [SerializeField] ScrCardInfo CardInfo;
    [SerializeField] Image BuildingImage;
    [SerializeField] TMP_Text GoldPriceText;
    [SerializeField] TMP_Text GemPriceText;
    [SerializeField] GameObject GoldPriceObject;
    [SerializeField] GameObject GemPriceObject;
    void Start()
    {
        SetCardAcordingToCardInfo();
        IsPriceOnlyOne();// if gold price or gem price is 0 removes it from render and centers the other price
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void SetCardAcordingToCardInfo()  
    {
        GoldPriceText.text = CardInfo.GoldPrice;
        GemPriceText.text = CardInfo.GemPrice;
        BuildingImage.sprite = CardInfo.BuildingImage;
    }


    void IsPriceOnlyOne()  // if gold price or gem price is 0 removes it from render and centers the other price
    {
        

        if (CardInfo.GemPrice == "0")
        {
            GoldPriceObject.transform.localPosition = (GoldPriceObject.transform.localPosition + GemPriceObject.transform.localPosition) / 2;
            GemPriceObject.SetActive(false);
        }else if(CardInfo.GoldPrice == "0")
        {
            GemPriceObject.transform.localScale = (GoldPriceObject.transform.localPosition + GemPriceObject.transform.localPosition) / 2;
            GoldPriceObject.SetActive(false);
        }
    }

}
