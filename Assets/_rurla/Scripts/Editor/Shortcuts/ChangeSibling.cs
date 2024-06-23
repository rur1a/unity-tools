using UnityEditor;
using UnityEngine;
using static _rurla.Scripts.Editor.Shortcuts.Shortcuts;

namespace _rurla.Scripts.Editor.Shortcuts
{
    public static class ChangeSibling
    {
        [MenuItem(Path + MoveSiblingUp)]
        private static void Up()
        {
            Transform t = Selection.activeTransform;
            t.SetSiblingIndex(t.GetSiblingIndex() - 1);
        }

        [MenuItem(Path + MoveSiblingUp, true)]
        private static bool CanUp()
        {
            return Selection.activeTransform != null && Selection.activeTransform.GetSiblingIndex() != 0;
        }

        [MenuItem(Path + MoveSiblingDown)]
        private static void Down()
        {
            Transform t = Selection.activeTransform;
            t.SetSiblingIndex(t.GetSiblingIndex() + 1);
        }

        [MenuItem(Path + MoveSiblingDown, true)]
        private static bool CanDown()
        {
            return Selection.activeTransform != null;
        }
    }
}