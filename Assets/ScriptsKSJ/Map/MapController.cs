using sihyeon;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MapController : MonoBehaviour, IPointerDownHandler
{
    [Header("[움직일 애]")]
    public List<Soldier> soldiers = new List<Soldier>();//현재 선택된 움직일 병사들 turntext가 select일때 타일을 누르면
    public int soldierNum;
    //그곳이 자신의 땅에 포함되어 있으면 병사들을 모두 가져옴 (명수 버튼 생기면 함수 수정예정)
    public NodeMember nowTile;//현재 선택한 타일
    List<string> nodeStrings = new List<string>(); //움직일 노드의 순서를 저장한 리스트 (계산한 것)
    MapExtra mapExtra;//계산을 위해 스크립트 저장해놓은것
    Coroutine moveCo;//이동할 때 코루틴 시작하는 거 저장
    Soldier checkSoldier;//코루틴 돌릴때 이동 끝난거 체크하려고 병사하나 넣어 놓은 것

    [Header("[맵 카메라]")]
    public Camera miniMapCam; // 클릭을 위해 있는 렌더러 카메라
    public GameObject prefaba; // 클릭했을때 클릭한 곳에 생성되는 박스(클릭포인트)
    [SerializeField]
    LayerMask layerMask;//타일만 선택할 수 있게 레이어마스크 설정

    
    
    private void Start()
    {
        mapExtra = RoundManager.Instance.mapExtra;
        soldierNum = 0;
    }
    public void CursorCal(PointerEventData eventData)
    {
        Vector2 cursor = new Vector2(0, 0);
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponent<RawImage>().rectTransform,
            eventData.pressPosition, eventData.pressEventCamera, out cursor))
        {
            Texture texture = GetComponent<RawImage>().texture;
            Rect rect = GetComponent<RawImage>().rectTransform.rect;

            float coordX = Mathf.Clamp(0.0f, (((cursor.x - rect.x) * texture.width) / rect.width), texture.width);
            float coordY = Mathf.Clamp(0.0f, (((cursor.y - rect.y) * texture.height) / rect.height), texture.height);
            //Debug.Log(coordX + ", " + coordY);
            //Debug.Log(curosr.x + ", " + curosr.y);
            //Debug.Log(rect.x + ", " + rect.y);
            //Debug.Log(rect.width + ", " + rect.height);//클릭하는 캔버스의 크기
            //Debug.Log(texture.width + ", " + texture.height);//맵 rawimage에 들어가는  rawimage텍스쳐의 크기
            float calX = coordX / texture.width;
            float calY = coordY / texture.height;
            
            cursor = new Vector2(calX, calY);
            //여기까지 클릭되는 곳 계산한 것
            CastRayToWorld(cursor);
            /*
            if (RoundManager.Instance.testType == RoundManager.SoldierTestType.Select)//선택될떄마다
            {
                tileTextObj.gameObject.SetActive(true);
                tileTextObj.position = cursor;
            }*/
            
        }
    }
    private void Update()
    {
        /*
        if(tileTextObj !=null && nowTile == null)
        {
            tileTextObj.gameObject.SetActive(false);
        }*/
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        CursorCal(eventData);
    }

    private void CastRayToWorld(Vector2 vec)//맵만 쏠수있게 바ㅜ꺼야함
    {

        Ray MapRay = miniMapCam.ScreenPointToRay(new Vector2(vec.x * miniMapCam.pixelWidth,
            vec.y * miniMapCam.pixelHeight));
        //캔버스에서 계산한 값을 레이캐스터를 렌더러카메라에서 바닥으로 쏴서 
        //클릭한 곳을 찾는 것
        RaycastHit miniMapHit;
        //minimapHit.point를 하면 클릭한 위치 찾을 수 있음.
        if (Physics.Raycast(MapRay, out miniMapHit, Mathf.Infinity, layerMask))
        {
            Instantiate(prefaba, miniMapHit.point, Quaternion.identity);

            if (RoundManager.Instance.nowPlayer != null)
            {
                SetSoldier(miniMapHit);
            }
        }
    }
    //클릭할 떄 enum변수에 따라서 이벤트가 달라짐.
    void SetSoldier(RaycastHit miniMapHit)
    {
        switch (RoundManager.Instance.testType)//나중에 스위치문 전체를 nodemember있는지 체크하는거로 바꾸고 통일함
        {
            case RoundManager.SoldierTestType.None:
                break;
            case RoundManager.SoldierTestType.Select:
                if (miniMapHit.transform.TryGetComponent(out NodeMember tempMem))//nodemember를 찾음.
                {
                    Debug.Log(tempMem.nodeName);
                    nowTile = tempMem;
                    //선택된 애들을 리스트에 넣어줌.
                }
                else
                {
                    soldiers.Clear();
                }
                break;
            case RoundManager.SoldierTestType.MoveSelect:
                if (miniMapHit.transform.TryGetComponent(out NodeMember tempTile))//nodemember를 찾음.
                {
                    if (RoundManager.Instance.nowPlayer.hasSoldierDic.ContainsKey(tempTile.nodeName) &&
                        RoundManager.Instance.nowPlayer.hasSoldierDic[tempTile.nodeName].Count > 0)
                    {
                        nowTile = tempTile;
                        soldiers = RoundManager.Instance.nowPlayer.hasSoldierDic[tempTile.nodeName];
                        Uimanager.Instance.playerUI.MoveSoldier();
                        //선택된 애들을 리스트에 넣어줌.
                    }
                }
                else
                {
                    soldiers.Clear();
                }
                break;
            case RoundManager.SoldierTestType.Move:
                NodeMember finNode = null;
                if(moveCo != null)
                {
                    StopCoroutine(moveCo);
                }
                RoundManager.Instance.moveOver = true;                
                if (miniMapHit.transform.TryGetComponent(out NodeMember mem))
                {
                    finNode = mem;
                    //Debug.Log("a" + nowTile.nodeName + "/" + finNode.nodeName);
                    nodeStrings = mapExtra.SetAl(nowTile.nodeName, finNode.nodeName);
                    //최단거리 계산하는 부분.
                }
                moveCo = StartCoroutine("MoveCoroutine");

                Uimanager.Instance.playerUI.soldierMove.SetActive(false);
                Uimanager.Instance.playerUI.isOn = true;
                RoundManager.Instance.testType = RoundManager.SoldierTestType.Select;
                break;
            case RoundManager.SoldierTestType.Build:
                if (miniMapHit.transform.TryGetComponent(out NodeMember buildTile))//nodemember를 찾음.
                {                    
                    nowTile = buildTile;
                    RoundManager.Instance.nowPlayer.SpawnBuilding(nowTile.nodeName, nowTile.transform,
                    BuildingManager.Instance.selectedBuilding);
                    RoundManager.Instance.testType = RoundManager.SoldierTestType.Select;
                }                    
                break;
            case RoundManager.SoldierTestType.Revoit:
                if (miniMapHit.transform.TryGetComponent(out NodeMember revoitTile))//nodemember를 찾음.
                {
                    nowTile = revoitTile;
                    BuildingManager.Instance.SetWoodBase(nowTile);
                    RoundManager.Instance.nowPlayer.SpawnBuilding(nowTile.nodeName, nowTile.transform,
                    BuildingManager.Instance.selectedBuilding);
                    RoundManager.Instance.testType = RoundManager.SoldierTestType.Select;
                }
                break;
            case RoundManager.SoldierTestType.CatSet:
                if (miniMapHit.transform.TryGetComponent(out NodeMember settingTile))
                {
                    nowTile = settingTile;
                    if (nowTile == RoundManager.Instance.mapExtra.mapTiles[11])
                    {
                        for (int i = 0; i < mapExtra.mapTiles.Count; i++)
                        {
                            if (i != 0)
                                RoundManager.Instance.cat.SpawnSoldier(mapExtra.mapTiles[i].nodeName, mapExtra.mapTiles[i].transform);
                        }
                        RoundManager.Instance.cat.SpawnBuilding(nowTile.nodeName, nowTile.transform,
                        BuildingManager.Instance.catBasePrefab);
                       
                    }
                    else if (nowTile == RoundManager.Instance.mapExtra.mapTiles[8])
                    {
                        for (int i = 0; i < mapExtra.mapTiles.Count; i++)
                        {
                            if (i != 2)
                                RoundManager.Instance.cat.SpawnSoldier(mapExtra.mapTiles[i].nodeName, mapExtra.mapTiles[i].transform);
                        }
                        RoundManager.Instance.cat.SpawnBuilding(nowTile.nodeName, nowTile.transform,
                             BuildingManager.Instance.catBasePrefab);
                    }
                    else if (nowTile == RoundManager.Instance.mapExtra.mapTiles[2])
                    {
                        for (int i = 0; i < mapExtra.mapTiles.Count; i++)
                        {
                            if (i != 8)
                                RoundManager.Instance.cat.SpawnSoldier(mapExtra.mapTiles[i].nodeName, mapExtra.mapTiles[i].transform);
                        }
                        RoundManager.Instance.cat.SpawnBuilding(nowTile.nodeName, nowTile.transform,
                         BuildingManager.Instance.catBasePrefab);
                    }
                    else if (nowTile == RoundManager.Instance.mapExtra.mapTiles[0])
                    {
                        for (int i = 0; i < mapExtra.mapTiles.Count; i++)
                        {
                            if (i != 11)
                                RoundManager.Instance.cat.SpawnSoldier(mapExtra.mapTiles[i].nodeName, mapExtra.mapTiles[i].transform);
                        }
                        RoundManager.Instance.cat.SpawnBuilding(nowTile.nodeName, nowTile.transform,
                          BuildingManager.Instance.catBasePrefab);
                    }
                    RoundManager.Instance.cat.isDisposable = false;
                    RoundManager.Instance.testType = RoundManager.SoldierTestType.Select;
                }
                break;
            default: break;
        }
    }
    IEnumerator MoveCoroutine()//병사 이동하는 코루틴 
    {
        int count = nodeStrings.Count;
        int num = 1;
        while (count > 0)
        {
            NodeMember foundNode = mapExtra.mapTiles.Find(node => node.nodeName == nodeStrings[num]);
            Vector3 tempPostion = foundNode.transform.position;
                        
            if (RoundManager.Instance.moveOver)
            {
                checkSoldier = soldiers[soldierNum - 1];
                for (int i = 1; i <= soldierNum; i++)
                {                    
                    //Debug.Log(soldiers.Count - 1);
                    Soldier tempSoldier = soldiers[soldiers.Count - 1];
                    //Debug.Log(nodeStrings[num]);
                    tempSoldier.MoveAuto(tempPostion);//움직이게하고  
                    RoundManager.Instance.nowPlayer.SetHasNode(foundNode.nodeName, tempSoldier);//옮겨갈 땅으로 병사정보
                    RoundManager.Instance.nowPlayer.hasSoldierDic[nowTile.nodeName].RemoveAt(soldiers.Count - 1);
                    //원래있던 곳에서 없애줌
                }
                //foreach (Soldier debuging in RoundManager.Instance.nowPlayer.hasSoldierDic[nowTile.nodeName])
                //{
                //    Debug.Log(debuging.name + "현재병사");
                //}
                //foreach (Soldier debuging in RoundManager.Instance.nowPlayer.hasSoldierDic[foundNode.nodeName])
                //{
                //    Debug.Log(debuging.name + "바뀐병사");
                //}

                nowTile = foundNode;
                soldiers = RoundManager.Instance.nowPlayer.hasSoldierDic[foundNode.nodeName];
                //RoundManager.Instance.nowPlayer.hasSoldierDic[nowTile.nodeName] = 
                count--;//다움직일때까지
                num++;//시작점을 제외하고 움직이기
                RoundManager.Instance.moveOver = false;                
            }
            yield return new WaitForSeconds(Time.deltaTime * 20f);
            if (RoundManager.Instance.moveOver == false)
            {                
                if (checkSoldier.agent.remainingDistance < 1f)
                {
                    Debug.Log("들어옴");
                    RoundManager.Instance.moveOver = true;
                }
            }            
            if (num >= nodeStrings.Count)
                break;
            yield return new WaitForSeconds(Time.deltaTime * 10f);
        }
        yield return null;
    }
}
