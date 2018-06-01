using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using LitJson;
using UnityEngine.Networking;


public enum TestEnum {
MP,HP,SKILL,ITEM
}

[Serializable]public class TestP { public string sdasda = ""; public int iii = 2; }

public class Test : MonoBehaviour {

    public TestP[] tp;
    public bool isss;

    [Serializable] public class TestChild
    {
        public bool see = false;
        public int age = 0;
        public string _name = "";

    }



    public delegate void ChangeHandler(TestEnum type, object o);
    public event ChangeHandler ChangeHandlerEvent;


    public static Test _instance;
    private Dictionary<TestEnum, List<ChangeHandler>> lister = new Dictionary<TestEnum, List<ChangeHandler>>();
    IEnumerator testIE;
    [HideInInspector] public string s = "";
    private Animator anim;

    [HideInInspector] public Test4 t4;

    float t = 0;
    private bool testBool = true;

    private test5 t5;

    public int HP { get; private set; }

    public void SetProp(int hpValue)
    {
        HP = hpValue;
    }

    private Animation ani;



    protected virtual void Awake()
    {



        _instance = this;
        //anim = GetComponent<Animator>();
        //ani = GetComponent<Animation>();
        //print(GetComponent<PlayerInfo>());
        //StartCoroutine("TestIE");
        //print(Application.dataPath);
        //FileStream fs = new FileStream("test.txt", FileMode.OpenOrCreate);
        //fs.Close();
        //Test2 t2 = new Test2();
        //t2.Age = 23;
        //t2.Name = "王舜";
        //Test2 t2 = new Test2();
        //t4 = new Test4(); 
        //t4.Test();
        //List<int> newlist = new List<int>() {1231,213,2132,899,646 };
        //List<string> newlist2 = new List<string>() {"asds","asdas","hfghfg","jksdj","uretbnj"};
        //Dictionary<int, string> newdic = new Dictionary<int, string>();
        //int temp = 0;
        //foreach (int i in newlist)
        //{

        //    newdic.Add(i, newlist2[temp]); temp++;
        //}
        //Test2 t2 = new Test2() { dic = new Dictionary<int, int>()
        //{
        //    {1,1},{2,2 },{ 3,3}
        //}
        //};
        //ArrayList AA1 = new ArrayList { "aaaaaa", 111111 };
        //ArrayList AA2 = new ArrayList { 222222, "bbbbbb" };
        //List<int> int1 = new List<int> {1,2,3,2,1 };
        //List<int> int2 = new List<int> {5,4,3,2,1 };
        //int[] i1 = {1,15,4,15,51,515,1 };
        //int[] i2 = { 54,5645,46,454,646,4};
        //Test2 t2 = new Test2() { a_list = new List<ArrayList>() { AA1, AA2 } , mylist = new List<int[]>() { i1,i2} };
        //SaveHelper.SaveByJson(t2,"ttt");

        //foreach (KeyValuePair<int,string> ky in newdic)
        //{
        //    print(ky.Key + "||" + ky.Value);
        //}
        //t5 = GetComponent<test5>();
        //t5.SetStr("改变");


        //StartCoroutine("ie1");




    }

    //private IEnumerator ie1()
    //{
    //    print("ie1开始");
    //    yield return new WaitForSeconds(2);
    //    StartCoroutine("ie2");
    //    print("ie1结束");
    //}
    //private IEnumerator ie2()
    //{
    //    print("ie2开始");
    //    yield return new WaitForSeconds(3);
    //    print("ie2结束");
    //}


    ////private void Start()
    ////{
    ////    StreamWriter sw = File.CreateText(Application.dataPath + "/" + "test.json");
    ////    Test2 t2 = new Test2 { Age = 23, Name = "王舜", };
    ////    Test3 t3 = new Test3 { Age = 23, Name = "王舜", };

    ////    string s = JsonMapper.ToJson(t3);
    ////    sw.WriteLine(s);
    ////    //File.WriteAllText(Application.dataPath + "/" + "test.json","新的内容");

    ////    sw.Close();
    ////}
    ////protected virtual void Update()
    ////{


    ////    //if (Input.GetKeyDown(KeyCode.Space)) {
    ////    //    string j = File.ReadAllText("test.json");
    ////    //    print(j);
    ////    //    JsonData jd = JsonMapper.ToObject(j);
    ////    //    print(jd["Name"]);
    ////    //}
    ////    ////print("2");
    ////    ////t += Time.deltaTime * 5;
    ////    ////print(Vector3.Lerp(new Vector3(0, 0, 0), new Vector3(20, 10, 10), t));

