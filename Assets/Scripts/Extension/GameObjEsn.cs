using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public static class GameObjEsn
{
    public static GameObject SetTrue(this GameObject obj)
    {
        obj.SetActive(true);
        return obj;
    }
    public static GameObject SetFalse(this GameObject obj)
    {
        obj.SetActive(false);
        GameObject dd;
        
        return obj;
    }

    public static List<T> GetAllChildren<T>(this GameObject obj,bool Hide=true) where T:Component
    {

        List<T> ObjLst=new List<T>();
        foreach (var item in obj.GetComponentsInChildren<T>(Hide))
        {
            if(obj!=item.gameObject)
            ObjLst.Add(item);
        }

        return ObjLst;
    }

    public static T SetParent<T>(this T obj,Transform parent) where T:Component
    {
        obj.transform.parent = parent;

        return obj;
    }



}

