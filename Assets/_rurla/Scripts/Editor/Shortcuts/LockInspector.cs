using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using static _rurla.Scripts.Editor.Shortcuts.Shortcuts;

// Inspired by https://forum.unity.com/threads/shortcut-key-for-lock-inspector.95815/#post-5013983
namespace _rurla.Scripts.Editor.Shortcuts
{
    public static class LockToggle
    {
        [MenuItem(Path + LockFocusedWindow)]
        private static void ToggleLockFocusedWindow()
        {
            ToggleLockEditorWindow(EditorWindow.focusedWindow);
        }

        [MenuItem(Path + LockMouseOverWindow)]
        private static void ToggleLockMouseOverWindow()
        {
            ToggleLockEditorWindow(EditorWindow.mouseOverWindow);
        }

        [MenuItem(Path + LockAllWindows)]
        private static void ToggleLockAllWindows()
        {
            EditorWindow[] allWindows = Resources.FindObjectsOfTypeAll<EditorWindow>();
            foreach (EditorWindow editorWindow in allWindows) ToggleLockEditorWindow(editorWindow);
        }

        private static void ToggleLockEditorWindow(EditorWindow editorWindow)
        {
            var editorAssembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
            Type projectBrowserType = editorAssembly.GetType("UnityEditor.ProjectBrowser");
            Type inspectorWindowType = editorAssembly.GetType("UnityEditor.InspectorWindow");
            Type sceneHierarchyWindowType = editorAssembly.GetType("UnityEditor.SceneHierarchyWindow");

            Type editorWindowType = editorWindow.GetType();
            if (editorWindowType == projectBrowserType)
            {
                // Unity C# reference: https://github.com/Unity-Technologies/UnityCsReference/blob/c6ec7823//Editor/Mono/ProjectBrowser.cs#L113
                PropertyInfo propertyInfo =
                    projectBrowserType.GetProperty("isLocked", BindingFlags.Instance | BindingFlags.NonPublic);

                var value = (bool)propertyInfo.GetValue(editorWindow);
                propertyInfo.SetValue(editorWindow, !value);
            }
            else if (editorWindowType == inspectorWindowType)
            {
                // Unity C# reference: https://github.com/Unity-Technologies/UnityCsReference/blob/c6ec7823//Editor/Mono/Inspector/InspectorWindow.cs##L492
                PropertyInfo propertyInfo = inspectorWindowType.GetProperty("isLocked");

                var value = (bool)propertyInfo.GetValue(editorWindow);
                propertyInfo.SetValue(editorWindow, !value);
            }
            else if (editorWindowType == sceneHierarchyWindowType)
            {
                // Unity C# reference: https://github.com/Unity-Technologies/UnityCsReference/blob/c6ec7823/Editor/Mono/SceneHierarchyWindow.cs#L34
                PropertyInfo sceneHierarchyPropertyInfo = sceneHierarchyWindowType.GetProperty("sceneHierarchy");
                object sceneHierarchy = sceneHierarchyPropertyInfo.GetValue(editorWindow);

                // Unity C# reference: https://github.com/Unity-Technologies/UnityCsReference/blob/c6ec7823/Editor/Mono/SceneHierarchy.cs#L88
                Type sceneHierarchyType = editorAssembly.GetType("UnityEditor.SceneHierarchy");
                PropertyInfo propertyInfo =
                    sceneHierarchyType.GetProperty("isLocked", BindingFlags.Instance | BindingFlags.NonPublic);

                var value = (bool)propertyInfo.GetValue(sceneHierarchy);
                propertyInfo.SetValue(sceneHierarchy, !value);
            }
            else
            {
                return;
            }

            editorWindow.Repaint();
        }
    }
}