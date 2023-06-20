using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_GridCell : MonoBehaviour
{
    public Vector2Int GridPos;
    [SerializeField] GameObject EmptyImage;
    [SerializeField] GameObject ConstructionImage;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnImageBuildingCompleated()
    {
        EmptyImage.SetActive(false);
        ConstructionImage.SetActive(false);
    }
    public void TurnImageConstruct()
    {
        EmptyImage.SetActive(false);
        ConstructionImage.SetActive(true);
    }

    public void TurnImageEmpty()
    {
        EmptyImage.SetActive(true);
        ConstructionImage.SetActive(false);
    }
}
