using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomCursorController : MonoBehaviour
{
    public Texture2D cursorTexture;  // 默认的自定义光标图像
    public Texture2D[] rotatingCursorTextures; // 旋转Sprite的序列
    public Vector2 hotSpot = new Vector2(0, 0);  // 光标的"活动点"，通常是光标的中心或指针的尖端

    private int currentRotateIndex = 0;

    private void Start()
    {
        // 设置默认的自定义光标
        Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.Auto);

        // 检查当前场景是否为“Load”
        if (SceneManager.GetActiveScene().name == "Load")
        {
            StartCoroutine(StartRotatingCursorAfterDelay(35f));
        }
    }

    private IEnumerator StartRotatingCursorAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // 开始循环播放旋转Sprite
        StartCoroutine(RotateCursor());
    }

    private IEnumerator RotateCursor()
    {
        while (true)
        {
            Cursor.SetCursor(rotatingCursorTextures[currentRotateIndex], hotSpot, CursorMode.Auto);
            currentRotateIndex = (currentRotateIndex + 1) % rotatingCursorTextures.Length;
            yield return new WaitForSeconds(0.125f);
        }
    }
}