using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class CountEsn
{

    /// <summary>
    /// PCT
    /// </summary>
    /// <param name="value"></param>
    /// <param name="max"></param>
    /// <param name="min"></param>
    /// <returns></returns>
    public static float ClampData(this float value, float max, float min)
    {

        if (min < 0)
        {
            float result = Mathf.Clamp(value, min, max)
             + Mathf.Abs(min);
            result /= (Mathf.Abs(min) + max);
            return result;
        }
        else
        {
            float result = Mathf.Clamp(value, min, max)
             - Mathf.Abs(min);
            result /= (max - min);

            return result;

        }
    }

    public static List<T> GetRandoms<T>(this List<T> list, int Count)
    {
       

        List<T> tempList = new List<T>();
        List<T> ReturnList = new List<T>();

        for (int i = 0; i < list.Count; i++)
        {
            tempList.Add(list[i]);
        }

        for (int i = 0; i < Count; i++)
        {
            int temp = UnityEngine.Random.Range(0, tempList.Count);


            ReturnList.Add(tempList[temp]);
            tempList.Remove(tempList[temp]);


        }

        return ReturnList;

    }

    public static T GetRandom<T>(this List<T> list)
    {


     

        return list[UnityEngine.Random.Range(0,list.Count)];

    }

    public static void eew(this GameObject game)
    {
        game = new GameObject();
    }
}

