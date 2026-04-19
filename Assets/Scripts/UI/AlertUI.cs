using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AlertUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Text messageText;
    [SerializeField] private CanvasGroup canvasGroup;

    [Header("Timing")]
    [SerializeField] private float fadeInDuration = 0.25f;
    [SerializeField] private float displayDuration = 2f;
    [SerializeField] private float fadeOutDuration = 0.25f;
    [SerializeField] private float delayBetweenMessages = 0.2f;

    private readonly Queue<string> messageQueue = new();
    private Coroutine queueCoroutine;
    private bool isShowingMessage;

    public static AlertUI Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();

        if (canvasGroup != null)
            canvasGroup.alpha = 0f;
    }

    public void ShowAlert(string message)
    {
        if (string.IsNullOrWhiteSpace(message))
            return;

        messageQueue.Enqueue(message);

        if (queueCoroutine == null)
            queueCoroutine = StartCoroutine(ProcessQueue());
    }

    private IEnumerator ProcessQueue()
    {
        while (messageQueue.Count > 0)
        {
            isShowingMessage = true;

            string message = messageQueue.Dequeue();
            messageText.text = message;

            yield return FadeCanvas(0f, 1f, fadeInDuration);
            yield return new WaitForSeconds(displayDuration);
            yield return FadeCanvas(1f, 0f, fadeOutDuration);

            isShowingMessage = false;

            if (messageQueue.Count > 0)
                yield return new WaitForSeconds(delayBetweenMessages);
        }

        queueCoroutine = null;
    }

    private IEnumerator FadeCanvas(float from, float to, float duration)
    {
        if (canvasGroup == null)
            yield break;

        if (duration <= 0f)
        {
            canvasGroup.alpha = to;
            yield break;
        }

        float elapsed = 0f;
        canvasGroup.alpha = from;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            canvasGroup.alpha = Mathf.Lerp(from, to, t);
            yield return null;
        }

        canvasGroup.alpha = to;
    }

    public void ClearQueue()
    {
        messageQueue.Clear();
    }

    public bool IsShowingMessage()
    {
        return isShowingMessage;
    }
}