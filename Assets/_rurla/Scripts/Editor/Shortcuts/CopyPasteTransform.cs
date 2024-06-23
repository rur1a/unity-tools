using UnityEditor;
using UnityEngine;
using static _rurla.Scripts.Editor.Shortcuts.Shortcuts;

namespace _rurla.Scripts.Editor.Shortcuts
{
    public static class CopyPasteTransform
    {
        private static Data m_data;

        [MenuItem(Path + CopyTransform)]
        public static void Copy()
        {
            m_data = new Data(Selection.activeTransform);
        }

        [MenuItem(Path + PasteTransform)]
        public static void Paste()
        {
            foreach (GameObject n in Selection.gameObjects)
            {
                Transform t = n.transform;
                Undo.RecordObject(t, "Paste Transform Values");
                t.localPosition = m_data.m_localPosition;
                t.localRotation = m_data.m_localRotation;
                t.localScale = m_data.m_localScale;
            }
        }

        [MenuItem(Path + CopyTransform, true)]
        public static bool CanCopy()
        {
            return Selection.activeTransform != null;
        }

        [MenuItem(Path + PasteTransform,true)]
        public static bool CanPaste()
        {
            GameObject[] gameObjects = Selection.gameObjects;
            return m_data != null && gameObjects != null && 0 < gameObjects.Length;
        }

        private class Data
        {
            public readonly Vector3 m_localPosition;
            public readonly Quaternion m_localRotation;
            public readonly Vector3 m_localScale;
            
            public Data(Vector3 localPosition, Quaternion localRotation, Vector3 localScale)
            {
                m_localPosition = localPosition;
                m_localRotation = localRotation;
                m_localScale = localScale;
            }

            public Data(Transform t) : this(t.localPosition, t.localRotation, t.localScale)
            {
            }
        }
    }
}