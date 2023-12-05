using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace sihyeon
{
    interface IDestroyAble
    {
        public void Destroy();
    }


    public enum Building_TYPE
    {
        CAT_SAWMILL,
        CAT_BARRACKS,
        CAT_WORKSHOP,
        WOOD_BASE,
        BIRD_NEST
    }

    public enum playerState
    {
        CAT,
        WOOD,
        BIRD
    }


    public class Building
    {
        public playerState state;
        public Building_TYPE type;
        public int cost;



        public void BuildBuilding(Building_TYPE buildingName)
        {
            Debug.Log(buildingName + "건설");
        }


    }


    public class Build : MonoBehaviour, IDestroyAble
    {
        public Dictionary<string, Building> BuildingDics;


        public BuildingManager BuildMgr;

        private void Start()
        {
            BuildMgr = BuildingManager.Instance;
            BuildingDics = BuildMgr.BuildingDics;
            BuildMgr.setBuilding();
        }


        public void BuildToBuilding(string buildingName)
        {
            if (BuildingDics.ContainsKey(buildingName))
            {
                BuildingDics[buildingName].BuildBuilding(BuildingDics[buildingName].type);
            }


        }




        public void Destroy()
        {
            Debug.Log("파괴됨");
        }


        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Q))
            {
                //테스트용 
            }
            if (Input.GetKeyDown(KeyCode.W))
            {

            }
        }








    }

}
