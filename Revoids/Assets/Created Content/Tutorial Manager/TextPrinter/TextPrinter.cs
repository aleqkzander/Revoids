using System.Collections;
using TMPro;
using UnityEngine;

public class TextPrinter : MonoBehaviour
{
    public TMP_Text printerText;

    public void StartTutorial()
    {
        string textToPrint = printerText.text;
        printerText.text = string.Empty;
        StartCoroutine(PrintTutorialText(textToPrint));
    }

    private IEnumerator PrintTutorialText(string text)
    {
        for (int i = 0; i < text.Length; i++)
        {
            printerText.text = printerText.text + text[i];
            yield return new WaitForSeconds(0.1f);
        }
    }
}
