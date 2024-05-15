using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class FirstMonologue : MonoBehaviour
{
    public GameObject fadeIn;
    public GameObject fadeOut;
    
    public NPCConversation monolog;


    void Start()
    {
        fadeOut.SetActive(false);
        fadeIn.SetActive(true);

        StartCoroutine(WaitForSecond());
    }

    IEnumerator WaitForSecond()
    {
        yield return new WaitForSeconds(0.5f);

        ConversationManager.Instance.StartConversation(monolog);
    }

    /*
    private void OnDestroy()
    {
        ConversationManager.OnConversationEnded -= OnConversationEnded;
    }

    public void OnConversationEnded()
    {
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Dialogue reference is not set");
        }
    }
    */
}
