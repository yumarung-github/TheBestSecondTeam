using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MapController : MonoBehaviour,IPointerDownHandler
{
    [Header("[움직일 애]")]
    public List<Soldier> soldiers = new List<Soldier>();
    public NodeMember nowTile;
    [Header("[맵 카메라]")]
    public Camera miniMapCam;
    public GameObject prefaba;    

    [SerializeField]
    LayerMask layerMask;
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
            //Debug.Log(rect.width + ", " + rect.height);//클릭하는 캔버스의 크기
            //Debug.Log(texture.width + ", " + texture.height);//맵 rawimage에 들어가는  rawimage텍스쳐의 크기
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

    private void CastRayToWorld(Vector2 vec)//맵만 쏠수있게 바ㅜ꺼야함
    {

        Ray MapRay = miniMapCam.ScreenPointToRay(new Vector2(vec.x * miniMapCam.pixelWidth,
            vec.y * miniMapCam.pixelHeight));

        RaycastHit miniMapHit;

        if (Physics.Raycast(MapRay, out miniMapHit, Mathf.Infinity, layerMask))
        {
            Instantiate(prefaba, miniMapHit.point, Quaternion.identity);
            //Debug.Log(miniMapHit.transform.name);

            if (RoundManager.Instance.nowPlayer != null)
            {
                if (RoundManager.Instance.isMove)
                {
                    if(!RoundManager.Instance.isSelected)//이동할 타일 선택할 때
                    {
                        if (miniMapHit.transform.TryGetComponent(out NodeMember tempMem))
                        {
                            Debug.Log(tempMem.nodeName);
                            soldiers = RoundManager.Instance.nowPlayer.hasSoldierDic[tempMem.nodeName];

                            foreach (Soldier tempSoldier in soldiers)
                            {
                                tempSoldier.MoveAuto(miniMapHit.transform.position);
                            }
                        }
                        else
                        {
                            //soldiers.Clear();
                        }
                    }
                    else//선택중일때
                    {

                    }              
                }
                else
                {
                    if (miniMapHit.transform.TryGetComponent(out NodeMember tempMem2))
                    {
                        Debug.Log(tempMem2.nodeName);
                    }
                }
                              
            }
        }
    }
}
