using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace KittyCook.Helpers
{
    public class Timer : MonoBehaviour
    {
        public static Timer Instance;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                return;
            }

            Destroy(gameObject);
        }

        public void WaitForSecondsRealtime(float seconds, UnityAction callback)
        {
            StartCoroutine(WaitForSecondsRealtimeCor(seconds, callback));
        }

        public void WaitForSeconds(float seconds, UnityAction callback)
        {
            StartCoroutine(WaitForSecondsCor(seconds, callback));
        }

        public void WaitUntil(System.Func<bool> predicate, UnityAction callback)
        {
            StartCoroutine(WaitUntilCor(predicate, callback));
        }

        public void WaitWhile(System.Func<bool> predicate, UnityAction callback)
        {
            StartCoroutine(WaitWhileCor(predicate, callback));
        }

        private IEnumerator WaitForSecondsRealtimeCor(float seconds, UnityAction callback)
        {
            yield return new WaitForSecondsRealtime(seconds);
            callback?.Invoke();
        }

        private IEnumerator WaitForSecondsCor(float seconds, UnityAction callback)
        {
            yield return new WaitForSeconds(seconds);
            callback?.Invoke();
        }

        private IEnumerator WaitUntilCor(System.Func<bool> predicate, UnityAction callback)
        {
            yield return new WaitUntil(predicate);

            callback?.Invoke();
        }

        private IEnumerator WaitWhileCor(System.Func<bool> predicate, UnityAction callback)
        {
            yield return new WaitWhile(predicate);

            callback?.Invoke();
        }
    }
}