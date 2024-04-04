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

        public void WaitForSeconds(float seconds, UnityAction callback)
        {
            StartCoroutine(WaitForSecondsCor(seconds, callback));
        }

        private IEnumerator WaitForSecondsCor(float seconds, UnityAction callback)
        {
            yield return new WaitForSeconds(seconds);
            callback?.Invoke();
        }
    }
}