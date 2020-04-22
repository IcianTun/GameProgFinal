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
        Debug.Log("DisplayDialog is called");
        timerDisplay = displayTime;
        if (!dialogBox.activeInHierarchy)
        {
            Debug.Log("case1");
            currentConversationNode = startConversationNode;
            dialogBox.SetActive(true);
        } else if(currentConversationNode.nextNode)
        {
            Debug.Log("case2");
            currentConversationNode = currentConversationNode.nextNode;
        }
        Debug.Log("set text");
        dialogText.SetText(currentConversationNode.message);
    }
}
