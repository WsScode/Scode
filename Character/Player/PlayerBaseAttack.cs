using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;



/// <summary>
/// 人物普攻逻辑
/// </summary>
public class PlayerBaseAttack : MonoBehaviour {

    private Enemy m_enemy = null;
    private string animName;
    private float attackDistance;
    private float m_distance;
    

    private void Awake()
    {
        
    }

    /// <summary>
    /// 根据主武器类型决定攻击的动画 距离 攻击类型（物理或者魔法）
    /// </summary>
    /// <param name="mainWeaponType"></param>
    /// <param name="attack"></param>
    public void AttackEnemy(Weapon.MainHandWeaponType mainWeaponType,int attack) {
        if (mainWeaponType == Weapon.MainHandWeaponType.None) 
        switch (mainWeaponType)
        {
            case Weapon.MainHandWeaponType.None:
                return;//TODO 没有主武器应该提示获取或者装备
            case Weapon.MainHandWeaponType.RangedMagicweapon:
                break;
            case Weapon.MainHandWeaponType.Shortrangedweapon:
                    
                break;
            case Weapon.MainHandWeaponType.RangePhysicalWeapon:
                    
                break;
            default:
                break;
        }

    }

    /// <summary>
    /// 得到要攻击的目标
    /// </summary>
    public void GetAttackDir() {
        if (Input.GetMouseButtonDown(1) && EventSystem.current.IsPointerOverGameObject() == false) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitinfo;
            bool ishit = Physics.Raycast(ray,out hitinfo);
            if (ishit && hitinfo.collider.tag == "Enemy") {
                m_enemy = hitinfo.collider.GetComponent<Enemy>();
                m_distance = Vector3.Distance(hitinfo.point, transform.position);
            }

        }

    }




}
