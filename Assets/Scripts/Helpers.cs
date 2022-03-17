using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

namespace MuhammetInce.HelperUtils
{
    public static class Helpers
    {
        public static async void DefaultLayerAsync(GameObject obj, int milliseconds)
        {
            await Task.Delay(milliseconds);
            obj.layer = 0;
        }

        public static async void IgnoreRayLayerAsync(GameObject obj, int milliseconds)
        {
            await Task.Delay(milliseconds);
            obj.layer = 2;
        }

        public static void CaseBigger(GameObject obj, float scaleFactor, float duration)
        {
            var scale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
            obj.transform.DOScale(scale, duration);
        }

        public static void CaseSmaller(GameObject obj, float defaultScaleFactor, float duration)
        {
            var defaultScale = new Vector3(defaultScaleFactor, defaultScaleFactor, defaultScaleFactor);
            obj.transform.DOScale(defaultScale, duration);
        }
    }
}