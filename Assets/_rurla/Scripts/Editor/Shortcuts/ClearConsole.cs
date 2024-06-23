using System;
using System.Reflection;
using UnityEditor;
using static _rurla.Scripts.Editor.Shortcuts.Shortcuts;

namespace _rurla.Scripts.Editor.Shortcuts
{
    public static class ClearConsole
    {
        [MenuItem(Path + Shortcuts.ClearConsole)]
        public static void Invert()
        {
            Type type = Assembly
                .GetAssembly(typeof(SceneView))
                .GetType("UnityEditor.LogEntries");
            BindingFlags attr = BindingFlags.Static | BindingFlags.Public;
            MethodInfo method = type.GetMethod("Clear", attr);
            method.Invoke(null, null);
        }
    }
}