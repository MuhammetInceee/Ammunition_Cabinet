using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MuhammetInce.DesignPattern.Singleton;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private List<Image> praiseWords = new List<Image>();

    [SerializeField] private GameObject praiseHolder;
    
    [SerializeField] private Vector3 bigPraiseWordScale;
    [SerializeField] private Vector3 smallPraiseWordScale;
    [SerializeField] private float praiseBiggerDuration;

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

    public void PraiseVisualizer()
    {
        var randomPraise = Random.Range(0, praiseWords.Count);
        var selectedPraiseObject = praiseWords[randomPraise].gameObject;
        selectedPraiseObject.SetActive(true);
        praiseWords[randomPraise].rectTransform.position = praiseHolder.transform.position;
        selectedPraiseObject.transform.DOScale(bigPraiseWordScale, praiseBiggerDuration).OnComplete(() =>
            selectedPraiseObject.transform.DOScale(smallPraiseWordScale, praiseBiggerDuration));
        StartCoroutine(PraiseDeactivaterCoroutine(selectedPraiseObject));
    }

    private IEnumerator PraiseDeactivaterCoroutine(GameObject obj)
    {
        var waitForSecond = new WaitForSeconds(praiseBiggerDuration * 2);
        yield return waitForSecond;
        
        obj.SetActive(false);
    }
}
