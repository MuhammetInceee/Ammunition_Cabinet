using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtonsManager : MonoBehaviour
{
    public void NextLevelButton()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            AsyncOperation asyn = SceneManager.LoadSceneAsync(Random.Range(0, 2));
        }

        else if (SceneManager.GetActiveScene().buildIndex < 2)
        {
            AsyncOperation asyn = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
