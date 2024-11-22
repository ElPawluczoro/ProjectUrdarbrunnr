using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace Editor
{
    public class InspectorView : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<InspectorView, VisualElement.UxmlTraits> { }

        UnityEditor.Editor editor;

        public InspectorView() { }

        internal void UpdateSelection(NodeView nodeView)
        {
            Clear();

            UnityEngine.Object.DestroyImmediate(editor);
            editor = UnityEditor.Editor.CreateEditor(nodeView.node);
            IMGUIContainer container = new IMGUIContainer(() => 
            {
                if (editor.target != null)
                {
                    editor.OnInspectorGUI();
                }
            });
            Add(container);
        }
    }
}
