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

    public Slider lifeScore;

    public Image bloodEffect;

    private float r;
    private float g;
    private float b;
    private float a;

    void Start()
    {
        lifePoints = 100;
        ChangeLife();
        ChangeAllyText(FindObjectsOfType<NavigationAlly>().Length);

        r = bloodEffect.color.r;
        g = bloodEffect.color.g;
        b = bloodEffect.color.b;
        a = bloodEffect.color.a;
    }

    public float GetLifePoint()
    {
        return lifePoints;
    }
    
    public void SetLifePoint(float rest)
    {
        lifePoints +=rest;
        ChangeLife();

        a += 0.01f;
        a = Mathf.Clamp(a, 0, 1f);
        ChangeColor();
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

    public void ChangeColor()
    {
        Color c = new Color(r, g, b);
        bloodEffect.color = c;
    }

}