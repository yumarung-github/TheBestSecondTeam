using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace sihyeon
{
    interface IDestroyAble
    {
        public void Destroy();
    }

    public enum Building_STATE
    {
        DESTROY,
        NONE
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
        public void Build()
        {
            Debug.Log("건설test1");
        }
    }


    public class Build : MonoBehaviour, IDestroyAble
    {
        public Dictionary<string, Building> BuildingDics;
        private BuildingManager BuildMgr;
        public Building_TYPE types;
        public int cost;

        private void Start()
        {
            BuildMgr = BuildingManager.Instance;
            BuildingDics = BuildMgr.BuildingDics;
            // BuildMgr.setBuilding();
        }

        public void SetBuilding(string keyValue, Building_TYPE _TYPE, int _cost)
        {
            _TYPE = types;
            _cost = cost;
            BuildingDics[keyValue].cost = _cost;
            BuildingDics[keyValue].type = _TYPE;
        }

        public void BuildBuildng(string buildingName)
        {
            BuildingDics[buildingName].Build();
        }


        public void Destroy()
        {
            Debug.Log("파괴됨");
        }










    }

}
