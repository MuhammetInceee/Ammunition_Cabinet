using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuhammetInce.DesignPattern.Singleton
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = GameObject.FindObjectOfType<T>();

                    if (_instance == null)
                        _instance = new GameObject(name: "Instance of " + typeof(T)).AddComponent<T>();
                }

                return _instance;
            }
        }

        private void Awake()
        {
            if (_instance == null) Destroy(this.gameObject);
        }
    }
}
