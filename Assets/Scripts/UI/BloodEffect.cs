using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodEffect : MonoBehaviour
{
    public Image bloodEffect;

    private float r;
    private float g;
    private float b;
    private float a;

    public void Start()
    {
        r = bloodEffect.color.r;
        g = bloodEffect.color.g;
        b = bloodEffect.color.b;
        a = bloodEffect.color.a;
    }

    public void DamageEffect()
    {
        a += 0.01f;
        a = Mathf.Clamp(a, 0, 1f);

        ChangeColor();
    }

    public void ChangeColor()
    {
        Color c = new Color(r, g, b);
        bloodEffect.color = c;
    }
}
