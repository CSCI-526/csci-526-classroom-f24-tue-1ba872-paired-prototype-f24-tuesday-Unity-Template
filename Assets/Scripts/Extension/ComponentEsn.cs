using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

public static  class ComponentEsn
{
    public static T SetActiveF<T>( this  T rectTrans) where T : Component
    {
        
            rectTrans.gameObject.SetActive(false);

        return rectTrans;

    }

    public static T SetActiveT<T>(this T rectTrans) where T : Component
    {
        rectTrans.gameObject.SetActive(true);
        return rectTrans;
    }
}

