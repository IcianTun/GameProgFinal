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
    void Start()
    {
        dialogBox.SetActive(false);
        timerDisplay = -1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerDisplay >= 0)
        {
            timerDisplay -= Time.deltaTime;
            if (timerDisplay < 0)
            {
                dialogBox.SetActive(false);
                currentConversationNode = startConversationNode;
            }
        }
    }

    public void DisplayDialog()
    {
        timerDisplay = displayTime;
        if (!dialogBox.activeInHierarchy)
        {
            currentConversationNode = startConversationNode;
            dialogBox.SetActive(true);
        } else if(currentConversationNode.nextNode)
        {
            currentConversationNode = currentConversationNode.nextNode;
        }
        dialogText.SetText(currentConversationNode.message);
    }
}