    ////    //if (Input.GetMouseButtonDown(0))
    ////    //{
    ////    //    PlayerInfo.Instance.M_Mp += 555;
    ////    //    PlayerInfo.Instance.PushEvent(MyEventType.MP);
    ////    //}
    ////    //if (Input.GetMouseButtonDown(1))
    ////    //{
    ////    //    PlayerInfo.Instance.M_Exp = 555;
    ////    //    MyEventSystem.m_MyEventSystem.PushEvent(MyEventType.EXP);
    ////    //}
    ////    ////TestFunc();
    ////}

    ////public void RegisterEvent(TestEnum testEnum, ChangeHandler callback) {
    ////    List<ChangeHandler> l = null;
    ////    if (lister.TryGetValue(testEnum, out l))
    ////    {
    ////        l.Add(callback);
    ////    }
    ////    else
    ////    {
    ////        l = new List<ChangeHandler>();
    ////        l.Add(callback);
    ////        lister.Add(testEnum, l);
    ////    }
    ////}

    ////public void PushEvent(TestEnum testenum) {
    ////    List<ChangeHandler> handler = null;
    ////    Debug.Log("000000000000000000000000");
    ////    if (lister.TryGetValue(testenum, out handler))
    ////    {
    ////        Debug.Log("11111111111111111111111111111111");
    ////        Debug.Log(handler);
    ////        //var arr = handler.ToArray();
    ////        for (int i = 0; i < handler.Count; i++) {
    ////            //ChangeHandlerEvent(testenum);
    ////            handler[i](testenum);//触发事件  所有注册的的都会被调用
    ////        }
    ////    }
    ////}


    //IEnumerator TestIE()
    //{

    //    //print(1);
    //    //yield return new WaitForSeconds(1);
    //    //print(2);
    //    //yield return new WaitForSeconds(4);
    //    //print(3);
    //    //yield return new WaitForSeconds(10);
    //    //print("结束");
    //    //while (testBool) {
    //    //    print("1");
    //    //    yield return null;
    //    //}
    //    return null;

    //}
    //protected virtual void TestFunc() {
    //    print("父类中的testfunc");
    //}


    //private void OnDestroy()
    //{
    //    //print("Destroy");
    //}


    //private Rigidbody _rigidbody;
    //private AttrController attr;
    //public void Start()
    //{
    //    //Test2 t2 = new Test2();
    //    ////t2.go = GameObject.CreatePrimitive(PrimitiveType.Cube);
    //    //t2.Name = "xxxxxxxxxx";
    //    //t2.Age = 18;
    //    //t2.M_List = new List<string>() { "asdas", "323424" };
    //    //t2.m_ItemList = new List<int>();
    //    //foreach (Grid g in Inventory.Instance.GetCurrentGridList())
    //    //{
    //    //    if (g.grid_item == null) continue;
    //    //    t2.m_ItemList.Add(g.grid_item.m_ID);
    //    //}
    //    ////t2.m_Dic = new Dictionary<int, string>();//字典不行
    //    ////t2.m_Dic.Add(1,"a");
    //    //ArrayList al = new ArrayList() { "s44444", 65645 };
    //    //t2.a_list = new List<ArrayList>() { al};


    //    //string jd = JsonMapper.ToJson(t2);
    //    ////print(jd);
    //    //string s = "";
    //    //JsonData toObject = JsonMapper.ToObject(jd);
    //    ////print("ToObject:" + toObject["M_List"][0] + toObject["Name"] + toObject["m_ItemList"][5] == null?null: toObject["m_ItemList"][5]);
    //    //StartCoroutine("LoadABTest");
    //    //m_camera = Camera.main;

    //    //_rigidbody = GetComponent<Rigidbody>();
    //    //attr = GetComponent<AttrController>();
    //    //BulletPool.Instance.InitArrowPool();//初始化物品池
    //}




    //private IEnumerator IE()
    //{
    //    while (true)
    //    {
    //        if (i == 2) { print("IE结束"); break; }
    //        yield return null;
    //    }

    //}

    //private int i = 0;
    //private Camera m_camera;
    //private void Update()
    //{

