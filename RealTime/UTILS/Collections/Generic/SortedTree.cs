﻿//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace UAOOI.ProcessObserver.RealTime.Utils.Collections.Generic
{
  /// <summary>
  /// Generic sorted tree (tree where nodes are sorted)
  /// </summary>
  /// <remarks>This tree allows to keep only one reference to the instance of the object on the tree</remarks>
  /// <typeparam name="T">the type that is stored on the tree</typeparam>
  public partial class SortedTree<T> : IEnumerator<T>, IEnumerable<T>, ICloneable
  {
    private SortedTreeNodeList m_Roots;

    private void RaiseTreeHasChangedEvent()
    {
      TreeHasChanged?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Exception that can be thrown by SortedTree
    /// </summary>
    [Serializable]
    public class SortedTreeNodeException : Exception
    {
      /// <summary>
      /// Initializes a new instance of the <see cref="SortedTree&lt;T&gt;.SortedTreeNodeException"/> class.
      /// </summary>
      /// <param name="message">The message.</param>
      public SortedTreeNodeException(string message) : base(message) { }
    }

    /// <summary>
    /// Sorted List of nodes from Sorted Tree
    /// </summary>
    public class SortedTreeNodeSortedList : SortedList<int, SortedTreeNode>, ICloneable
    {
      /// <summary>
      /// Initializes a new instance of the <see cref="SortedTree{SortedTreeNodeSortedList}"/> class.
      /// </summary>
      public SortedTreeNodeSortedList()
        : base()
      { }

      /// <summary>
      /// Initializes a new instance of the <see cref="SortedTree{SortedTreeNodeSortedList}"/> class.
      /// </summary>
      /// <param name="list">The list.</param>
      public SortedTreeNodeSortedList(SortedTreeNodeSortedList list)
        : base(list.Count)
      {
        foreach (KeyValuePair<int, SortedTreeNode> kvp in list)
          Add(kvp.Key, kvp.Value);
      }

      /// <summary>
      /// Adds the specified list.
      /// </summary>
      /// <param name="list">The list.</param>
      public void Add(SortedTreeNodeSortedList list)
      {
        foreach (KeyValuePair<int, SortedTreeNode> kvp in list)
          Add(kvp.Key, kvp.Value);
      }

      /// <summary>
      /// Gets the first node from this collection.
      /// </summary>
      /// <returns></returns>
      public SortedTreeNode GetFirstNodeFromThisCollection()
      {
        if (Count == 0)
          return null;
        int key = Keys[0];
        foreach (int key2 in Keys)
          if (key2 < key)
            key = key2;
        return this[key];
      }

      #region ICloneable Members

      /// <summary>
      /// Creates a new object that is a copy of the current instance.
      /// </summary>
      /// <returns>
      /// A new object that is a copy of this instance.
      /// </returns>
      object ICloneable.Clone()
      {
        return new SortedTreeNodeSortedList(this);
      }

      #endregion ICloneable Members
    }

    /// <summary>
    ///  List of nodes from Sorted Tree
    /// </summary>
    public class SortedTreeNodeList : List<SortedTreeNode>, ICloneable
    {
      /// <summary>
      /// Initializes a new instance of the <see cref="SortedTree{SortedTreeNodeList}"/> class.
      /// </summary>
      public SortedTreeNodeList()
        : base()
      { }

      /// <summary>
      /// Initializes a new instance of the <see cref="SortedTree{SortedTreeNodeList}"/> class.
      /// </summary>
      /// <param name="list">The list.</param>
      public SortedTreeNodeList(SortedTreeNodeSortedList list)
        : base(list.Count)
      {
        foreach (KeyValuePair<int, SortedTreeNode> kvp in list)
          Add(kvp.Value);
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="SortedTree{SortedTreeNodeList}"/> class.
      /// </summary>
      /// <param name="list">The list.</param>
      public SortedTreeNodeList(SortedTreeNodeList list)
        : base(list.Count)
      {
        foreach (SortedTreeNode node in list)
          Add(node);
      }

      /// <summary>
      /// Adds the specified list.
      /// </summary>
      /// <param name="list">The list.</param>
      public void Add(SortedTreeNodeSortedList list)
      {
        foreach (KeyValuePair<int, SortedTreeNode> kvp in list)
          Add(kvp.Value);
      }

      /// <summary>
      /// Adds the specified list.
      /// </summary>
      /// <param name="list">The list.</param>
      public void Add(SortedTreeNodeList list)
      {
        foreach (SortedTreeNode node in list)
          Add(node);
      }

      /// <summary>
      /// Gets the first node from this collection.
      /// </summary>
      /// <returns>first sorted tree node of the collection</returns>
      public SortedTreeNode GetFirstNodeFromThisCollection()
      {
        if (Count == 0)
          return null;
        return this[0];
      }

      #region ICloneable Members

      object ICloneable.Clone()
      {
        return new SortedTreeNodeList(this);
      }

      #endregion ICloneable Members
    }

    /// <summary>
    /// Occurs when tree has changed.
    /// </summary>
    public event EventHandler TreeHasChanged;

    /// <summary>
    /// Gets the node.
    /// </summary>
    /// <param name="Value">The value.</param>
    /// <returns>node if found and null otherwise</returns>
    public SortedTreeNode GetNode(T Value)
    {
      SortedTreeNode nodetobereturned = null;
      foreach (SortedTreeNode Node in m_Roots)
      {
        if (Node.Value.Equals(Value))
          nodetobereturned = Node;
        else
          nodetobereturned = Node.GetNode(Value);
        if (nodetobereturned != null)
          return nodetobereturned;
      }
      return null;
    }

    /// <summary>
    /// Gets the nodes from level.
    /// </summary>
    /// <param name="level">The level.</param>
    /// <returns></returns>
    public SortedTreeNodeList GetNodesFromLevel(int level)
    {
      MarkAllUnvisited();
      SortedTreeNodeList nodestobereturned = new SortedTreeNodeList();
      foreach (SortedTreeNode node in m_Roots)
        nodestobereturned.Add(node.GetNodesFromLevel(level));
      return nodestobereturned;
    }

    /// <summary>
    /// Adds the node.
    /// </summary>
    /// <param name="NodeValue">The node value.</param>
    /// <returns></returns>
    public SortedTreeNode AddNode(T NodeValue)
    {
      if (GetNode(NodeValue) != null)
        throw new SortedTreeNodeException("this tree already contains this value");
      SortedTreeNode newnode = new SortedTreeNode(NodeValue, this, null, 0);
      m_Roots.Add(newnode);
      RaiseTreeHasChangedEvent();
      return newnode;
    }

    /// <summary>
    /// Adds the node.
    /// </summary>
    /// <param name="ParentNodeValue">The parent node value. If this value is null this element is added as root element</param>
    /// <param name="NodeNumber">The node number.</param>
    /// <param name="NewValue">The new value.</param>
    /// <param name="ParentConnectorNumber">The parent connector number.</param>
    /// <returns></returns>
    public SortedTreeNode AddNode(T ParentNodeValue, int NodeNumber, T NewValue, int ParentConnectorNumber)
    {
      if (ParentNodeValue == null)
        return AddNode(NewValue);
      SortedTreeNode ParentNode = GetNode(ParentNodeValue);
      if (ParentNode == null)
        throw new SortedTreeNodeException("Cannot find parent node");
      SortedTreeNode currentnode = GetNode(NewValue);
      RaiseTreeHasChangedEvent();
      if (currentnode == null)
        return ParentNode.AddNode(NodeNumber, NewValue, ParentConnectorNumber);
      else
        return ParentNode.AddNode(NodeNumber, currentnode, ParentConnectorNumber);
    }

    /// <summary>
    /// Removes the connection between parent and child and move element to roots.
    /// </summary>
    /// <param name="ParentValue">The parent value.</param>
    /// <param name="ChildValue">The child value.</param>
    public void RemoveConnectionBetweeenParentAndChildAndMoveElementToRoots(T ParentValue, T ChildValue)
    {
      SortedTreeNode ParentNode = GetNode(ParentValue);
      if (ParentNode == null)
        throw new SortedTreeNodeException("Cannot find such parent value:" + ParentValue.ToString());
      SortedTreeNode ChildNode = null;
      foreach (KeyValuePair<int, SortedTreeNode> kvpchildnode in ParentNode.GetChildNodes())
      {
        if (kvpchildnode.Value.Value.Equals(ChildValue))
        {
          ChildNode = kvpchildnode.Value;
          break;
        }
      }
      if (ParentNode == null || ChildNode == null)
        throw new SortedTreeNodeException("Cannot find such connection");
      ParentNode.RemoveNodeFromChildNodes(ChildNode.Value);
      ChildNode.RemoveFromParentNodes(ParentNode.Value);
      SortedTreeNode Node = GetNode(ChildValue);
      if (Node == null)
      {
        //this entity is no longer in the tree - you need to convert it to the root entity
        m_Roots.Add(ChildNode);
        ChildNode.ClearParent();
      }
      ParentCleanup();
    }

    /// <summary>
    /// Removes the value.
    /// </summary>
    /// <param name="ValueToBeRemoved">The value to be removed.</param>
    /// <param name="Shallow">if set to <c>true</c> [shallow] removal is done (all children are moved to root if they are not connected to any others elements).</param>
    public void RemoveValue(T ValueToBeRemoved, bool Shallow)
    {
      SortedTreeNode NodeToBeRemoved = GetNode(ValueToBeRemoved);
      SortedTreeNode node;
      if (NodeToBeRemoved == null)
        throw new SortedTreeNodeException("the value cannot be found on the tree");
      if (Shallow)
      {
        //because it is not allowed to change the collection and make foreach on it:
        SortedTreeNodeSortedList childnodes = NodeToBeRemoved.GetChildNodes();
        node = childnodes.GetFirstNodeFromThisCollection();
        while ((node = childnodes.GetFirstNodeFromThisCollection()) != null)
        {
          RemoveConnectionBetweeenParentAndChildAndMoveElementToRoots(NodeToBeRemoved.Value, node.Value);
          childnodes = NodeToBeRemoved.GetChildNodes();
        }
      }
      //removal of connection above node that is removed
      SortedTreeNodeSortedList parentnodes = NodeToBeRemoved.ParentNodes;
      node = parentnodes.GetFirstNodeFromThisCollection();
      while ((node = parentnodes.GetFirstNodeFromThisCollection()) != null)
      {
        RemoveConnectionBetweeenParentAndChildAndMoveElementToRoots(node.Value, NodeToBeRemoved.Value);
        parentnodes = NodeToBeRemoved.ParentNodes;
      }
      foreach (KeyValuePair<int, SortedTreeNode> kvpnode in parentnodes)
        RemoveConnectionBetweeenParentAndChildAndMoveElementToRoots(kvpnode.Value.Value, NodeToBeRemoved.Value);
      RemoveSortedNodeFromRoots(NodeToBeRemoved);
      ParentCleanup();
    }

    private void RemoveSortedNodeFromRoots(SortedTreeNode nodetoberemoved)
    {
      for (int idx = 0; idx < m_Roots.Count; idx++)
        if (m_Roots[idx].Value.Equals(nodetoberemoved.Value))
        {
          m_Roots.RemoveAt(idx);
          break;
        }
      ParentCleanup();
    }

    private void ParentCleanup()
    {
      // because some functions may lead to the root being removed for some element, but its parents will not be removed from its list so you have to sort the parent list
      foreach (SortedTreeNode node in m_Roots)
        node.ParentCleanup();
      RaiseTreeHasChangedEvent();
    }

    private void SetParentTree(SortedTree<T> tree)
    {
      foreach (SortedTreeNode node in m_Roots)
        node.SetParentTree(tree);
      RaiseTreeHasChangedEvent();
    }

    /// <summary>
    /// Gets the subtree from node.
    /// </summary>
    /// <param name="StartValue">The start value.</param>
    public SortedTree<T> GetSubtreeFromNode(T StartValue)
    {
      SortedTree<T> TreeToBeReturned = (SortedTree<T>)((ICloneable)this).Clone();
      TreeToBeReturned.MoveNodeToRoots(StartValue);
      // no we have to remove other roots
      int idx = 0;
      SortedTreeNodeList nodelist = TreeToBeReturned.GetRoots();
      while (nodelist.Count > 1)
      {
        if (!nodelist[idx].Value.Equals(StartValue))
          TreeToBeReturned.RemoveValue(nodelist[idx].Value, false);
        else
          idx++;
        nodelist = TreeToBeReturned.GetRoots();
      }
      return TreeToBeReturned;
    }

    /// <summary>
    /// Marks all nodes as unvisited.
    /// </summary>
    public void MarkAllUnvisited()
    {
      foreach (SortedTreeNode node in m_Roots)
        node.MarkAllUnvisited();
    }

    /// <summary>
    /// Gets the roots.
    /// </summary>
    /// <returns>Cloned list of roots</returns>
    public SortedTreeNodeList GetRoots()
    {
      return (SortedTreeNodeList)((ICloneable)m_Roots).Clone();
    }

    /// <summary>
    /// Gets the next unvisited node.
    /// </summary>
    /// <returns>next unvisited node</returns>
    public SortedTreeNode GetNextUnvisited()
    {
      SortedTreeNode result = null;
      foreach (SortedTreeNode node in m_Roots)
      {
        result = node.GetNextUnvisited();
        if (result != null) return result;
      }
      return result;
    }

    /// <summary>
    /// Gets the height of the tree.
    /// </summary>
    /// <value>The height.</value>
    public int Height
    {
      get
      {
        int maxheight = 0;
        foreach (SortedTreeNode node in m_Roots)
        {
          int currentheight = node.Height;
          if (currentheight > maxheight)
            maxheight = currentheight;
        }
        return maxheight;
      }
    }

    /// <summary>
    /// Gets the height of the node.
    /// </summary>
    /// <param name="Node">The node.</param>
    /// <returns></returns>
    public int GetHeightOfTheNode(T Node)
    {
      return GetNode(Node).Height;
    }

    /// <summary>
    /// Gets the count.
    /// </summary>
    /// <value>The count.</value>
    public int Count
    {
      get
      {
        int count = 0;
        foreach (T node in this)
          count++;
        return count;
      }
    }

    /// <summary>
    /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
    /// </summary>
    /// <returns>
    /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
    /// </returns>
    public override string ToString()
    {
      StringBuilder result = new StringBuilder(base.ToString() + " elements:");
      result.Append("{");
      foreach (SortedTreeNode node in m_Roots)
        result.Append(node);
      result.Append("}");
      return result.ToString();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SortedTree{T}"/> class.
    /// </summary>
    public SortedTree()
    {
      m_Roots = new SortedTreeNodeList();
    }

    #region IEnumerable Members

    /// <summary>
    /// Returns an enumerator that iterates through a collection.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
      return ((IEnumerable<T>)this).GetEnumerator();
    }

    #endregion IEnumerable Members

    #region IEnumerable<T> Members

    /// <summary>
    /// Returns an enumerator that iterates through the collection.
    /// </summary>
    /// <returns>
    /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
    /// </returns>
    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
      return (IEnumerator<T>)((ICloneable)this).Clone();
    }

    #endregion IEnumerable<T> Members

    #region IEnumerator<T> Members

    private SortedTreeNode IEnumeratorCurrentNode = null;

    /// <summary>
    /// Gets the element in the collection at the current position of the enumerator.
    /// </summary>
    /// <value></value>
    /// <returns>The element in the collection at the current position of the enumerator.</returns>
    T IEnumerator<T>.Current
    {
      get
      {
        if (IEnumeratorCurrentNode == null)
          IEnumeratorCurrentNode = m_Roots[0];
        return IEnumeratorCurrentNode.Value;
      }
    }

    #endregion IEnumerator<T> Members

    #region IDisposable Members

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    void IDisposable.Dispose() { }

    #endregion IDisposable Members

    #region IEnumerator Members

    /// <summary>
    /// Gets the element in the collection at the current position of the enumerator.
    /// </summary>
    /// <value></value>
    /// <returns>The element in the collection at the current position of the enumerator.</returns>
    object IEnumerator.Current => ((IEnumerator<T>)this).Current;

    /// <summary>
    /// Advances the enumerator to the next element of the collection.
    /// </summary>
    /// <returns>
    /// true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
    bool System.Collections.IEnumerator.MoveNext()
    {
      SortedTreeNode nextnode = GetNextUnvisited();
      if (nextnode != null)
      {
        IEnumeratorCurrentNode = nextnode;
        return true;
      }
      return false;
    }

    /// <summary>
    /// Sets the enumerator to its initial position, which is before the first element in the collection.
    /// </summary>
    /// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
    void IEnumerator.Reset()
    {
      IEnumeratorCurrentNode = null;
      MarkAllUnvisited();
    }

    #endregion IEnumerator Members

    #region ICloneable Members

    /// <summary>
    /// Creates a new object that is a copy of the current instance.
    /// </summary>
    /// <returns>
    /// A new object that is a copy of this instance.
    /// </returns>
    object ICloneable.Clone()
    {
      SortedTree<T> newtree = new SortedTree<T>();

      SortedTreeNodeList currentelementlist = null; // lista elementow z aktualnej warstwy
      SortedTreeNodeList nextlevelelementlist = null; // lista elementow z nastepnej warstwy

      // kopiujemy najpierw roots elements
      currentelementlist = new SortedTree<T>.SortedTreeNodeList(m_Roots);
      //a teraz w petli
      while (currentelementlist.Count > 0)
      {
        nextlevelelementlist = new SortedTree<T>.SortedTreeNodeList();
        foreach (SortedTreeNode node in currentelementlist)
        {
          nextlevelelementlist.Add(node.GetChildNodes());
          node.AddThisNodeToAnotherTree(ref newtree);
        }
        currentelementlist = new SortedTree<T>.SortedTreeNodeList(nextlevelelementlist);
      }
      return newtree;
    }

    #endregion ICloneable Members

    /// <summary>
    /// Tests the tree if can be connected.
    /// </summary>
    /// <param name="NodeToWhichWeConnectsTheTreeValue">The node to which we connects the tree value.</param>
    /// <param name="NodeToWhichWeConnectsTheTreeConnectorNumer">The node to which we connects the tree connector numer.</param>
    /// <param name="TreeToBeConnected">The tree to be connected.</param>
    /// <param name="TreeToBeConnectedConnectorNumer">The tree to be connected connector numer.</param>
    /// <returns></returns>
    public bool TestTreeIfCanBeConnected(T NodeToWhichWeConnectsTheTreeValue, int NodeToWhichWeConnectsTheTreeConnectorNumer, SortedTree<T> TreeToBeConnected, int TreeToBeConnectedConnectorNumer)
    {
      SortedTreeNode NodeToWhichWeConnects = GetNode(NodeToWhichWeConnectsTheTreeValue);
      if (NodeToWhichWeConnects == null)
        return false;
      if (NodeToWhichWeConnects.GetChildNodes().ContainsKey(NodeToWhichWeConnectsTheTreeConnectorNumer))
        return false;
      if (TreeToBeConnected.GetRoots().Count > 1)
        return false;
      foreach (T nodevalue in TreeToBeConnected)
        if (GetNode(nodevalue) != null)
          return false;
      return true;
    }

    /// <summary>
    /// Tests the tree if can be connected.
    /// </summary>
    /// <param name="NodeToWhichWeConnectsTheValue">The node to which we connects the value.</param>
    /// <param name="NodeToWhichWeConnectsConnectorNumer">The node to which we connects connector numer.</param>
    /// <param name="NodeToBeConnectedValue">The node to be connected value.</param>
    /// <param name="NodeToBeConnectedValueConnectorNumber">The node to be connected value connector number.</param>
    /// <returns></returns>
    public bool TestNodeIfCanBeConnected(T NodeToWhichWeConnectsTheValue, int NodeToWhichWeConnectsConnectorNumer, T NodeToBeConnectedValue, int NodeToBeConnectedValueConnectorNumber)
    {
      SortedTreeNode NodeToWhichWeConnects = GetNode(NodeToWhichWeConnectsTheValue);
      if (NodeToWhichWeConnects == null)
        return false;
      if (NodeToWhichWeConnects.GetChildNodes().ContainsKey(NodeToWhichWeConnectsConnectorNumer))
        return false;
      SortedTreeNode NodeToBeConnected = GetNode(NodeToBeConnectedValue);
      if (NodeToBeConnected == null)
        return false;
      if (NodeToBeConnected.ParentNodes.ContainsKey(NodeToBeConnectedValueConnectorNumber))
        return false;
      if (NodeToWhichWeConnects.GetNode(NodeToBeConnectedValue) != null)// ponizej tego noda jest juz ta wartosc
        return false;
      if (NodeToBeConnected.GetNode(NodeToWhichWeConnectsTheValue) != null)// ponizej tego noda jest juz ta wartosc
        return false;
      return true;
    }

    /// <summary>
    /// Connects the tree to the node.
    /// </summary>
    /// <param name="NodeToWhichWeConnectsTheTreeValue">The node to which we connects the tree value.</param>
    /// <param name="NodeToWhichWeConnectsTheTreeConnectorNumer">The node to which we connects the tree connector numer.</param>
    /// <param name="TreeToBeConnected">The tree to be connected.</param>
    /// <param name="TreeToBeConnectedConnectorNumer">The tree to be connected connector numer.</param>
    public void ConnectTheTreeToTheNode(T NodeToWhichWeConnectsTheTreeValue, int NodeToWhichWeConnectsTheTreeConnectorNumer, SortedTree<T> TreeToBeConnected, int TreeToBeConnectedConnectorNumer)
    {
      //sprawdzamy czy polaczenie moze nastapic
      if (!TestTreeIfCanBeConnected(NodeToWhichWeConnectsTheTreeValue, NodeToWhichWeConnectsTheTreeConnectorNumer, TreeToBeConnected, TreeToBeConnectedConnectorNumer))
        throw new SortedTreeNodeException("Cannot connect such tree");
      //wyszukujemy node do ktorego chcemy podlaczyc drzewo
      SortedTreeNode NodeToWhichWeConnectsTheTree = GetNode(NodeToWhichWeConnectsTheTreeValue);
      if (NodeToWhichWeConnectsTheTree == null)
        throw new SortedTreeNodeException("Cannot find parent node");
      //klonujemy drzewo ktore dolaczamy aby uniknac ew. zmian w tym drzewie (chcemy miec pewnosc ze ktos zmieniajac oryginal to drzewo (this) pozostanie nie zmienione)
      SortedTree<T> clonnedtree = (SortedTree<T>)((ICloneable)TreeToBeConnected).Clone();
      clonnedtree.SetParentTree(this);
      NodeToWhichWeConnectsTheTree.AddNode(clonnedtree.GetRoots()[0], NodeToWhichWeConnectsTheTreeConnectorNumer, TreeToBeConnectedConnectorNumer);
      ParentCleanup();
    }

    /// <summary>
    /// Connects the node to other node.
    /// </summary>
    /// <param name="NodeToWhichWeConnectsTheTreeValue">The node to which we connects the tree value.</param>
    /// <param name="NodeToWhichWeConnectsConnectorNumer">The node to which we connects connector numer.</param>
    /// <param name="NodeToBeConnectedValue">The node to be connected value.</param>
    /// <param name="NodeToBeConnectedConnectorNumber">The node to be connected connector number.</param>
    public void ConnectTheNodeToOtherNode(T NodeToWhichWeConnectsTheTreeValue, int NodeToWhichWeConnectsConnectorNumer, T NodeToBeConnectedValue, int NodeToBeConnectedConnectorNumber)
    {
      if (!TestNodeIfCanBeConnected(
        NodeToWhichWeConnectsTheTreeValue, NodeToWhichWeConnectsConnectorNumer,
        NodeToBeConnectedValue, NodeToBeConnectedConnectorNumber))
        throw new SortedTreeNodeException("Cannot connect such nodes");

      SortedTreeNode NodeToWhichWeConnectsTheTree = GetNode(NodeToWhichWeConnectsTheTreeValue);
      if (NodeToWhichWeConnectsTheTree == null)
        throw new SortedTreeNodeException("Cannot find parent node");
      SortedTreeNode NodeToBeConnected = GetNode(NodeToBeConnectedValue);
      if (NodeToBeConnected == null)
        throw new SortedTreeNodeException("Cannot find parent node");
      m_Roots.Remove(NodeToBeConnected);
      NodeToWhichWeConnectsTheTree.AddNode(NodeToBeConnected, NodeToWhichWeConnectsConnectorNumer, NodeToBeConnectedConnectorNumber);
      ParentCleanup();
    }

    /// <summary>
    /// Moves the node to roots.
    /// </summary>
    /// <param name="NodeToBeMovedToRootsValue">The node to be moved to roots value.</param>
    public void MoveNodeToRoots(T NodeToBeMovedToRootsValue)
    {
      SortedTreeNode NodeToBeMovedToRoots = GetNode(NodeToBeMovedToRootsValue);
      if (NodeToBeMovedToRoots == null)
        throw new SortedTreeNodeException("This node cannot be found");
      while (NodeToBeMovedToRoots.ParentNodes.Count > 0)
        RemoveConnectionBetweeenParentAndChildAndMoveElementToRoots(NodeToBeMovedToRoots.ParentNodes.GetFirstNodeFromThisCollection().Value, NodeToBeMovedToRootsValue);
      ParentCleanup();
    }
  }
}