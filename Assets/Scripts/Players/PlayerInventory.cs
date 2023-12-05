using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace sihyeon
{
    public class PlayerInventory : MonoBehaviour
    {
        
        Dictionary<string,GameObject> cardInventory = new Dictionary<string, GameObject>();
        Dictionary<string , GameObject> popInventory = new Dictionary<string , GameObject>();
        Dictionary<string, GameObject> resourceInventory = new Dictionary<string , GameObject>();

       
        public void UseCard()
        {
            Debug.Log("카드 사용했음");
        }

        public void AddCard(GameObject card)
        {
            cardInventory.Add(card.name, card);
        }
        public void UseCard(GameObject card)
        {

            cardInventory.Remove(card.name);
        }







    }




}




public class PlayerInventory : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
