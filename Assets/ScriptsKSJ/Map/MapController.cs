using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MapController : MonoBehaviour, IPointerDownHandler
{
    [Header("[움직일 애]")]
    public List<Soldier> soldiers = new List<Soldier>();
    public NodeMember nowTile;
    List<string> nodeStrings = new List<string>();
    MapExtra mapExtra;
    Coroutine moveCo;
    public RectTransform tileTextObj;
    Soldier checkSoldier;

    [Header("[맵 카메라]")]
    public Camera miniMapCam;
    public GameObject prefaba;

    [SerializeField]
    LayerMask layerMask;
    private void Start()
    {
        mapExtra = RoundManager.Instance.mapExtra;
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
            CastRayToWorld(cursor);
            if (RoundManager.Instance.testType == RoundManager.SoldierTestType.Select)
            {
                tileTextObj.gameObject.SetActive(true);
                tileTextObj.position = cursor;
            }
            
        }
    }
    private void Update()
    {
        if(tileTextObj !=null && nowTile == null)
        {
            tileTextObj.gameObject.SetActive(false);
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        CursorCal(eventData);
    }

    private void CastRayToWorld(Vector2 vec)//맵만 쏠수있게 바ㅜ꺼야함
    {

        Ray MapRay = miniMapCam.ScreenPointToRay(new Vector2(vec.x * miniMapCam.pixelWidth,
            vec.y * miniMapCam.pixelHeight));

        RaycastHit miniMapHit;

        if (Physics.Raycast(MapRay, out miniMapHit, Mathf.Infinity, layerMask))
        {
            Instantiate(prefaba, miniMapHit.point, Quaternion.identity);

            if (RoundManager.Instance.nowPlayer != null)
            {
                SetSoldier(miniMapHit);
            }
        }
    }
    void SetSoldier(RaycastHit miniMapHit)
    {
        switch (RoundManager.Instance.testType)
        {
            case RoundManager.SoldierTestType.None:

                break;
            case RoundManager.SoldierTestType.Select:
                if (miniMapHit.transform.TryGetComponent(out NodeMember tempMem))
                {
                    Debug.Log(tempMem.nodeName);
                    nowTile = tempMem;
                    soldiers = RoundManager.Instance.nowPlayer.hasSoldierDic[tempMem.nodeName];
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
                    Debug.Log("a" + nowTile.nodeName + "/" + finNode.nodeName);
                    nodeStrings = mapExtra.SetAl(nowTile.nodeName, finNode.nodeName);
                    
                    // mapExtra.asAlgo.FindAs(mapExtra.graph, nowTile.nodeName, finNode.nodeName);
                }
                //mapExtra.mapTiles.Find(node => node.nodeName == tempName).transform);
                moveCo = StartCoroutine("MoveCoroutine");
                break;
            default: break;
        }
    }
    IEnumerator MoveCoroutine()
    {
        int count = nodeStrings.Count;
        int num = 1;
        while (count > 0)
        {
            Vector3 tempPostion = mapExtra.mapTiles.Find(node => node.nodeName == nodeStrings[num]).transform.position;

            checkSoldier = soldiers[0];

            if (RoundManager.Instance.moveOver)
            {
                foreach (Soldier tempSoldier in soldiers)
                {
                    Debug.Log(nodeStrings[num]);
                    tempSoldier.MoveAuto(tempPostion);
                    nowTile.nodeName = nodeStrings[num];
                }
                count--;
                num++;
                RoundManager.Instance.moveOver = false;
                yield return new WaitForSeconds(Time.deltaTime);
            }
            if(checkSoldier != null)
            {
                if (checkSoldier.agent.remainingDistance < 1.5f)
                {
                    //Debug.Log("dda");
                    RoundManager.Instance.moveOver = true;
                }
            }            
            if (num >= nodeStrings.Count)
                break;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        yield return null;
    }
}
