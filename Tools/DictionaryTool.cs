using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DictionaryTool  {

    public static TValue TryGetTool<Tkey, TValue>(this Dictionary<Tkey, TValue> dic,Tkey key){
        TValue value;
        dic.TryGetValue(key, out value);
        return value;
    }
}
