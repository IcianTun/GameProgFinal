using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NonPlayerCharacter : MonoBehaviour
{
    public float displayTime = 4.0f;
    public GameObject dialogBox;
    public TMP_Text dialogText;


    public ConversationNode startConversationNode;
    ConversationNode currentConversationNode;

    float timerDisplay;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        if (dialogBox != null)
        {
            dialogBox.SetActive(false);
        }
        timerDisplay = -1.0f;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (timerDisplay >= 0)
        {
            timerDisplay -= Time.deltaTime;
            if (timerDisplay < 0)
            {
                if(dialogBox != null)
                {
                    dialogBox.SetActive(false);
                    currentConversationNode = startConversationNode;
                }
            }
        }
    }

    virtual public void DisplayDialog()
    {
        timerDisplay = displayTime;
        if (dialogBox != null && !dialogBox.activeInHierarchy)
        {
            dialogBox.SetActive(true);
            if(currentConversationNode)
            currentConversationNode = startConversationNode;
        }

        if (currentConversationNode && currentConversationNode.nextNode)
        {
            currentConversationNode = currentConversationNode.nextNode;
        }
        if(dialogText)
        dialogText.SetText(currentConversationNode.message);
    }
}