    //    //if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1") )
    //    //{
    //    //    AnimationEvent evt = new AnimationEvent();
    //    //    evt.time = 0.9f;
    //    //    evt.functionName = "Testxx";
    //    //    anim.GetCurrentAnimatorClipInfo(0)[0].clip.AddEvent(evt);
    //    //}

    //    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        //i ++;
    //        ////Save save = new Save();
    //        ////SaveHelper.SaveByJson(save,"GameSave.txt");
    //        //ETCJoystick joy = ETCInput.GetControlJoystick("MyJoystick");
    //        //print(joy);
    //        //m_camera.GetComponent<AudioSource>().volume = 0.5f;
    //        //PushEvent(TestEnum.SKILL);
    //        ChangeHandlerEvent(TestEnum.SKILL,this);


    //    }
    //    //    if (Mathf.Abs(attr.myJoystick.axisX.axisValue) > 0.1f || Mathf.Abs(attr.myJoystick.axisX.axisValue) > 0.1f)
    //    //    {
    //    //        _rigidbody.MovePosition(transform.position + new Vector3(1,0,1) * new Vector3(attr.myJoystick.axisX.axisValue,0, attr.myJoystick.axisX.axisValue).magnitude);
    //    //    }
    //    //RaycastHit hitinfo;
    //    //if (Physics.BoxCast(transform.position, new Vector3(0.00000001f, 0.00000001f, 0.00000001f), transform.forward, out hitinfo))
    //    //{
    //    //    if (hitinfo.collider.tag == Tags.Enemy) { print("shit"); }

    //    //}


    //    //Collider[] cols = Physics.OverlapSphere(transform.position, 1f);
    //    //if (cols.Length > 0)
    //    //{
    //    //    foreach (Collider c in cols)
    //    //    {
    //    //        if (c.tag == "Enemy")
    //    //        {
    //    //            Debug.Log("Enemy");
    //    //        }
    //    //    }
    //    //}

    //    //RaycastHit hitinfo;
    //    //if (Physics.Raycast(transform.position, transform.forward, out hitinfo,1))
    //    //{
    //    //    if (hitinfo.collider.tag == Tags.Enemy) { print("fuck"); }

    //    //}

    //    //TestTriger(GetComponent<BoxCollider>());
    //}


    //public void Testxx() {
    //    print("xxxxxxxxxxxxxxxxxx");

    //    GameObject go = BulletPool.Instance.GetArrow();
    //    go.transform.position = attr.transform.position + new Vector3(0, 1f, 0);
    //    go.SetActive(true);
    //}
    ////private void TestCollider(Collider coll) { print(coll.gameObject.name); }
    //private void TestTriger(Collider coll) { print(coll.gameObject.name); }


    //private void OnParticleTrigger()
    //{
    //    print("fuck you");
    //}

    //private void OnParticleCollision(GameObject other)
    //{
    //    if (other.gameObject.tag == Tags.Enemy)
    //    {
    //        print("fuck you");
    //    }
    //}


    private AssetBundle ab;
    IEnumerator LoadABTest()
    {
        string path = "AB/sound.unity3d";
        //第一种加载方式LoadFromMemoryAsync 从内存里面加载
        //AssetBundleCreateRequest request = AssetBundle.LoadFromMemoryAsync(File.ReadAllBytes(path));
        //yield return request;
        //AssetBundle ab = request.assetBundle;

        //第二种 LoadFromFile
        //AssetBundleCreateRequest request = AssetBundle.LoadFromFileAsync(path);
        //yield return request;
        //AssetBundle ab = request.assetBundle;

        //第三种 WWW
        //while (Caching.ready == false)
        //{
        //    yield return null;
        //}
        //WWW www = WWW.LoadFromCacheOrDownload(@"file:///C:\\Users\Administrator\Desktop\Scode\AB\111222.unity3d", 1);
        //if (!string.IsNullOrEmpty(www.error))
        //{
        //    Debug.Log(www.error);
        //    yield break;
        //}
        //AssetBundle ab = www.assetBundle;


        ////第四种 UnityWebRequest
        //string url = @"http://127.0.0.1/test/111222.unity3d";
        //UnityWebRequest request = UnityWebRequest.GetAssetBundle(url);
        //yield return request.SendWebRequest();//等待传输结束
        ////AssetBundle ab = DownloadHandlerAssetBundle.GetContent(request);//获取AB包方式1
        // ab = (request.downloadHandler as DownloadHandlerAssetBundle).assetBundle;//获取AB包方式2


        ////利用ManiFest文件加载所有的依赖包
        //AssetBundle abManifest = AssetBundle.LoadFromFile("AB/AB");
        //AssetBundleManifest manifest = abManifest.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        ////取得指定包所依赖的所有包的名字 并加载
        //string[] strs = manifest.GetAllDependencies("111222.unity3d");
        //print(manifest);
        //foreach (string abname in strs)
        //{
        //    print(abname);
        //    AssetBundle.LoadFromFile("AB/" + abname);
        //}

        AssetBundleCreateRequest request = AssetBundle.LoadFromFileAsync(path);
        yield return request;
        AssetBundle ab = request.assetBundle;


        AudioClip ac = ab.LoadAsset<AudioClip>("arrow");
        if (ac != null) { print("arrow"); }
        ////使用资源
        //GameObject g = ab.LoadAsset<GameObject>("Cube");
        //GameObject.Instantiate(g, new Vector3(14.53f, -91.27188f, 15.26f), Quaternion.identity);

        yield return null;
    }








