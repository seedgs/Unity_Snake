using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GamePanel : MonoBehaviour
{
    
    public EventTrigger et;
    public RectTransform ImgJoy;
    private PlayerObject player;

    void Start()
    {
        player = FindObjectOfType<PlayerObject>();
        //为摇杆注册事件
        //拖动中
        EventTrigger.Entry en = new EventTrigger.Entry();
        en.eventID = EventTriggerType.Drag;
        en.callback.AddListener(OnDrag);
        et.triggers.Add(en);

        //结束拖动
        en = new EventTrigger.Entry();
        en.eventID = EventTriggerType.EndDrag;
        en.callback.AddListener(EndOnDrag);
        et.triggers.Add(en);
    }
    private void OnDrag(BaseEventData data)
    {
        //摇杆移动的坐标
        Vector2 NowPos;
        PointerEventData pointer = data as PointerEventData;
        //以前是通过加上鼠标偏移位置 让图标动起来
        //ImgJoy.position += new Vector3(pointer.delta.x, pointer.delta.y, 0);

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            ImgJoy.parent as RectTransform,
            pointer.position,
            pointer.enterEventCamera,
            out NowPos
            );
        ImgJoy.localPosition = NowPos;

        //我们有专门的参数  得到相对于锚点的点，这里的120是指Joy中心点距离外圆的边缘距离 防止摇杆出去
        if (ImgJoy.anchoredPosition.magnitude > 70)
        {
            //拉回来
            //单位向量 * 长度 = 临界长度
            ImgJoy.anchoredPosition = ImgJoy.anchoredPosition.normalized * 70;
        }
        //让玩家移动
        player.Move(ImgJoy.anchoredPosition);
    }

    private void EndOnDrag(BaseEventData data)
    {
        ImgJoy.localPosition = new Vector2(-224, -371);
        //停止移动 
        player.Move(Vector2.zero);
    }

}