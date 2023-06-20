using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class ScrCardInfo : ScriptableObject
{
    public Sprite BuildingImage;

    public GameObject SpawnBuilding;

    public string GoldPrice;

    public string GemPrice;

    public Vector2Int[] GridSize;

    public float XSizeMultiply = 1;

    public float YSizeMultiply = 1;

    public int GoldRevanueForTime;

    public int GemRevanueForTime;

    public float RevanueTime;

    public float BuildingTime;


}
