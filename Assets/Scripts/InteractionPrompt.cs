using TMPro;
using UnityEngine;
using System.Collections;

public class InteractionPrompt : MonoBehaviour
{
    [SerializeField] private TMP_Text promptText;

    [Header("Message Settings")]
    [SerializeField] private float messageDuration = 2f;

    private Coroutine hideCoroutine;

    private void Awake()
    {
        HidePrompt();
    }

    public void ShowGrabPrompt(string itemName)
    {
        StopHideCoroutine();

        promptText.text = "Use Grab Button to grab " + itemName;
        promptText.gameObject.SetActive(true);
    }

    public void ShowStorePrompt(string itemName)
    {
        StopHideCoroutine();

        promptText.text = "Press A to store " + itemName;
        promptText.gameObject.SetActive(true);
    }

    public void ShowMessage(string message)
    {
        StopHideCoroutine();

        promptText.text = message;
        promptText.gameObject.SetActive(true);

        hideCoroutine = StartCoroutine(HideAfterDelay());
    }

    private IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(messageDuration);

        HidePrompt();
        hideCoroutine = null;
    }

    public void HidePrompt()
    {
        promptText.gameObject.SetActive(false);
    }

    private void StopHideCoroutine()
    {
        if (hideCoroutine != null)
        {
            StopCoroutine(hideCoroutine);
            hideCoroutine = null;
        }
    }
}