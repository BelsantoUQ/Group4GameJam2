using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{

    [SerializeField] 
    private TextMeshProUGUI lifeText;
    [SerializeField] 
    private TextMeshProUGUI allyText;
    
    private float lifePoints;
    
    // Start is called before the first frame update
    void Start()
    {
        lifePoints = 100;
        ChangeLifeText();
        ChangeAllyText(FindObjectsOfType<NavigationAlly>().Length);
    }


    public float GetLifePoint()
    {
        return lifePoints;
    }

    public void SetLifePoint(float rest)
    {
        lifePoints +=rest;
        ChangeLifeText();
    }

    private void ChangeLifeText()
    {
        lifeText.text = "Turrets Life Points: " + lifePoints;
    }

    public void ChangeAllyText(int allies)
    {
//        Debug.Log("Allies : "+allies);
        allyText.text = "Allies : " + allies;
    }
    
}