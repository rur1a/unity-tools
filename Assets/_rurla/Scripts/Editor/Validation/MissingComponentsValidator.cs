using System.Collections.Generic;
using System.Linq;
using _rurla.Scripts.Editor.Utils;
using NUnit.Framework;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _rurla.Scripts.Editor.Validation
{
    public class MissingComponentsValidator
    {
        [MenuItem("☀rurla/🔍 Validation/⚠️ Find All Missing Objects")]
        public static void FindAllMissingComponents()
        {
            var allProjectScenePaths = SceneUtils.AllProjectScenePaths;
            var gameObjectsWithMissingComponents =
                allProjectScenePaths
                    .SelectMany(FindAllMissingComponents)
                    .ToList();

            foreach (var gameObject in gameObjectsWithMissingComponents)
            {
                Debug.unityLogger.LogWarning("⚠️", $"GameObject {gameObject.name} from {gameObject.scene.path} has missing scripts", gameObject);
            }
        }

        public static List<GameObject> FindAllMissingComponents(string scenePath)
        {
            var openScene = EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Additive);

            var gameObjectsWithMissingComponents =
                SceneUtils.GetAllSceneGameObjects(openScene)
                    .Where(sceneGameObject =>
                        GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(sceneGameObject) > 0)
                    .ToList();
            
            EditorSceneManager.CloseScene(openScene, true);
            return gameObjectsWithMissingComponents;
        }
    }
}