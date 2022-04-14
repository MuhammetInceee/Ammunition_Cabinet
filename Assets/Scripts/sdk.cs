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

    void Start()
    {
        GameAnalytics.Initialize();

        TinySauce.OnGameStarted();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
