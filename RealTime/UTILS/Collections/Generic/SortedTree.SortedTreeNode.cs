﻿//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using System.Collections.Generic;
using System.Text;

namespace UAOOI.ProcessObserver.RealTime.Utils.Collections.Generic
{
  public partial class SortedTree<T> : IEnumerator<T>, IEnumerable<T>, ICloneable
  {
    /// <summary>
    /// The node that is stored on the tree
    /// </summary>
    public class SortedTreeNode
    {
      #region private fields

      private SortedTree<T> MyParentTree = null;
      private SortedTreeNodeSortedList mChildNodes = new SortedTreeNodeSortedList();
      // <remarks>
      // Gets the parent connector number.
      // (kazdy element umieszczony na drzewie ma swoje connectory - tj punkty przylaczenia:
      // punkty przylaczenia sa numerowane jako integery, parent connector jest punktem przylaczenia dziecka
      // (przez jaki jest on podlaczony do rodzica)
      //
      // <![CDATA[
      // -----------
      // | parent  |
      // | node    |
      // | (list   |
      // | of child|
      // | nodes   |
      // -----------
      //  |  | <- connector number
      //     |
      //     |
      //  |  | <- parent connector number
      //  ----------
      //  | child  |
      //  | node   |
      //  |        |
      //  ----------
      //
      //
      // ]]>
      //
      //
      //
      // </remarks>

      #endregion private fields

      /// <summary>
      /// Gets a value indicating whether this <see cref="SortedTree&lt;T&gt;.SortedTreeNode"/> is visited.
      /// </summary>
      /// <value><c>true</c> if visited; otherwise, <c>false</c>.</value>
      internal bool Visited { get; private set; } = false;

      internal SortedTreeNode(T val)
      {
        Value = val;
      }

      internal T GetValue()
      {
        return Value;
      }

      /// <summary>
      /// gets the height of this instance.
      /// </summary>
      /// <returns>the height</returns>
      internal int Height
      {
        get
        {
          int height = 1;
          int maxchildheight = 0;
          foreach (KeyValuePair<int, SortedTreeNode> KVPNode in mChildNodes)
          {
            int current_height = 0;
            if (KVPNode.Value != null)
              current_height = KVPNode.Value.Height;
            if (current_height > maxchildheight)
              maxchildheight = current_height;
          }
          height += maxchildheight;
          return height;
        }
      }

