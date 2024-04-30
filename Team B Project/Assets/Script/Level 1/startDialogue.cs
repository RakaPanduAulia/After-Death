using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor; // Required for accessing Dialogue Manager

public class startDialogue : MonoBehaviour
{
    public NPCConversation conversation; // Conversation to start

    private void Start() 
    {
        ConversationManager.Instance.StartConversation(conversation); // Start the conversation    
    }
}
