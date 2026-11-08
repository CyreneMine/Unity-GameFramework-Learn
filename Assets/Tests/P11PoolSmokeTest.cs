using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class P11PoolSmokeTest : MonoBehaviour
{
    void Update()
    {
        var mouse = Mouse.current;
        if (mouse == null) return;
        if (mouse.leftButton.wasPressedThisFrame) StartCoroutine(Spawn("P11/PoolCube"));
        if (mouse.rightButton.wasPressedThisFrame) StartCoroutine(Spawn("P11/PoolSphere"));
    }

    IEnumerator Spawn(string key)
    {
        var obj = PoolMgr.Instance.GetObj(key);
        obj.transform.position = Random.insideUnitSphere * 2;
        yield return new WaitForSeconds(2);
        PoolMgr.Instance.PushObj(obj);
    }
}
