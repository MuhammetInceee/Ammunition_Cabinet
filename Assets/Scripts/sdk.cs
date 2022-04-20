using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;
using Facebook.Unity;

public class sdk : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        FB.Init();

        DontDestroyOnLoad(gameObject);
        
    }

    private void Start()
    {
        GameAnalytics.Initialize();

        TinySauce.OnGameStarted();
        
        TinySauce.OnGameFinished(true, 100);
    }
}
