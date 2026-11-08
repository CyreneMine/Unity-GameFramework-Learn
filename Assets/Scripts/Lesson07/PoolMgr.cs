using System.Collections.Generic;
using UnityEngine;

public class PoolData
{
    private Stack<GameObject> stack = new Stack<GameObject>();
    private GameObject poolObj;
    public List<GameObject> usedList = new List<GameObject>();
    public int Count => stack.Count;
    public PoolData(GameObject root,string name,GameObject obj)
    {
        if (PoolMgr.Instance.isOpenLayout)
        {
            poolObj = new GameObject(name);
            poolObj.transform.SetParent(root.transform);
        }
        PushUsedList(obj);
    }
    public GameObject Pop()
    {
        GameObject obj;
        if (Count > 0)//疑问？ 为什么这里还要判断Count>0 在外面GetObj的时候不是已经if里面写了非0的情况了 此刻的count一定不等于0
        {
            //疑问？ 如果进入的时候usedList已经满了此时在Add不就报错了吗
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
        stack.Push(obj);
        usedList.Remove(obj);
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

    public GameObject GetObj(string poolName,int maxNum=50)
    {
        if (poolObj == null&&isOpenLayout)
            poolObj = new GameObject("Pool");
        GameObject obj;
        #region 有数量限制写法

        if (!poolDic.ContainsKey(poolName)||
            poolDic[poolName].Count == 0 && poolDic[poolName].usedList.Count < maxNum)
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
