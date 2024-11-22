using Editor;
using Scripts.BehaviourTree;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Callbacks;
using System;

public class BehaviourTreeEditor : EditorWindow
{
    BehaviourTreeView treeView;
    InspectorView inspectorView;
    //TODO
    //IMGUIContainer blackboardView;

    //SerializedObject treeObject;
    //SerializedProperty blackboardProperty;
    //

    [MenuItem("BehaviourTreeEditor/Editor")]
    public static void OpenWindow()
    {
        BehaviourTreeEditor wnd = GetWindow<BehaviourTreeEditor>();
        wnd.titleContent = new GUIContent("BehaviourTreeEditor");
    }

    [OnOpenAsset]
    public static bool OnOpenAsset(int instanceId, int line)
    {
        if(Selection.activeObject is BehaviourTree)
        {
            OpenWindow();
            return true;
        }
        return false;
    }

    public void CreateGUI()
    {
        VisualElement root = rootVisualElement;

        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/BehaviourTreeEditor.uxml");
        visualTree.CloneTree(root);

        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/BehaviourTreeEditor.uss");
        root.styleSheets.Add(styleSheet);

        treeView = root.Q<BehaviourTreeView>();
        inspectorView = root.Q<InspectorView>();
        //TODO
        //blackboardView = root.Q<IMGUIContainer>();
        //blackboardView.onGUIHandler = () =>
        //{
        //    treeObject.Update();
        //    EditorGUILayout.PropertyField(blackboardProperty);
        //    treeObject.ApplyModifiedProperties();
        //};
        //
        treeView.OnNodeSelected = OnNodeSelectionChanged;

        OnSelectionChange();
    }

    private void OnSelectionChange()
    {
        BehaviourTree tree = Selection.activeObject as BehaviourTree;
        if (!tree)
        {
            if (Selection.activeGameObject)
            {
                BehaviourTreeRunner runner = Selection.activeGameObject.GetComponent<BehaviourTreeRunner>();
                if (runner)
                {
                    tree = runner.tree;
                }
            }
        }
        if (Application.isPlaying)
        {
            if (tree && treeView != null)
            {
                treeView.PopulateView(tree);
            }
        }
        else
        {
            if (tree && AssetDatabase.CanOpenAssetInEditor(tree.GetInstanceID()))
            {
                treeView.PopulateView(tree);
            }
        }

        //TODO
        //if(tree != null)
        //{
        //    treeObject = new SerializedObject(tree);
        //    blackboardProperty = treeObject.FindProperty("blackboard");
        //}
        //
    }

    private void OnEnable()
    {
        EditorApplication.playModeStateChanged -= OnPLayModeStateChanged;
        EditorApplication.playModeStateChanged += OnPLayModeStateChanged;
    }

    private void OnDisable()
    {
        EditorApplication.playModeStateChanged -= OnPLayModeStateChanged;
    }

    private void OnPLayModeStateChanged(PlayModeStateChange change)
    {
        switch (change)
        {
            case PlayModeStateChange.EnteredEditMode:
                OnSelectionChange();
                break;
            case PlayModeStateChange.ExitingEditMode:
                break;
            case PlayModeStateChange.EnteredPlayMode:
                OnSelectionChange();
                break;
            case PlayModeStateChange.ExitingPlayMode:
                break;
        }
    }

    void OnNodeSelectionChanged(NodeView node)
    {
        inspectorView.UpdateSelection(node);
    }

    private void OnInspectorUpdate()
    {
        treeView?.UpdateNodeState();
    }
}
