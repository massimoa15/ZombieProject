using System;
using System.Collections;
using Entities;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatDisplayer : MonoBehaviour
{
    public Text text;
    private MBPlayer activePlayer = null;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player") && activePlayer == null)
        {
            activePlayer = other.gameObject.GetComponent<MBPlayer>();
            text.text = activePlayer.GetStatString();
            StartCoroutine(FadeInTextCoroutine(0.25f, text));
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag.Equals("Player") && other.gameObject.GetComponent<MBPlayer>() == activePlayer)
        {
            StartCoroutine(FadeOutTextCoroutine(0.25f, text));
            activePlayer = null;
        }
    }

    IEnumerator FadeInTextCoroutine(float fadeDuration, Text text)
    {
        float elapsedTime = 0;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            text.color = Color.Lerp(new Color(1,1,1,0), Color.white, elapsedTime / fadeDuration);
            yield return null;
        }
    }

    IEnumerator FadeOutTextCoroutine(float fadeDuration, Text text)
    {
        float elapsedTime = 0;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            text.color = Color.Lerp(Color.white, new Color(1,1,1,0), elapsedTime / fadeDuration);
            yield return null;
        }
    }
}
