using sihyeon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace sihyeon
{
    public class BuildingManager : SingleTon<BuildingManager>
    {
        public Dictionary<string, Building> BuildingDics = new Dictionary<string, Building>();

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
            BuildingDics.Add("catWorkShop", catBarracks);
            BuildingDics.Add("woodBase", woodBase);
            BuildingDics.Add("birdNest", birdNest);
        }









    }
}
