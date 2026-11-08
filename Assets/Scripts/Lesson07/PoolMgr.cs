using System.Collections.Generic;
using UnityEngine;

public class PoolData
{
    private Stack<GameObject> stack = new Stack<GameObject>();
    private GameObject poolObj;
    
    public int Count => stack.Count;
    public PoolData(GameObject root,string name)
    {
        if (PoolMgr.Instance.isOpenLayout)
        {
            poolObj = new GameObject(name);
            poolObj.transform.SetParent(root.transform);
        }
    }
    public GameObject Pop()
    {
        GameObject obj = stack.Pop();
        obj.gameObject.SetActive(true);
        if (PoolMgr.Instance.isOpenLayout)
            obj.transform.SetParent(null);
        return obj;
    }

    public void Push(GameObject obj)
    {
        if (PoolMgr.Instance.isOpenLayout)
            obj.transform.SetParent(poolObj.transform);
        obj.SetActive(false);
        stack.Push(obj);
    }
}
public class PoolMgr : BaseManager<PoolMgr>
{
    public bool isOpenLayout = true;
    private GameObject poolObj;
    private PoolMgr(){}
    private Dictionary<string, PoolData> poolDic = new Dictionary<string, PoolData>();

    public GameObject GetObj(string poolName)
    {
        GameObject obj;
        if (poolDic.ContainsKey(poolName) && poolDic[poolName].Count > 0 )
        {
            obj = poolDic[poolName].Pop();
        }
        else
        {
            obj = GameObject.Instantiate(Resources.Load<GameObject>(poolName));
            obj.name = poolName;
        }
        return obj;
    }

    public void PushObj(GameObject obj)
    {
        if (poolObj == null&&isOpenLayout)
            poolObj = new GameObject("Pool");
        if (!poolDic.ContainsKey(obj.name))
            poolDic.Add(obj.name,new PoolData(poolObj,obj.name));
        poolDic[obj.name].Push(obj);
    }

    public void ClearPool()
    {
        poolDic.Clear();
        poolObj = null;
    }
}
