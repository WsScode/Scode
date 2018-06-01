using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FollowTarget : MonoBehaviour {

    private Camera m_mainCamera;
    private Transform player;
    private Vector3 offset;//偏移距离
    private bool isRotate;//是否是旋转状态
    private float rotateSpeed = 2;
    private float zoomSpeed = 5;
    private float smooth = 0.5f;




    private void Awake()
    {
        m_mainCamera = Camera.main;
        player = GameObject.FindGameObjectWithTag(Tags.Player).transform;
        offset = transform.position - player.position;
        //print("name： " + player.name + ":" +  player.position);
        transform.LookAt(player);
    }


    void LateUpdate () {
        //Vector3.SmoothDamp(,);
        transform.position = player.position + offset;
        RotateView();
        ZoomView();
    }

    /// <summary>
    /// 控制视野旋转 
    /// </summary>
    void RotateView() {
        float mx = Input.GetAxis("Mouse X");//得到鼠标平面X轴的移动距离
        float my = Input.GetAxis("Mouse Y");//得到鼠标平面Y轴的移动距离

        if (Input.GetMouseButton(0) && EventSystem.current.IsPointerOverGameObject() == false) {
            isRotate = true;
        }
        if (Input.GetMouseButtonUp(0)) {
            isRotate = false;
        }
        if (isRotate) {
            Vector3 nowPos = transform.position;
            Quaternion nowRot = transform.rotation;
            transform.RotateAround(player.position,player.up,mx * rotateSpeed);
            transform.RotateAround(player.position,transform.right,-my * rotateSpeed);
            float x = transform.eulerAngles.x;//X轴的旋转值
            if (x < 7 || x > 75) {
                transform.position = nowPos;
                transform.rotation = nowRot;
            }
            if (transform.eulerAngles.z != 0) {
                transform.eulerAngles= new Vector3(transform.eulerAngles.x, transform.eulerAngles.y,0);
            }
        }
        offset = transform.position - player.position;//更新偏移距离！！！
    }

    /// <summary>
    /// 视野缩放
    /// </summary>
    void ZoomView() {
        float distance =  offset.magnitude;//得到偏移向量长度  也就是相机到玩家的距离 
        distance  -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        Mathf.Clamp(distance, 1, 3);//限制距离大小
        if (distance < 8) distance = 8;
        if (distance > 30) distance = 30;
        offset = offset.normalized * distance;
        
    }

}
