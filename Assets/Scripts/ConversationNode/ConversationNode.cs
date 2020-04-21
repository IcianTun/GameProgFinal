using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConversationNode", menuName = "ScriptableObjects/ConversationNode", order = 1)]
public class ConversationNode : ScriptableObject
{
    public string message;
    public ConversationNode nextNode;
}
