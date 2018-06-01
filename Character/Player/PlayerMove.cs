using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMove : MonoBehaviour {

    private Rigidbody m_rigidbody;
    private Animator m_animator;
    private float rotatteSpeed = 8;
    private CharacterController m_cc;
    private Vector3 target;
    private Ray ray = new Ray();//保存射线
    private Camera m_camera ;
    private bool ismove;
    private Vector3 dir;//移动方向
    public GameObject clickEffect;  


    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_animator = GetComponent<Animator>();
        m_cc = GetComponent<CharacterController>();
        m_camera = Camera.main;
        dir = Vector3.zero;
    }

    public void FixedUpdate()
    {
        MoveByMouse(3);
    }

    /// <summary>
    /// 按键控制移动
    /// </summary>
    /// <param name="speed"></param>
    /// <returns></returns>
    public bool MoveByButton(float speed)
    {
       
        float h = Input.GetAxis("Horizontal");//水平方向 x轴
        float v = Input.GetAxis("Vertical");//垂直方向 z轴
        //Vector3 nowVelocity = m_rigidbody.velocity;
        target = new Vector3(h,0,v);
        if (Mathf.Abs(h) > 0.05f || Mathf.Abs(v) > 0.05f) {
            //m_rigidbody.velocity = new Vector3(h * speed,nowVelocity.y,v * speed);
            m_animator.SetFloat("MoveSpeed",1);
            //Vector3 dir = new Vector3(h,0,v);
            //transform.LookAt(target);
           //Debug.Log(transform.forward);
            Quaternion newRotation = Quaternion.LookRotation(target, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, rotatteSpeed * Time.fixedDeltaTime);
            
            m_cc.SimpleMove(transform.forward * speed);//没有按照自身坐标系  TODO 
            return true;
        }
        m_animator.SetFloat("MoveSpeed", 0);
        return false;
    }


    /// <summary>
    /// 鼠标右键进行移动控制
    /// </summary>
    /// <param name="speed"></param>
    /// <returns></returns>
    public bool MoveByMouse(float speed) {
        SetDirction();
        //m_cc.Move(target * 5);
        if (dir != Vector3.zero) {
            if (Vector3.Distance(dir, transform.position) > 1f)
            {
                transform.LookAt(dir);
                m_cc.SimpleMove(transform.forward * 5);
                m_animator.SetFloat("MoveSpeed", 1);
               
            }
            else
            {
                m_animator.SetFloat("MoveSpeed", 0);
                ismove = false;
            }
           
        }
        
        //if (ismove)
        //{
        //    //移动中的转向
        //    transform.LookAt(dir);
        //    m_cc.SimpleMove(transform.forward * 5);
        //    m_animator.SetFloat("MoveSpeed", 1);
        //}

        return ismove;
    }


    private void SetDirction() {
        if (Input.GetMouseButtonDown(1) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            ray = m_camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            bool isHit = Physics.Raycast(ray, out hitInfo);
            if (isHit && hitInfo.collider.tag == Tags.Ground)
            {
               
                Instantiate(clickEffect,hitInfo.point +new Vector3(0,0.3f,0),Quaternion.identity);
                dir = hitInfo.point;
                ismove = true;
            }
           
        }
        if (Input.GetMouseButtonUp(1))
        {
            ismove = false;
        }
        if (ismove)
        {
            ray = m_camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitinfo;
            Physics.Raycast(ray, out hitinfo);
            transform.LookAt(hitinfo.point);
            Quaternion rotation = Quaternion.LookRotation(hitinfo.point,Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation,rotation, rotatteSpeed * Time.fixedDeltaTime);
            dir = hitinfo.point;
        }
    }




   

}
