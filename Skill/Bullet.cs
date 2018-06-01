using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 子弹类
/// </summary>
public class Bullet : MonoBehaviour
{
    public float m_Speed = 30;
    public float m_MaxDistance = 50;
    public Vector3 m_Offset;
    private float distance = 0;

    public int m_Count;//一次生成几颗子弹
    public float m_Range = 0;


    private void Awake()
    {

    }

    private void FixedUpdate()
    {

        //if (AttrController.Instance.m_IsDie) return;
        //m_Rigidbody.MovePosition(transform.position + transform.forward * Time.deltaTime * m_Speed);
        //transform.Translate(transform.forward * Time.deltaTime * m_Speed);
        transform.position += transform.forward * m_Speed * Time.deltaTime;
        //if (distance > m_MaxDistance) { distance = 0; gameObject.SetActive(false); } else { distance += Time.deltaTime * m_Speed; print(distance); }
        if (gameObject.activeInHierarchy) { StartCoroutine("AutoHide"); }

    }

    public void OnTriggerEnter(Collider c)
    {
        //先判断这个是否是范围伤害
        if (m_Range > 0) return;
        //检测子弹是否击中敌人
        if (c.tag == Tags.Enemy)
        {

            //c.gameObject.GetComponent<Enemy>().DoDamage(new ArrayList { 100, 0, 0, 0, 0 });
            gameObject.SetActive(false);
            MyEvent evt = new MyEvent(MyEvent.MyEventType.SkillTrigger);
            evt.m_GOPara = c.gameObject;
            MyEventSystem.m_MyEventSystem.PushEvent(evt);
        }
    }

    IEnumerator AutoHide()
    {
        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);
        yield return null;
    }


    private void Update()
    {
        if (m_Range > 0)
        {
            CheckEnemy();
        }
    }
     
    void CheckEnemy()
    {
        Collider[] cs =  Physics.OverlapSphere(transform.position,m_Range);
        if (cs.Length == 0) return;
        foreach (Collider c in cs)
        {
            if (c.tag == Tags.Enemy)
            {
                //c.gameObject.GetComponent<Enemy>().DoDamage(new ArrayList());
                print("检测到敌人");
            }
        }
    }

    






}
