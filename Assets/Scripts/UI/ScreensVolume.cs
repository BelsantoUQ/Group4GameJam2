using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreensVolume : MonoBehaviour
{
    public static ScreensVolume instance;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        } else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
