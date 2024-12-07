using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Blinker : MonoBehaviour
{
    public float fadeOutSpeed = 1.0f; // フェードアウトのスピード
    public float blinkSpeed = 5.0f;   // 点滅のスピード
    public float blinkInterval = 0.5f; // 点滅のインターバル
    private Text text;
    private Outline outline;
    private bool isBlinking = true;

    // フェードアウト時の最大サイズ倍率
    public float maxScaleMultiplier = 2.0f;

    void Start()
    {
        text = this.gameObject.GetComponent<Text>();
        outline = this.gameObject.GetComponent<Outline>();

        if (outline == null)
        {
            Debug.LogError("Outline component is missing on this game object");
        }
    }

    void Update()
    {
        if (isBlinking)
        {
            BlinkOutline();
        }
    }

    void BlinkOutline()
    {
        if (outline != null)
        {
            Color color = outline.effectColor;
            float alpha = Mathf.PingPong(Time.time * blinkSpeed, 1.0f);

            if (alpha > blinkInterval)
            {
                color.a = alpha;
            }
            else
            {
                color.a = 0;
            }

            outline.effectColor = color;
        }
    }

    public IEnumerator FadeOut()
    {
        Color textColor = text.color;
        Color outlineColor = outline.effectColor;
        Vector3 originalScale = text.transform.localScale;
        Vector3 targetScale = originalScale * maxScaleMultiplier;

        float fadeDuration = 3.0f; // フェードアウトの持続時間を3秒に設定
        float elapsedTime = 0.0f;

        while (textColor.a > 0)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
            textColor.a = alpha;
            text.color = textColor;

            outlineColor.a = alpha;
            outline.effectColor = outlineColor;

            text.transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / fadeDuration);

            yield return null;
        }

        // 完全に透明になった後、オブジェクトを非アクティブにする場合
        gameObject.SetActive(false);
    }

    public void StopBlinking()
    {
        isBlinking = false;
    }
}
