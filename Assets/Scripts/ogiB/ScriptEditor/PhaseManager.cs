using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    public List<GameObject> _phaseList;   
    public float _zVariable;
    public List<GameObject> _instantiatePhase;
    public bool _isRandom;

    float _zPos;
    void Start()
    {
        _zPos = 0;
    }
    void Update()
    {
        
    }
    public void GenerateLevel()
    {
        _zPos = 0;
        if (!_isRandom)
        {
            for (int i = 0; i < _phaseList.Count; i++)
            {
                var phase = Instantiate(_phaseList[i], new Vector3(0, 0, _zPos), Quaternion.identity);
                _zPos += _zVariable;
                _instantiatePhase.Add(phase);
            }
        }
        else
        {
            for (int i = 0; i < _phaseList.Count; i++)
            {
                var phase = Instantiate(_phaseList[Random.Range(0,_phaseList.Count)], new Vector3(0, 0, _zPos), Quaternion.identity);
                _zPos += _zVariable;
                _instantiatePhase.Add(phase);
            }
        }
    }
    public void DeleteAll()
    {
        for(int i = 0; i < _instantiatePhase.Count; i++)
        {
            DestroyImmediate(_instantiatePhase[i]);
        }
        _instantiatePhase.Clear();
    }
  
}

