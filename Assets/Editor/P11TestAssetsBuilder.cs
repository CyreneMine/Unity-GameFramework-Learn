using System;
using System.IO;
using Unity.CodeEditor;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>Agent 创建的测试资源生成工具；不会覆盖已有同名场景或预制体。</summary>
public static class P11TestAssetsBuilder
{
    public const string PrefabPath = "Assets/Resources/P11/PoolCube.prefab";
    public const string SpherePath = "Assets/Resources/P11/PoolSphere.prefab";
    public const string ScenePath = "Assets/Scenes/P11PoolTest.unity";

    [MenuItem("Tools/学习检查/P11/生成测试资源与场景")]
    public static void CreateAssets()
    {
        if (!Application.isBatchMode && !EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            return;

        Directory.CreateDirectory("Assets/Resources/P11");
        AssetDatabase.Refresh();

        CreatePrefab(PrefabPath, PrimitiveType.Cube);
        CreatePrefab(SpherePath, PrimitiveType.Sphere);

        if (!File.Exists(ScenePath))
        {
            Scene scene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
            Camera camera = Camera.main;
            camera.transform.position = new Vector3(0, 1.5f, -5);
            camera.transform.LookAt(Vector3.zero);
            var runner = new GameObject("P11 Pool Validation");
            runner.AddComponent<P11PoolSmokeTest>();
            if (!EditorSceneManager.SaveScene(scene, ScenePath))
                throw new InvalidOperationException("无法保存 P11 测试场景");
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("[P11] 测试资源与场景已准备完成。");
    }

    private static void CreatePrefab(string path, PrimitiveType type)
    {
        if (File.Exists(path)) return;
        GameObject obj = GameObject.CreatePrimitive(type);
        obj.name = Path.GetFileNameWithoutExtension(path);
        try
        {
            if (PrefabUtility.SaveAsPrefabAsset(obj, path) == null)
                throw new InvalidOperationException("无法保存 P11 测试预制体");
        }
        finally
        {
            UnityEngine.Object.DestroyImmediate(obj);
        }
    }

    [MenuItem("Tools/学习检查/P11/重新生成脚本工程文件")]
    public static void SyncSolution()
    {
        CodeEditor.Editor.CurrentCodeEditor.SyncAll();
        Debug.Log("[P11] 已请求 Unity 同步脚本工程文件。");
    }
}
