using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SpriteSwitcher : MonoBehaviour
{
    public Image targetImage;           // UI Image组件，要更改其Sprite的组件
    public Sprite[] spritesToCycle;     // 要循环切换的Sprite数组
    public float switchInterval = 0.5f; // 切换的间隔时间

    private int currentIndex = 0;

    // 可以使用一个静态变量来存储全局状态
    public static bool isBusy = false;

    private void Start()
    {
        // 如果有Sprite要切换，则开始切换
        if (spritesToCycle.Length > 0)
        {
            StartCoroutine(SwitchSprites());
        }

        // 开始计时协程
        StartCoroutine(StartBusyAfterDelay(26f));
    }

    private IEnumerator SwitchSprites()
    {
        while (!isBusy)
        {
            targetImage.sprite = spritesToCycle[currentIndex];
            
            currentIndex = (currentIndex + 1) % spritesToCycle.Length;
            targetImage.SetNativeSize();
            yield return new WaitForSeconds(switchInterval);
        }
    }

    private IEnumerator StartBusyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isBusy = true;
    }
}