    private SkillMachine m_SkillMachine;
    private MyEvent evt;


    private void Start()
    {

        GetComponent<Animator>().SetTrigger("Skill2");
        SkillInfo skill = GetComponent<SkillsInfo>().GetSkillByID(2);
        m_SkillMachine = SkillLogic.CreateSkillMechine(skill, transform.position + new Vector3(0, 0.5f, 0.5f), this.gameObject);
        evt = new MyEvent(MyEvent.MyEventType.SkillTrigger);


        //DateTime d = DateTime.Now;

        //DateTime dt = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        //TimeSpan nowtime = d - dt;
        //string nowTimeStamp = Convert.ToInt64(nowtime.TotalSeconds).ToString();

        //long time = long.Parse(nowTimeStamp);


        //TimeSpan ts = new TimeSpan(time);
        //Debug.Log(dt.AddSeconds(time));

        //int year = DateTime.Now.Year;
        //int month = DateTime.Now.Month;
        //DateTime d1 = new DateTime(DateTime.Now.Year, DateTime.Now.Month - 4, 1);
        //Debug.Log(d1.AddHours(23).AddMinutes(60));//当月第一天
        //Debug.Log(d1.AddMonths(1).AddDays(-1));//当月最后一天
        //SetTimeTable();
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) { MyEventSystem.m_MyEventSystem.PushEvent(evt); }
    }




    //private int[] flagDays = new int[] { 1,3,8,15,26};
    ////private List<int> nowMonth = new List<int>();
    //private Dictionary<int, Text> nowMonth = new Dictionary<int, Text>();

    //void SetTimeTable()
    //{
    //    Text[] texts = GetComponentsInChildren<Text>();
    //    DateTime nowTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
    //    DateTime PrevMonth = nowTime.AddMonths(-1);//上一个月
    //    //获取上月最后一天的 计算最近的星期天
    //    int lastday = (int) PrevMonth.AddMonths(1).AddDays(-1).DayOfWeek;
        
    //    DateTime lastSunday = PrevMonth.AddMonths(1).AddDays(-(lastday+1));
    //    for (int i = 0; i < 42; i++)
    //    {
    //        texts[i + 7].text = lastSunday.AddDays(i).Day + "";
    //        if (lastSunday.AddDays(i).Month == nowTime.Month) { nowMonth.Add(lastSunday.AddDays(i).Day, texts[i + 7]); }
    //    }
    //    foreach (KeyValuePair<int,Text> i in nowMonth)
    //    {
    //        foreach(int j in flagDays)
    //        {
    //            if (i.Key == j)
    //            {
    //                i.Value.text = "已签到";
    //            }
    //        }
    //    } 
    //}

    /// <summary>
    /// 获取时间戳
    /// </summary>
    long GetTimeStamp(DateTime time)
    {
        DateTime dt = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970,1,1));
        TimeSpan nowTime = time - dt;
        string nowTimeStamp = Convert.ToInt64(nowTime.TotalSeconds).ToString();
        return long.Parse(nowTimeStamp);
    }

    DateTime GetDatetimeByStamp(long timeStamp)
    {
        DateTime dt = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970,1,1));
        DateTime newTime = dt.AddSeconds(timeStamp);
        return newTime;
    }


}

