using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProgressBarFiller : MonoBehaviour
{
    public Image fillImage; // 这是你的UI Image组件

    private void Start()
    {
        StartCoroutine(FillProgress());
    }

    private IEnumerator FillProgress()
    {
        // Scene刚被load，fill amount = 0
        fillImage.fillAmount = 0f;

        // 在scene load之后的0-0.5秒内，fill amount从0逐渐变成0.05
        yield return FillGradually(0f, 0.05f, 0.5f);

        // 0.5秒-2秒内维持在0.05不变
        yield return new WaitForSeconds(1.5f);

        // 2秒到2.5秒内，从0.05逐渐变成0.3
        yield return FillGradually(0.05f, 0.3f, 0.5f);

        // 2.5秒到5秒不变
        yield return new WaitForSeconds(2.5f);

        // 5秒到10秒内逐渐从0.3变成0.5
        yield return FillGradually(0.3f, 0.5f, 5f);

        // 10秒到15秒内不变
        yield return new WaitForSeconds(5f);

        // 15秒到16秒内从0.5变成0.9
        yield return FillGradually(0.5f, 0.9f, 0.2f);

        // 此后不变
    }

    private IEnumerator FillGradually(float startValue, float endValue, float duration)
    {
        float startTime = Time.time;
        while (Time.time < startTime + duration)
        {
            float elapsed = (Time.time - startTime) / duration;
            fillImage.fillAmount = Mathf.Lerp(startValue, endValue, elapsed);
            yield return null;
        }
        fillImage.fillAmount = endValue;
    }
}