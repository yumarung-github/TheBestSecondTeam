using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace sihyeon
{
    public class BuildingManager : SingleTon<BuildingManager>
    {
        public Dictionary<string, GameObject> BuildingDics = new Dictionary<string, GameObject>();
        //빌딩의 딕셔너리, 건설할수있는 건물들
        [Header("테스트용 버튼들")]

        public Button buildCatABtn;
        public Button buildCatBBtn;
        public Button buildCatCBtn;
        public Button buildWoodBaseBtn;
        public Button buildBirdNestBtn;


        [Header("리스트")]

        public List<Building> buildingList;
        //건설된 건물의 리스트.

        [Header("테스트용 게임오브젝트aka 건물")]
        public GameObject catSawMillPrefab;
        public GameObject catBarrackPrefab;
        public GameObject catWorkShopPrefab;
        public GameObject birdNestPrefab;
        public GameObject woodBasePrefab;




        private void Start()
        {
            buildingList = new List<Building>();
            setBuilding();
        }
        private new void Awake()
        {
            base.Awake();
        }
        public void buildBuilding(Building building)
        {
            Debug.Log(building.type + "건설test");
        }

        public void setBuilding()
        {

            BuildingDics.Add("catSawMill", catSawMillPrefab);
            BuildingDics.Add("catBarracks", catBarrackPrefab);
            BuildingDics.Add("catWorkShop", catWorkShopPrefab);
            BuildingDics.Add("woodBase", woodBasePrefab);
            BuildingDics.Add("birdNest", birdNestPrefab);
        }

        //테스트용 버튼 // 
        /*public void SetBtnTest()
        {
            buildCatABtn.onClick.RemoveAllListeners();
            buildCatABtn.onClick.AddListener(() => {
                //buildBuilding(BuildingDics["catSawmill"]);
                //TestSpawnBuilding(BuildingDics["catSawmill"]);
                //buildingList.Add(BuildingDics["catSawmill"]);
            });
        }
        */

        public void TestSpawnBuilding(GameObject building)
        {
            NodeMember node = RoundManager.Instance.mapController.nowTile;
            GameObject buildingPrefab = Instantiate(building, node.transform.position, Quaternion.identity);
        }

    }
}
