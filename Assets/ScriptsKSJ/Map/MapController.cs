using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MapController : MonoBehaviour, IPointerDownHandler
{
    [Header("[������ ��]")]
    public List<Soldier> soldiers = new List<Soldier>();
    public NodeMember nowTile;
    List<string> nodeStrings = new List<string>();
    MapExtra mapExtra;
    Coroutine moveCo;

    [Header("[�� ī�޶�]")]
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
        Vector2 curosr = new Vector2(0, 0);
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponent<RawImage>().rectTransform,
            eventData.pressPosition, eventData.pressEventCamera, out curosr))
        {
            Texture texture = GetComponent<RawImage>().texture;
            Rect rect = GetComponent<RawImage>().rectTransform.rect;

            float coordX = Mathf.Clamp(0.0f, (((curosr.x - rect.x) * texture.width) / rect.width), texture.width);
            float coordY = Mathf.Clamp(0.0f, (((curosr.y - rect.y) * texture.height) / rect.height), texture.height);
            //Debug.Log(coordX + ", " + coordY);
            //Debug.Log(curosr.x + ", " + curosr.y);
            //Debug.Log(rect.x + ", " + rect.y);
            //Debug.Log(rect.width + ", " + rect.height);//Ŭ���ϴ� ĵ������ ũ��
            //Debug.Log(texture.width + ", " + texture.height);//�� rawimage�� ����  rawimage�ؽ����� ũ��
            float calX = coordX / texture.width;
            float calY = coordY / texture.height;

            curosr = new Vector2(calX, calY);

            CastRayToWorld(curosr);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        CursorCal(eventData);
    }

    private void CastRayToWorld(Vector2 vec)//�ʸ� ����ְ� �٤̲�����
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
            //Debug.Log(num);
            Vector3 tempPostion = mapExtra.mapTiles.Find(node => node.nodeName == nodeStrings[num]).transform.position;

            Soldier checkSoldier = soldiers[0];

            if (RoundManager.Instance.moveOver)
            {
                foreach (Soldier tempSoldier in soldiers)
                {
                    tempSoldier.MoveAuto(tempPostion);
                }
                count--;
                num++;
                RoundManager.Instance.moveOver = false;
            }
            if (checkSoldier.agent.remainingDistance < 0.5f)
            {
                //Debug.Log("dda");
                RoundManager.Instance.moveOver = true;
            }
            if (num >= nodeStrings.Count)
                break;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        yield return null;
    }
}