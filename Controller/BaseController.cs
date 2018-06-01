using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController {

    protected ManagerController m_MC;

    public BaseController(ManagerController mc) { m_MC = mc; }

    public virtual void OnStart() { }
    public virtual void OnUpdate() { }
    public virtual void OnDestroy() { }


}
