using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<Slot> slot = new List<Slot>();
    public Transform parentTransfom;
    private void Awake()
    {
        slot = parentTransfom.GetComponentsInChildren<Slot>().ToList();
    }
    public void Use()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            slot[0].UseCard();
    }
    public void AddCard(Card card)
    {
        for (int i = 0; i < slot.Count; i++)
        {
            Debug.Log(slot[i].card);
            if (slot[i].card == null)
            {
                slot[i].SetItem(card);
                return;
            }
        }
    }
    private void Update()
    {
        Use();
    }
}

//namespace sihyeon
//{
//    public class PlayerInventory : MonoBehaviour
//    {

//        Dictionary<string,GameObject> cardInventory = new Dictionary<string, GameObject>();
//        Dictionary<string , GameObject> popInventory = new Dictionary<string , GameObject>();
//        Dictionary<string, GameObject> resourceInventory = new Dictionary<string , GameObject>();
//        public void UseCard()
//        {
//            Debug.Log("카드 사용했음");
//        }

//        public void AddCard(GameObject card)
//        {
//            cardInventory.Add(card.name, card);
//        }
//        public void UseCard(GameObject card)
//        {
//            cardInventory.Remove(card.name);
//        }
//    }
//}