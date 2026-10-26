using System;
using System.Reflection;
using UnityEngine;

public class BaseManager<T> where T:class
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                // instance =new T();
                //利用反射解决安全隐患
                Type type = typeof(T);
                ConstructorInfo info = type.GetConstructor(
                    BindingFlags.Instance | BindingFlags.NonPublic,
                    null,
                    Type.EmptyTypes,
                    null);
                if (info != null)
                    instance = info.Invoke(null) as T;
                else
                    Debug.Log("没有对应的无参构造函数");
            }
            return instance;
        }
    }
}