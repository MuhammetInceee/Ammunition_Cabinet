using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

namespace MuhammetInce.HelperUtils
{
    public static class HelperUtils
    {
        public static async void LayerChangerDefaultAsync(GameObject obj, int milliseconds)
        {
            await Task.Delay(milliseconds);
            obj.layer = 0;
        }

        public static async void LayerChangerIgnoreRaycastAsync(GameObject obj, int milliseconds)
        {
            await Task.Delay(milliseconds);
            obj.layer = 2;
        }

        public static void LayerChangerDefault(GameObject obj)
        {
            obj.layer = 0;
        }
        public static void LayerChangerIgnoreRaycast(GameObject obj)
        {
            obj.layer = 2;
        }

        public static void CaseBigger(GameObject obj, float scaleFactor, float secondDuration)
        {
            var scale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
            obj.transform.DOScale(scale, secondDuration);
        }

        public static void CaseSmaller(GameObject obj, float defaultScaleFactor, float secondDuration)
        {
            var defaultScale = new Vector3(defaultScaleFactor, defaultScaleFactor, defaultScaleFactor);
            obj.transform.DOScale(defaultScale, secondDuration);
        }

        public static void CaseRotater(GameObject obj, float rotateFactor, float secondDuration)
        {
            var rotateState = new Vector3(0, 90, -rotateFactor);
            obj.transform.DORotate(rotateState, secondDuration);
        }
        public static void DefaultCaseRotater(GameObject obj, float secondDuration)
        {
            var rotateState = new Vector3(0, 90, 0);
            obj.transform.DORotate(rotateState, secondDuration);
            
        }
    }
}