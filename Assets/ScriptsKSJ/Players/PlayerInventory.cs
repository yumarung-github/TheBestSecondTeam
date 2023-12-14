using CustomInterface;
using JetBrains.Annotations;
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
    public void AddCard(Card card)//굳이 맨뒤로가는데 매번 반복문 안돌려도될거같긴한데
    {
        for (int i = 0; i < slot.Count; i++)
        {
            //Debug.Log(slot[i].card);
            if (slot[i].card == null)
            {
                slot[i].SetItem(card);
                //Color32 tempColor = slot[i].image.color;
                //slot[i].image.color = new Color32(tempColor.r, tempColor.g, tempColor.b, 255);//카드를 색을 켜주거나
                slot[i].gameObject.SetActive(true);//걍 껐다키거나
                //초기화
                card.onActive = null;
                card.onActive += () => {
                    if (card.isUse == true)
                    {
                        Debug.Log(i+"번쨰 비움");
                        slot[i].EmptySlot();
                    }
                };
                return;
            }
        }
    }
    public void SetSort()
    {
        int tempCardNum = 0;
        foreach (KeyValuePair<ANIMAL_COST_TYPE, List<Card>> kv in RoundManager.Instance.nowPlayer.cardDecks)
        {
            tempCardNum += kv.Value.Count;
        }
        Debug.Log("남은카드" + tempCardNum);
        for (int j = 0; j < tempCardNum; j++)
        {
            Debug.Log("도는거 체크" + j);
            if (slot[j].card == null)
            {
                slot[j + 1].card.onActive = null;
                slot[j + 1].card.onActive += () =>
                {
                    Debug.Log(j + "번쨰 비움");
                    if (slot[j].card.isUse == true)
                    {                        
                        slot[j].EmptySlot();
                    }
                };
                slot[j].card = slot[j + 1].card;
                slot[j].image.sprite = slot[j + 1].image.sprite;
                slot[j + 1].card = null;
                slot[j + 1].image.sprite = null;

                
                Debug.Log(tempCardNum);
                Debug.Log(j);
                
                
            }
        }     
        
        if (slot[tempCardNum].card == null)
        {
            //Color32 tempColor = slot[tempCardNum].image.color;
            //slot[tempCardNum].image.color = new Color32(tempColor.r, tempColor.g, tempColor.b, 0);
            slot[tempCardNum].gameObject.SetActive(false);
        }
        
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