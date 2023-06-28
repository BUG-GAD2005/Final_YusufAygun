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

    GameObject Canvas;
    public GameObject SpawnObject;

    public bool HasMoney;
    int goldprice;
    int gemprice;
    void Start()
    {
        SetCardAcordingToCardInfo();
        IsPriceOnlyOne();// if gold price or gem price is 0 removes it from render and centers the other price
        Canvas = GameObject.FindWithTag("Canvas");
    }

    // Update is called once per frame
    void Update()
    {
        CheckHasMonay();
    }

    void CheckHasMonay()
    {
        if (S_Money.Bank.HasEnoughMoney(goldprice, gemprice))
        {
            if (!HasMoney)
            {
                HasMoney = true;
                ChangeImageAccordingToMoney();
                BuildingImage.color= Color.white;
            }
            BuildingImage.color = Color.white;
        }
        else
        {
            if(HasMoney)
            {
                HasMoney = false;
                ChangeImageAccordingToMoney();
                BuildingImage.color= Color.red;
            }
            BuildingImage.color = Color.red;
        }
    }

    void ChangeImageAccordingToMoney()
    {
        if (HasMoney)
        {
            //Debug.Log("HasMoney");
        }
        else Debug.Log("NoMoney");

    }
    void SetCardAcordingToCardInfo()  
    {
        GoldPriceText.text = CardInfo.GoldPrice;
        GemPriceText.text = CardInfo.GemPrice;
        BuildingImage.sprite = CardInfo.BuildingImage;
        goldprice = int.Parse(CardInfo.GoldPrice);
        gemprice=int.Parse(CardInfo.GemPrice);
        Invoke("ChangeImageAccordingToMoney", 0.2f);

    }

    public GameObject GetDragActor()
    {
        GameObject DragActor=Instantiate(SpawnObject);
        DragActor.transform.SetParent(Canvas.transform);
        DragActor.transform.localScale = SpawnObject.transform.localScale;

        DragActor.GetComponent<Scr_Building>().SetValues(CardInfo);
        return DragActor;
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
