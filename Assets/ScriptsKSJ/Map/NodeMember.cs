using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomInterface;

public class NodeMember : MonoBehaviour,IEnumerable<NodeMember>
{
    public Node node;
    public string nodeName;
    public ANIMAL_COST_TYPE nodeType;

    public IEnumerator<NodeMember> GetEnumerator()
    {
        throw new System.NotImplementedException();
    }

    void Awake()
    {
        node = new Node(nodeName);
        //Debug.Log(nodeName + "생성");
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new System.NotImplementedException();
    }

    void tempdd()
    {
        

    }
}
