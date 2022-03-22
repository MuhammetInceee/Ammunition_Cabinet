using UnityEngine;

namespace MuhammetInce.DesignPattern.Singleton
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance;

        private void Awake()
        {
            if (Instance == null) Instance = GameObject.FindObjectOfType<T>();
            else Destroy(gameObject);
        }
    }
}
