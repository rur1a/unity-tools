using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using static _rurla.Scripts.Editor.Shortcuts.Shortcuts;

namespace _rurla.Scripts.Editor.Shortcuts
{
    public static class DuplicateWithoutSerialNumber
    {
        private static readonly Regex m_regex = new(@"(.*)(\([0-9]*\))");

        [MenuItem(Path + DuplicateWithoutNumber)]
        public static void Duplicate()
        {
            var list = new List<int>();

            foreach (GameObject n in Selection.gameObjects)
            {
                GameObject clone = Object.Instantiate(n, n.transform.parent);
                clone.name = n.name;
                list.Add(clone.GetInstanceID());
                Undo.RegisterCreatedObjectUndo(clone, "Duplicate Without Serial Number");
            }

            Selection.instanceIDs = list.ToArray();
            list.Clear();
        }

        [MenuItem(Path + RemoveDuplicatedNames)]
        public static void Remove()
        {
            GameObject[] list = Selection.gameObjects
                    .Where(c => m_regex.IsMatch(c.name))
                    .ToArray()
                ;

            if (list == null || list.Length == 0) return;

            foreach (GameObject n in list)
            {
                Undo.RecordObject(n, "Remove Duplicated Name");
                n.name = m_regex.Replace(n.name, @"$1");
            }
        }

        [MenuItem(Path + DuplicateWithoutNumber, true)]
        public static bool CanDuplicate()
        {
            GameObject[] gameObjects = Selection.gameObjects;
            return gameObjects != null && 0 < gameObjects.Length;
        }

        [MenuItem(Path + RemoveDuplicatedNames, true)]
        public static bool CanRemove()
        {
            GameObject[] gameObjects = Selection.gameObjects;
            return gameObjects != null && 0 < gameObjects.Length;
        }
    }
}