using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace sihyeon
{
    public class BuildingManager : SingleTon<BuildingManager>
    {
        public Dictionary<string, Building> BuildingDics = new Dictionary<string, Building>();

        [Header("테스트용 버튼들")]

        public Button buildCatABtn;
        public Button buildCatBBtn;
        public Button buildCatCBtn;
        public Button buildWoodBaseBtn;
        public Button buildBirdNestBtn;

        private void Start()
        {
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

        //박스 생성후에 타입넣고(스크립트까지) 
        //
        public void setBuilding()
        {
            Building catSawmill = new Building();
            catSawmill.type = Building_TYPE.CAT_SAWMILL;

            //추후 자원과 관련된 코스트 까지 넣을예정.
            //임시
            Building catBarracks = new Building();
            catBarracks.type = Building_TYPE.CAT_BARRACKS;

            Building catWorkShop = new Building();
            catWorkShop.type = Building_TYPE.CAT_WORKSHOP;

            Building woodBase = new Building();
            woodBase.type = Building_TYPE.WOOD_BASE;

            Building birdNest = new Building();
            birdNest.type = Building_TYPE.BIRD_NEST;

            BuildingDics.Add("catSawmill", catSawmill);
            BuildingDics.Add("catBarracks", catBarracks);
            BuildingDics.Add("catWorkShop", catWorkShop);
            BuildingDics.Add("woodBase", woodBase);
            BuildingDics.Add("birdNest", birdNest);
            SetBtnTest();
        }


        public void SetBtnTest()
        {
            buildCatABtn.onClick.AddListener(() => {
                buildBuilding(BuildingDics["catSawmill"]);
            });
        }





    }
}
