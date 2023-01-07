using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    private Vector3 nowMove = Vector3.zero;
    public float moveSpeed = 3f;
    public float roundSpeed = 2f;
    void Update()
    {
        if (nowMove != Vector3.zero)
        {
            //玩家面朝向移动
            this.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            //不停的朝目标方向转向
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(nowMove), roundSpeed * Time.deltaTime);
        }
    }
    public void Move(Vector2 dir)
    {
        nowMove.x = dir.x;
        nowMove.y = 0;
        nowMove.z = dir.y;
    }
}

