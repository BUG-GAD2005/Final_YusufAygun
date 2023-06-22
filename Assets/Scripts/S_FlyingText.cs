using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class S_FlyingText : MonoBehaviour
{

    [SerializeField] Image Foto;
    [SerializeField] TMP_Text NumberText;

    [SerializeField] Sprite FotoGold;
    [SerializeField] Sprite FotoGem;

    [SerializeField] float MoveSpeed;

    [SerializeField] float StopTime;

    GameObject TargetLoc;
    Vector3 TargetPos;

    bool IsGold;            // if false þt means gem

    bool Move;// if set object will move towards cuzdan place

    public int Value;
    // Start is called before the first frame update
    void Start()
    {
        TargetLoc = GameObject.FindWithTag("CuzdanPlace");
        TargetPos = TargetLoc.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Move)
        {
            MoveToCuzdan();
            if (transform.position.x <= TargetPos.x)
            {
                ReachCuzdan();
            }
        }
    }

    void ReachCuzdan()
    {
        


            if (IsGold)
            {
                S_Money.Bank.AddMoney(Value, 0);
            }
            else
            {
                S_Money.Bank.AddMoney(0, Value);
            }
            Destroy(gameObject);
        
    }
    public void MoveToCuzdan()
    {
        
        transform.position += (( TargetPos - transform.position).normalized * MoveSpeed * Time.deltaTime);
        
    }
    public void SetGold(int value)
    {
        IsGold = true;
        Value=value;
        Foto.sprite = FotoGold;
        NumberText.text = Value.ToString();
        if(Value< 0)
        {
            NumberText.color=Color.red;
        }
        Invoke("StartMove", StopTime);
    }

    public void SetGem(int value)
    {
        IsGold = false;
        Value = value;
        Foto.sprite = FotoGem;

        NumberText.text = Value.ToString();
        if (Value < 0)
        {
            NumberText.color = Color.red;
        }

        Invoke("StartMove", StopTime);
    }

    void StartMove()
    {
        Move = true;
    }
}
