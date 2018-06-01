using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Test3  {
    public int Age { get; set; }
    public string Name { get; set; }

    public const int ip = 555555;

    //public Test t;
    //private void Start()
    //{
    //    //Test._instance.ChangeHandlerEvent += changePlayerHP;
    //    //Test._instance.RegisterEvent(TestEnum.HP, changePlayerHP);
    //    t.s = "ceshi";
    //    Test2 t2 = new Test2();

    //}

    //void changePlayerHP(TestEnum type) {
    //    Debug.Log("HP Changed2");
    //}
    //private void Update()
    //{
    //    //if (Input.GetMouseButtonDown(0)) {
    //    //    Debug.Log("HP Changed2");
    //    //}
    //}
    
    public IEnumerator TestIE() {
        while (true) {
            Debug.Log("1");
            yield return new WaitForSeconds(1);
        }
    }

}
