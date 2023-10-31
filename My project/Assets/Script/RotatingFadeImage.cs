using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SpriteSwitcherWithFade : MonoBehaviour
{
    public Image targetImage; // 要更换Sprite的Image组件
    public Sprite[] spritesToCycle; // 要切换的Sprite序列
    public float switchInterval = 0.5f; // 切换Sprite的间隔
    public float fadeDuration = 0.5f; // 淡入淡出的持续时间
    private bool isBusy = false; // 是否处于忙碌状态
    private int currentIndex = 0;

    private void Start()
    {
        if (spritesToCycle.Length > 0)
        {
            StartCoroutine(SwitchSpritesWithFade());
        }

        StartCoroutine(SetBusyAfterDelay(26f));
    }

    private IEnumerator SwitchSpritesWithFade()
    {
        while (!isBusy)
        {
            // Fade out
            yield return Fade(1f, 0f);

            // Switch sprite
            targetImage.sprite = spritesToCycle[currentIndex];
            currentIndex = (currentIndex + 1) % spritesToCycle.Length;
            targetImage.SetNativeSize();

            // Fade in
            yield return Fade(0f, 1f);

            yield return new WaitForSeconds(switchInterval - (2 * fadeDuration)); // We subtract twice the fade duration because we have both fade in and fade out.
        }
    }

    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            Color newColor = targetImage.color;
            newColor.a = alpha;
            targetImage.color = newColor;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Color finalColor = targetImage.color;
        finalColor.a = endAlpha;
        targetImage.color = finalColor;
    }

    private IEnumerator SetBusyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isBusy = true;
    }
}