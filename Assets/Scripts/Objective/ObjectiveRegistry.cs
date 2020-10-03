using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectiveRegistry : MonoBehaviour
{
    public const int InitialObjectiveCount = 99999;
    public static int ObjectiveCount = InitialObjectiveCount;
    public static int RespawnCount = 0;
    public static float RemainingTime;
    public static float MaxTime = 60;
    public int initialTime = 30;
    public string nextScene;

    private void Start()
    {
        RemainingTime = initialTime;

        foreach (var listScene in MySceneManager.ListScenes())
        {
            Debug.Log(listScene);
        }
        Debug.Log(SceneManager.GetActiveScene().name);
        for (var i = 0; i < SceneManager.sceneCount; i++)
        {
            Debug.Log(SceneManager.GetSceneAt(i).name);
        }
        }

    private void Update()
    {
        foreach (var allLoadedAssetBundle in AssetBundle.GetAllLoadedAssetBundles())
        {
            Debug.Log("+++"+allLoadedAssetBundle.name);
            var allScenePaths = allLoadedAssetBundle.GetAllScenePaths();
            Debug.Log(allScenePaths);    
        }
        Debug.Log(ObjectiveCount);
        if (ObjectiveCount == 0)
        {
            RespawnCount = 0;
            ObjectiveCount = InitialObjectiveCount;
            Debug.Log("Loading Scene " + nextScene);
            SceneManager.LoadScene("Assets/Scenes/"+nextScene+".unity");
        }

        RemainingTime -= Time.deltaTime;
        if (RemainingTime < 0)
        {
            RestartScene();
        }
    }

    public static void RestartScene()
    {
        RespawnCount++;
        ObjectiveCount = InitialObjectiveCount;
        MySceneManager.ReloadScene();
    }

    public static void AddTime(float delta)
    {
        RemainingTime += delta;
        if (RemainingTime > MaxTime)
        {
            RemainingTime = MaxTime;
        }
    }

    public static void AddObjective()
    {
        if (ObjectiveCount == InitialObjectiveCount)
        {
            ObjectiveCount = 1;
        }
        else
        {
            ObjectiveCount++;
        }
    }

    public static void CompleteObjective()
    {
        ObjectiveCount--;
    }
}