      /// <summary>
      /// Determines whether the node contains the specified node(connector) number.
      /// </summary>
      /// <param name="NodeNumber">The node number.</param>
      /// <returns>
      /// 	<c>true</c> if contains the specified node number otherwise, <c>false</c>.
      /// </returns>
      internal bool ContainsNodeNumber(int NodeNumber)
      {
        return mChildNodes.ContainsKey(NodeNumber);
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="SortedTree&lt;T&gt;.SortedTreeNode"/> class.
      /// </summary>
      /// <param name="ParentElementValue">The parent element value.</param>
      /// <param name="ParentTree">The parent tree.</param>
      /// <param name="ParentNode">The parent node.</param>
      /// <param name="ParentConnectorNumber">The parent connector number.</param>
      internal SortedTreeNode(T ParentElementValue, SortedTree<T> ParentTree, SortedTreeNode ParentNode, int ParentConnectorNumber)
      {
        MyParentTree = ParentTree;
        Value = ParentElementValue;
        if (ParentNode != null)
          ParentNodes.Add(ParentConnectorNumber, ParentNode);
      }

      /// <summary>
      /// Gets the value stored in this node.
      /// </summary>
      /// <value>The value.</value>
      public T Value { get; private set; }

      /// <summary>
      /// Gets the parent connector number for the selected node.
      /// </summary>
      /// <param name="ParentNodeValue">The parent node value.</param>
      /// <returns></returns>
      public int GetParentConnectorNumber(T ParentNodeValue)
      {
        foreach (int i in ParentNodes.Keys)
          if (ParentNodes[i].Value.Equals(ParentNodeValue))
            return i;
        return -1;
      }

      /// <summary>
      /// Gets the parent node.
      /// </summary>
      /// <value>The parent node.</value>
      internal SortedTreeNodeSortedList ParentNodes { get; } = new SortedTreeNodeSortedList();

      /// <summary>
      /// Gets the parent node by parent node number.
      /// </summary>
      /// <param name="ParentNodeNumber">The parent node number.</param>
      /// <returns>sorted tree node from parent list</returns>
      public SortedTreeNode GetParentNodeByParentNodeNumber(int ParentNodeNumber)
      {
        foreach (KeyValuePair<int, SortedTreeNode> kvpnode in ParentNodes)
          if (kvpnode.Key == ParentNodeNumber)
            return kvpnode.Value;
        return null;
      }

      /// <summary>
      /// Gets the first parent node.
      /// </summary>
      /// <returns>first sorted tree node from parent list</returns>
      public SortedTreeNode GetFirstParentNode()
      {
        return ParentNodes.GetFirstNodeFromThisCollection();
      }

      /// <summary>
      /// Gets the parent nodes count.
      /// </summary>
      /// <value>The parent nodes count.</value>
      public int ParentNodesCount => ParentNodes.Count;

      internal void AddConnectionToParent(SortedTreeNode ParentNode, int ParentConnectorNumber)
      {
        if (ParentNodes.ContainsKey(ParentConnectorNumber))
        {
          if (!ParentNode.Value.Equals(ParentNodes[ParentConnectorNumber].Value))
            throw new SortedTreeNodeException("This node already has such parent connector");
          return;
        }
        ParentNodes.Add(ParentConnectorNumber, ParentNode);
      }

      internal SortedTreeNode AddNode(int NodeNumber, SortedTreeNode NewNode, int ParentConnectorNumber)
      {
        if (ContainsNodeNumber(NodeNumber))
        {
          if (mChildNodes[NodeNumber].Value.Equals(NewNode.Value))
            return NewNode;
          throw new SortedTreeNodeException("Parent node contains this key already");
        }
        mChildNodes.Add(NodeNumber, NewNode);
        NewNode.AddConnectionToParent(this, ParentConnectorNumber);
        return NewNode;
      }

      /// <summary>
      /// Adds the node.
      /// </summary>
      /// <param name="NodeNumber">The node number.</param>
      /// <param name="NewValue">The new value.</param>
      /// <param name="ParentConnectorNumber">The parent connector number.</param>
      /// <returns></returns>
      internal SortedTreeNode AddNode(int NodeNumber, T NewValue, int ParentConnectorNumber)
      {
        return AddNode(NodeNumber, new SortedTreeNode(NewValue, MyParentTree, this, ParentConnectorNumber), ParentConnectorNumber);
      }

      /// <summary>
      /// Adds the node.
      /// </summary>
      /// <param name="NodeToBeAdded">The node to be added.</param>
      /// <param name="MyConnectorNumber">My connector number.</param>
      /// <param name="ParentConnectorNumber">The parent connector number.</param>
      /// <returns></returns>
      internal SortedTreeNode AddNode(SortedTreeNode NodeToBeAdded, int MyConnectorNumber, int ParentConnectorNumber)
      {
        if (mChildNodes.ContainsKey(MyConnectorNumber))
          throw new SortedTreeNodeException("This node has such connector number already");
        mChildNodes.Add(MyConnectorNumber, NodeToBeAdded);
        NodeToBeAdded.AddConnectionToParent(this, ParentConnectorNumber);
        return NodeToBeAdded;
      }

      /// <summary>
      /// Gets the node.
      /// </summary>
      /// <param name="Value">The value.</param>
      /// <returns></returns>
      public SortedTreeNode GetNode(T Value)
      {
        SortedTreeNode nodetobereturned = null;
        foreach (KeyValuePair<int, SortedTreeNode> KVPNode in mChildNodes)
        {
          if (KVPNode.Value.Value.Equals(Value))
            nodetobereturned = KVPNode.Value;
          else
            nodetobereturned = KVPNode.Value.GetNode(Value);
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
        SortedTreeNodeList nodestobereturned = new SortedTreeNodeList();
        if (!Visited && level > 0)
        {
          Visited = true;
          if (Height == level)
            nodestobereturned.Add(this);
          else
          {
            //zakladamy ze pewien element nie moze miec siebie samego jako dziecka
            foreach (KeyValuePair<int, SortedTreeNode> KVPNode in mChildNodes)
              nodestobereturned.Add(KVPNode.Value.GetNodesFromLevel(level));
          }
        }
        return nodestobereturned;
      }

      internal void RemoveNodeFromChildNodes(T nodeValue)
      {
        int keytoberemoved = -1;
        foreach (KeyValuePair<int, SortedTreeNode> kvpnode in mChildNodes)
          if (kvpnode.Value.Value.Equals(nodeValue))
          {
            keytoberemoved = kvpnode.Key;
            break;
          }
        mChildNodes.Remove(keytoberemoved);
      }

      internal void RemoveFromParentNodes(T nodeValue)
      {
        int keytoberemoved = -1;
        foreach (KeyValuePair<int, SortedTreeNode> kvpnode in ParentNodes)
          if (kvpnode.Value.Value.Equals(nodeValue))
          {
            keytoberemoved = kvpnode.Key;
            break;
          }
        ParentNodes.Remove(keytoberemoved);
      }

      /// <summary>
      /// Gets the child nodes.
      /// </summary>
      /// <returns></returns>
      public SortedTreeNodeSortedList GetChildNodes()
      {
        return mChildNodes;
      }

      /// <summary>
      /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
      /// </summary>
      /// <param name="obj">The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>.</param>
      /// <returns>
      /// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
      /// </returns>
      /// <exception cref="T:System.NullReferenceException">The <paramref name="obj"/> parameter is null.</exception>
      public override bool Equals(object obj)
      {
        SortedTreeNode nodeval = (SortedTreeNode)obj;
        bool tobereturned = Value.Equals(nodeval.Value);
        return tobereturned;
      }

      /// <summary>
      /// Serves as a hash function for a particular type.
      /// </summary>
      /// <returns>
      /// A hash code for the current <see cref="T:System.Object"/>.
      /// </returns>
      public override int GetHashCode()
      {
        return Value.GetHashCode();
      }

      /// <summary>
      /// Marks all nodes as unvisited.
      /// </summary>
      public void MarkAllUnvisited()
      {
        Visited = false;
        foreach (KeyValuePair<int, SortedTreeNode> KVPNode in mChildNodes)
          KVPNode.Value.MarkAllUnvisited();
      }

      /// <summary>
      /// Gets the connector number of specified node.
      /// </summary>
      /// <param name="node">The node.</param>
      /// <returns></returns>
      internal int GetConnectorNumberOfSpecifiedNode(SortedTreeNode node)
      {
        int idx = -1;
        foreach (KeyValuePair<int, SortedTreeNode> kvp in mChildNodes)
          if (kvp.Value.Value.Equals(node.Value))
            idx = kvp.Key;
        if (idx < 0)
          throw new SortedTreeNodeException("this node does not contain such child node");
        return idx;
      }

      /// <summary>
      /// Adds the this node to another tree.
      /// </summary>
      /// <param name="tree">The tree.</param>
      public void AddThisNodeToAnotherTree(ref SortedTree<T> tree)
      {
        if (ParentNodes.Count == 0)
          tree.AddNode(Value);
        else
        {
          foreach (KeyValuePair<int, SortedTreeNode> kvpnode in ParentNodes)
          {
            int ParentConnector = kvpnode.Value.GetConnectorNumberOfSpecifiedNode(this);
            //zanim dodamy nowy element musimy sprawdzic czy juz jest dodany jego parent
            if (tree.GetNode(kvpnode.Value.Value) != null)
              tree.AddNode(kvpnode.Value.Value, ParentConnector, Value, kvpnode.Key);
          }
        }
      }

      /// <summary>
      /// Gets the next unvisited.
      /// </summary>
      /// <returns></returns>
      public SortedTreeNode GetNextUnvisited()
      {
        SortedTreeNode result = null;
        if (Visited)
          return null;
        foreach (KeyValuePair<int, SortedTreeNode> KVPNode in mChildNodes)
        {
          result = KVPNode.Value.GetNextUnvisited();
          if (result != null)
            return result;
        }
        Visited = true;
        return this;
      }

      /// <summary>
      /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
      /// </summary>
      /// <returns>
      /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
      /// </returns>
      public override string ToString()
      {
        StringBuilder result = new StringBuilder();
        result.Append("(");
        result.Append(Value.ToString());
        result.Append("(Height:" + Height.ToString() + ")");
        // result.Append("(ParentConnector:"+this.GetConnectorNumberOfSpecifiedNode(.ToString()+")");
        result.Append("[");
        foreach (KeyValuePair<int, SortedTreeNode> kvpnode in mChildNodes)
        {
          result.Append(kvpnode.Key);
          result.Append(":");
          result.Append(kvpnode.Value);
        }
        result.Append("]");
        return result.ToString();
      }

      internal void ClearParent()
      {
        ParentNodes.Clear();
      }

      internal void ParentCleanup()
      {
        SortedTreeNodeSortedList myparentclonnedlist = (SortedTreeNodeSortedList)((ICloneable)ParentNodes).Clone();
        foreach (KeyValuePair<int, SortedTreeNode> kvpnode in myparentclonnedlist)
        {
          if (MyParentTree.GetNode(kvpnode.Value.Value) == null)
            //oznacza to ze dany node ma na swojej liscie tego parenta a drzewo juz nie ma, nalezy wiec go usunac:
            ParentNodes.Remove(kvpnode.Key);
        }
        foreach (KeyValuePair<int, SortedTreeNode> kvpnode in mChildNodes)
          kvpnode.Value.ParentCleanup();
      }

      internal void SetParentTree(SortedTree<T> tree)
      {
        foreach (KeyValuePair<int, SortedTreeNode> kvp_childnode in mChildNodes)
          kvp_childnode.Value.SetParentTree(tree);
        MyParentTree = tree;
      }
    }
  }
}