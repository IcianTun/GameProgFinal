using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AutoDialog : MonoBehaviour
{
    private float intervalTime = 6.0f;
    public float timerDisplay;
    private float sentenceInterval = 10f;

    public GameObject dialogBox;
    public TMP_Text dialogText;

    public List<ConversationNode> startNodeList;
    
    public ConversationNode currentConversationNode;

    public bool display = true;

    void Start()
    {
        timerDisplay = -1;
        display = true;
    }

    // Update is called once per frame
    void Update()
    { 
        timerDisplay -= Time.deltaTime;
        if (timerDisplay < 0)
        {
            if (display) { 
                DisplayDialog();
            } else
            {
                dialogBox.SetActive(false);
                timerDisplay = sentenceInterval;
                display = true;
            }
        }


    }

    virtual public void DisplayDialog()
    {
        timerDisplay = intervalTime;
        dialogBox.SetActive(true);

        if (!currentConversationNode || !currentConversationNode.nextNode)
        {
            currentConversationNode = startNodeList[Random.Range(0, startNodeList.Count)];
        } else
        {
            currentConversationNode = currentConversationNode.nextNode;
            if (!currentConversationNode.nextNode)
            {
                display = false;
            }
        }

        dialogText.SetText(currentConversationNode.message);
    }
}
