using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeMember : MonoBehaviour
{
    public Node node;
    public string nodeName;
    

    void Awake()
    {
        node = new Node(nodeName);
        //Debug.Log(nodeName + "»ý¼º");
    }
    void tempdd()
    {
        

    }
}
