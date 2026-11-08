using System.Collections.Generic;
using UnityEngine;

public class PoolData
{
    private Stack<GameObject> stack = new Stack<GameObject>();
    private GameObject poolObj;
    public List<GameObject> usedList = new List<GameObject>();
    private int maxNum;
    public bool needCreate => usedList.Count < maxNum;
    public int Count => stack.Count;
    public PoolData(GameObject root,string name,GameObject obj)
    {
        PoolObj poolObjScript = obj.GetComponent<PoolObj>();
        if (poolObjScript == null)
        {
            Debug.LogError("请为要使用缓存池的预设体挂载PoolObj脚本");
            return;
        }
        if (PoolMgr.Instance.isOpenLayout)
        {
            poolObj = new GameObject(name);
            poolObj.transform.SetParent(root.transform);
        }
        PushUsedList(obj);
        maxNum = poolObjScript.maxNum;
    }
    public GameObject Pop()
    {
        GameObject obj;
        if (Count > 0)
        {
            obj = stack.Pop();
            usedList.Add(obj);
        }
        else
        {
            obj = usedList[0];
            usedList.RemoveAt(0);
            usedList.Add(obj);
        }
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
        usedList.Remove(obj);
        stack.Push(obj);
    }
    public void PushUsedList(GameObject obj)
    {
        usedList.Add(obj);
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
        if (poolObj == null&&isOpenLayout)
            poolObj = new GameObject("Pool");
        GameObject obj;
        #region 有数量限制写法

        if (!poolDic.ContainsKey(poolName)||
            poolDic[poolName].Count == 0 && poolDic[poolName].needCreate)
        {
            obj = GameObject.Instantiate(Resources.Load<GameObject>(poolName));
            obj.name = poolName;
            if (!poolDic.ContainsKey(poolName))
                poolDic.Add(poolName, new PoolData(poolObj,poolName,obj));
            else
                poolDic[poolName].usedList.Add(obj);
        }else
        {
            obj = poolDic[poolName].Pop();
        }
        #endregion
        /*#region 无数量限制写法
         
        if (poolDic.ContainsKey(poolName) && poolDic[poolName].Count > 0 )
        {
            obj = poolDic[poolName].Pop();
        }
        else
        {
            obj = GameObject.Instantiate(Resources.Load<GameObject>(poolName));
            obj.name = poolName;
        }

        #endregion*/
        return obj;
    }

    public void PushObj(GameObject obj)
    {
        
        poolDic[obj.name].Push(obj);
    }

    public void ClearPool()
    {
        poolDic.Clear();
        poolObj = null;
    }
}
