using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

[CreateAssetMenu(menuName = "ConvertScene/SetTarget")]

public class Scene_WTG_ASSET : ScriptableObject
{
    [System.Serializable]
    public struct FromToScene
    {
        public string currentScene;
        public string targetScene;
        public FromToScene(string _currentScene, string _targetScene)
        {
            currentScene = _currentScene;
            targetScene = _targetScene;
        }
    }

    [SerializeField] public FromToScene fts = new FromToScene();
}
