using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Test2  {
    public int Age { get; set; }
    private string name;
    public string Name { get { return name; } set { name = value; } }
    public GameObject go { get; set; }
    public List<string> M_List { get; set; }
    public List<int> m_ItemList { get; set; }
    public List<ArrayList> a_list { get; set; }
    public Dictionary<int, int> dic { get; set; }
    [NonSerialized]public string _Name;
    public List<int[]> mylist { get; set; }

    //protected override void Awake()
    //{
    //    print("子类Awake");
    //}

    //private void Start()
    //{
    //    //Test._instance.ChangeHandlerEvent += HpChange;
    //    Test._instance.RegisterEvent(TestEnum.HP, HpChange);
    //}

    ///// <summary>
    ///// 这个方法就被通知 玩家血量改变  
    ///// 在里面执行血量改变的逻辑
    ///// </summary>
    ///// <param name="type"></param>
    //void HpChange(TestEnum type) {
    //    if (type == TestEnum.HP) {
    //        Debug.Log("HP Changed1");
    //    }

    //}

    //protected override void Update()
    //{
    //    //base.Update();
    //    TestFunc();
    //}

    //protected override void TestFunc()
    //{
    //    print("这是子类的testfunc");
    //}


   







}
