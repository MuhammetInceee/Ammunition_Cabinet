using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using DG.Tweening;
using MuhammetInce.DesignPattern.Singleton;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class UIManager : Singleton<UIManager>
{
    [Header("Lists")]
    [SerializeField] private List<Image> praiseWords = new List<Image>();

    [Header("Objects"), Space]
    [SerializeField] private GameObject praiseHolder;
    [SerializeField] private Image crossSprite;
    
    [Header("Vectors"), Space]
    [SerializeField] private Vector3 bigPraiseWordScale;
    [SerializeField] private Vector3 smallPraiseWordScale;
    
    [Header("Floats"), Space]
    [SerializeField] private float praiseBiggerDuration;

    [Header("Integers"), Space]
    [SerializeField] private int counterFirstValue = 2;
    public int praiseCounter;

    private void Start()
    {
        praiseCounter = counterFirstValue;
    }

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
        if(praiseCounter == 0)
        {
            var randomPraise = Random.Range(0, praiseWords.Count);
            var selectedPraiseObject = praiseWords[randomPraise].gameObject;
            selectedPraiseObject.SetActive(true);
            praiseWords[randomPraise].rectTransform.position = praiseHolder.transform.position;
            selectedPraiseObject.transform.DOScale(bigPraiseWordScale, praiseBiggerDuration).OnComplete(() =>
                selectedPraiseObject.transform.DOScale(smallPraiseWordScale, praiseBiggerDuration));
            StartCoroutine(PraiseDeactivaterCoroutine(selectedPraiseObject));
            praiseCounter = counterFirstValue;
        }
        
        else praiseCounter--;
    }

    private IEnumerator PraiseDeactivaterCoroutine(GameObject obj)
    {
        var waitForSecond = new WaitForSeconds(praiseBiggerDuration * 2);
        yield return waitForSecond;
        
        obj.SetActive(false);
    }

    public void CrossVisualizer()
    {
        crossSprite.gameObject.SetActive(true);
        crossSprite.transform.DOScale(bigPraiseWordScale, praiseBiggerDuration).OnComplete(() =>
            crossSprite.transform.DOScale(smallPraiseWordScale, praiseBiggerDuration));
        StartCoroutine(PraiseDeactivaterCoroutine(crossSprite.gameObject));
    }
}
