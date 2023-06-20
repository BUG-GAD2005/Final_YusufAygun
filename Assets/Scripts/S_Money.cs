using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class S_Money : MonoBehaviour
{
    public static S_Money Bank;


    public int Gold;

    public int Gem;

    [SerializeField] int StartGold;
    [SerializeField] int StartGem;

    [SerializeField] TMP_Text GoldText;
    [SerializeField] TMP_Text GemText;


    private void Awake()
    {
        if(Bank != null)
        {
            GameObject.Destroy(Bank);
        }
        else
        {
            Bank = this;
        }

        DontDestroyOnLoad(this);

        Gold = StartGold;       // burda paralarıı atıyoz
        Gem = StartGem;
    }
    void Start()
    {
        AdjustGoldGemText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AdjustGoldGemText()
    {
        GoldText.text=Gold.ToString();
        GemText.text=Gem.ToString();
    }

    public bool HasEnoughMoney(int a, int b)
    {
        if(Gold>=a&& Gem >= b)
        {
            return true;
        }
        return false;
    }
    public void AddMoney(int a, int b)
    {
        Gold += a;
        Gem += b;
        AdjustGoldGemText();
    }

    public void TakeMoney(int a, int b)
    {
        Gold -= a;
        Gem -= b;
        AdjustGoldGemText();
    }
}
