using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEditor;
using Scripts.BehaviourTree.DecoratorNodes;
using Scripts.BehaviourTree.CompositeNodes;
using Unity.VisualScripting;
using Scripts.Characters;

namespace Scripts.BehaviourTree
{
    [CreateAssetMenu()]
    public class BehaviourTree : ScriptableObject
    {
        public Node rootNode;
        public Node.State treeState = Node.State.RUNNING;
        public List<Node> nodes = new List<Node>();
        public Blackboard blackboard = new Blackboard();

        public Node.State Update()
        {
            if (rootNode.state == Node.State.RUNNING) 
            { 
                treeState = rootNode.Update();
            }

            return treeState;
        }

        public Node CreateNode(System.Type type)
        {
            Node node = ScriptableObject.CreateInstance(type) as Node;
            node.name = type.Name;
            node.guid = GUID.Generate().ToString();

            Undo.RecordObject(this, "Behaviour Tree (CreateNode)");
            nodes.Add(node);

            if (!Application.isPlaying)
            {
                AssetDatabase.AddObjectToAsset(node, this);
            }

            Undo.RegisterCreatedObjectUndo(node, "Behaviour Tree (CreateNode)");
            AssetDatabase.SaveAssets();

            return node;
        }

        public void DeleteNode(Node node)
        {
            Undo.RecordObject(this, "Behaviour Tree (CreateNode)");
            nodes.Remove(node);

            //AssetDatabase.RemoveObjectFromAsset(node);
            Undo.DestroyObjectImmediate(node);
            AssetDatabase.SaveAssets();
        }

        public void AddChild(Node parent, Node child)
        {
            DecoratorNode decorator = parent as DecoratorNode;
            if (decorator)
            {
                Undo.RecordObject(decorator, "Behaviour Tree (AddChild)");
                decorator.child = child;
                EditorUtility.SetDirty(decorator);
            }

            RootNode rootNode = parent as RootNode;
            if (rootNode)
            {
                Undo.RecordObject(rootNode, "Behaviour Tree (AddChild)");
                rootNode.child = child;
                EditorUtility.SetDirty(rootNode);
            }

            CompositeNode compsite = parent as CompositeNode;
            if (compsite)
            {
                Undo.RecordObject(compsite, "Behaviour Tree (AddChild)");
                compsite.children.Add(child);
                EditorUtility.SetDirty(compsite);
            }
        }

        public void RemoveChild(Node parent, Node child)
        {
            DecoratorNode decorator = parent as DecoratorNode;
            if (decorator)
            {
                Undo.RecordObject(decorator, "Behaviour Tree (RemoveChild)");
                decorator.child = null;
                EditorUtility.SetDirty(decorator);
            }

            RootNode rootNode = parent as RootNode;
            if (rootNode)
            {
                Undo.RecordObject(rootNode, "Behaviour Tree (RemoveChild)");
                rootNode.child = null;
                EditorUtility.SetDirty(rootNode);
            }

            CompositeNode compsite = parent as CompositeNode;
            if (compsite)
            {
                Undo.RecordObject(compsite, "Behaviour Tree (RemoveChild)");
                compsite.children.Remove(child);
                EditorUtility.SetDirty(compsite);
            }
        }

        public List<Node> GetChildren(Node parent)
        {
            List<Node> children = new List<Node>();

            DecoratorNode decorator = parent as DecoratorNode;
            if (decorator && decorator.child != null)
            {
                children.Add(decorator.child);
            }

            RootNode rootNode = parent as RootNode;
            if (rootNode && rootNode.child != null)
            {
                children.Add(rootNode.child);
            }

            CompositeNode compsite = parent as CompositeNode;
            if (compsite)
            {
                return compsite.children;
            }

            return children;
        }

        public void Traverse(Node node, System.Action<Node> visiter)
        {
            if (node)
            {
                visiter.Invoke(node);
                var chidren = GetChildren(node);    
                chidren.ForEach((n)=>Traverse(n, visiter));
            }
        }

        public BehaviourTree Clone()
        {
            BehaviourTree tree = Instantiate(this);
            tree.rootNode = tree.rootNode.Clone();
            tree.nodes = new List<Node>();
            Traverse(tree.rootNode, (n) =>
            {
                tree.nodes.Add(n);
            });
            return tree;
        }

        public void Bind(Transform transform, Animator animator, CharacterStatus characterStatus)
        {
            Traverse(rootNode, node =>
            {
                node.blackboard = blackboard;
                node.blackboard.transform = transform;
                node.blackboard.animator = animator;
                node.blackboard.characterStatus = characterStatus;
            });
        }
    }
}
