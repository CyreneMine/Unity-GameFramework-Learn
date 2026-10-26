using System.Collections.Generic;
using UnityEngine;

public class PoolMgr : BaseManager<PoolMgr>
{
    private PoolMgr(){}
    private Dictionary<string, Stack<GameObject>> poolDic = new Dictionary<string, Stack<GameObject>>();

    public GameObject GetObj(string poolName)
    {
        GameObject obj = new GameObject();
        if (poolDic.ContainsKey(poolName) && poolDic[poolName].Count > 0 )
        {
            obj = poolDic[poolName].Pop();
            obj.gameObject.SetActive(true);
        }
        else
        {
            obj = GameObject.Instantiate(Resources.Load<GameObject>(poolName));
        }
        return obj;
    }

    public void PushObj(GameObject obj)
    {
        obj.gameObject.SetActive(false);
        if (!poolDic.ContainsKey(obj.name))
            poolDic.Add(obj.name,new Stack<GameObject>());
        poolDic[obj.name].Push(obj);
    }

    public void ClearPool()
    {
        poolDic.Clear();
    }
}
