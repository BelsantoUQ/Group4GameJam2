using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField] 
    private TextMeshProUGUI allyText;
    
    private float lifePoints;
    private int powerUps;
    private bool powerAble;

    public Slider lifeScore;

    
    void Start()
    {
        powerUps = 1;
        lifePoints = 100;
        ChangeLife();
        ChangeAllyText(FindObjectsOfType<NavigationAlly>().Length);
        powerAble = false;
    }

    public void SetPowerUp()
    {
        powerUps += 1;
    }

    public void SetAblePowerUp(bool active)
    {
        powerAble = active;
    }

    public bool GetAblePowerUp()
    {
        return powerAble;
    }

    public int GetPowerUp()
    {
        return powerUps;
    }

    public float GetLifePoint()
    {
        return lifePoints;
    }
    
    public void SetLifePoint(float rest)
    {
        lifePoints +=rest;
        ChangeLife();
    }
    
    private void ChangeLife()
    {
        lifeScore.value = lifePoints;
        if (lifePoints < 1)
        {
            SceneManager.LoadScene("Restart");
        }
    }

    public void ChangeAllyText(int allies)
    {
//        Debug.Log("Allies : "+allies);
        allyText.text = "" + allies;
        
    }

}