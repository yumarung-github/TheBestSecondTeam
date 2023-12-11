using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace sihyeon
{
    public class BuildingManager : SingleTon<BuildingManager>
    {
        public Dictionary<string, Building> BuildingDics = new Dictionary<string, Building>();
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
            Building catSawMill = new Building();
            catSawMill.type = Building_TYPE.CAT_SAWMILL;
            catSawMill.cost = 1;
            catSawMill.buildingPrefabs = catSawMillPrefab;
            Building catBarracks = new Building();
            catBarracks.type = Building_TYPE.CAT_BARRACKS;
            catBarracks.cost = 1;
            catBarracks.buildingPrefabs = catBarrackPrefab;
            Building catWorkShop = new Building();
            catWorkShop.type = Building_TYPE.CAT_WORKSHOP;
            catWorkShop.cost = 1;
            catWorkShop.buildingPrefabs = catWorkShopPrefab;
            Building woodBase = new Building();
            woodBase.type = Building_TYPE.WOOD_BASE;
            woodBase.cost = 1;
            woodBase.buildingPrefabs = woodBasePrefab;
            Building birdNest = new Building();
            birdNest.type = Building_TYPE.BIRD_NEST;
            birdNest.cost = 1;
            birdNest.buildingPrefabs = birdNestPrefab;
            BuildingDics.Add("catSawmill", catSawMill);
            BuildingDics.Add("catBarracks", catBarracks);
            BuildingDics.Add("catWorkShop", catWorkShop);
            BuildingDics.Add("woodBase", woodBase);
            BuildingDics.Add("birdNest", birdNest);
            SetBtnTest();
        }

        //테스트용 버튼 // 
        public void SetBtnTest()
        {
            buildCatABtn.onClick.RemoveAllListeners();
            buildCatABtn.onClick.AddListener(() => {
                buildBuilding(BuildingDics["catSawmill"]);
                TestSpawnBuilding(BuildingDics["catSawmill"]);
                buildingList.Add(BuildingDics["catSawmill"]);
            });
        }


        public void TestSpawnBuilding(Building building)
        {

            NodeMember node = RoundManager.Instance.mapController.nowTile;

            GameObject buildingPrefab = Instantiate(building.buildingPrefabs, node.transform.position, Quaternion.identity);


        }

    }
}
