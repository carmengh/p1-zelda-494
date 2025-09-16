using System.Collections;
using TMPro;
using UnityEngine;

public class Typewriter : MonoBehaviour
{
    public TextMeshProUGUI textUI;
    [TextArea] public string fullText;
    public float delay = 0.05f;

    private Coroutine typingCoroutine;
    private bool hasTyped = false;

    private void Start()
    {
        textUI.text = ""; // Start empty
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTyped)
        {
            typingCoroutine = StartCoroutine(TypeText());
            hasTyped = true;
        }
    }

    IEnumerator TypeText()
    {
        textUI.text = "";

        foreach (char c in fullText)
        {
            textUI.text += c;
            yield return new WaitForSeconds(delay);
        }
    }
}