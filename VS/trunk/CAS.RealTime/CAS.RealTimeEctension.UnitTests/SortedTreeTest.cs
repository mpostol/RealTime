//<summary>
//  Title   : Sorted Tree Tests
//  System  : Microsoft Visual C# .NET 2008
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//    20080408 mzbrzezny: - TreeHasChanged event is added
//                        - ConnectTheTreeToTheNode is fixed (SetParentTree fix)
//    20080315: mzbrzezny - created
//
//  Copyright (C)2008, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto://techsupp@cas.eu
//  http://www.cas.eu
//</summary>

using CAS.Lib.RTLib.Utils.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CAS.RealTime.UnitTests
{
  /// <summary>
  ///This is a test class for SortedTreeTest and is intended
  ///to contain all SortedTreeTest Unit Tests
  ///</summary>
  [TestClass()]
  public class SortedTreeTest
  {

    /// <summary>
    /// Class that is used to test the tree (this class is stored on the tree
    /// </summary>
    /// <typeparam name="T">the type of element stored inside of class</typeparam>
    public class ClassAsTreeValue<T>
    {
      /// <summary>
      /// the value
      /// </summary>
      public T m_value;
      /// <summary>
      /// Initializes a new instance of the <see cref="ClassAsTreeValue&lt;T&gt;"/> class.
      /// </summary>
      /// <param name="val">The value.</param>
      public ClassAsTreeValue(T val)
      {
        m_value = val;
      }
      /// <summary>
      /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
      /// </summary>
      /// <returns>
      /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
      /// </returns>
      public override string ToString()
      {
        return m_value.ToString();
      }
    }

    #region Additional test attributes
    /// <summary>
    /// Initializes this instance.
    /// </summary>
    [TestInitialize()]
    public void Initialize()
    {
      A = new ClassAsTreeValue<string>("A");
      B = new ClassAsTreeValue<string>("B");
      C = new ClassAsTreeValue<string>("C");
      D = new ClassAsTreeValue<string>("D");
      E = new ClassAsTreeValue<string>("E");
      F = new ClassAsTreeValue<string>("F");
      G = new ClassAsTreeValue<string>("G");
      H = new ClassAsTreeValue<string>("H");
      I = new ClassAsTreeValue<string>("I");
      J = new ClassAsTreeValue<string>("J");
      K = new ClassAsTreeValue<string>("K");
      L = new ClassAsTreeValue<string>("L");
      M = new ClassAsTreeValue<string>("M");
      N = new ClassAsTreeValue<string>("N");
    }
    #endregion

    private ClassAsTreeValue<string> A, B, C, D, E, F, G, H, I, J, K, L, M, N;
    /// <summary>
    /// Helper that create the example tree.
    /// </summary>
    /// <param name="tree">The tree.</param>
    private void AddNodeWholeTreeTestHelper(ref SortedTree<ClassAsTreeValue<string>> tree)
    {
      //K
      //|
      //A     B
      //|\     \
      //C D     E
      // / \   / \
      // F  G  H  I
      //     \/
      //     J
      SortedTree<ClassAsTreeValue<string>>.SortedTreeNode node;
      SortedTree<ClassAsTreeValue<string>>.SortedTreeNode parentnode;
      SortedTree<ClassAsTreeValue<string>>.SortedTreeNode parentnode2;
      node = tree.AddNode(K);
      Assert.AreEqual(node.Value, K, "Element that is added must be equal to oryginal element");
      Assert.AreEqual(0, node.ParentNodesCount, "Parrent Node for root element must be null");
      parentnode = node;
      node = tree.AddNode(K, 0, A, 0);
      Assert.AreEqual(node.Value, A, "Element that is added must be equal to original element");
      Assert.AreEqual(node.GetParentNodeByParentNodeNumber(node.GetParentConnectorNumber(K)), parentnode, "Parent Node for A element must be K");
      parentnode = node;
      node = tree.AddNode(A, 0, C, 0);
      Assert.AreEqual(node.Value, C, "Element that is added must be equal to original element");
      Assert.AreEqual(node.GetParentNodeByParentNodeNumber(node.GetParentConnectorNumber(A)), parentnode, "Parent Node for C element must be A");
      node = tree.AddNode(A, 1, D, 0);
      Assert.AreEqual(node.Value, D, "Element that is added must be equal to original element");
      Assert.AreEqual(node.GetParentNodeByParentNodeNumber(node.GetParentConnectorNumber(A)), parentnode, "Parent Node for D element must be A");
      parentnode2 = node; //D is saved here
      parentnode = null;
      node = tree.AddNode(null, 0, B, 0);
      Assert.AreEqual(B, node.Value, "Element that is added must be equal to original element");
      Assert.AreEqual(0, node.ParentNodesCount, "Parent Node for root element must be null");
      parentnode = node;
      node = tree.AddNode(B, 0, E, 0);
      Assert.AreEqual(node.Value, E, "Element that is added must be equal to original element");
      Assert.AreEqual(node.GetParentNodeByParentNodeNumber(node.GetParentConnectorNumber(B)), parentnode, "Parent Node for E element must be B");
      parentnode = node;// save E;
      node = tree.AddNode(D, 0, F, 0);
      Assert.AreEqual(F, node.Value, "Element that is added must be equal to original element");
      Assert.AreEqual(parentnode2, node.GetParentNodeByParentNodeNumber(node.GetParentConnectorNumber(D)), "Parent Node for F element must be D");
      node = tree.AddNode(D, 1, G, 1);
      Assert.AreEqual(G, node.Value, "Element that is added must be equal to original element");
      Assert.AreEqual(parentnode2, node.GetParentNodeByParentNodeNumber(node.GetParentConnectorNumber(D)), "Parent Node for G element must be D");
      parentnode2 = node; //save G
      node = tree.AddNode(E, 1, I, 0);
      Assert.AreEqual(I, node.Value, "Element that is added must be equal to original element");
      Assert.AreEqual(parentnode, node.GetParentNodeByParentNodeNumber(node.GetParentConnectorNumber(E)), "Parent Node for I element must be E");
      node = tree.AddNode(E, 0, H, 0);
      Assert.AreEqual(H, node.Value, "Element that is added must be equal to original element");
      Assert.AreEqual(parentnode, node.GetParentNodeByParentNodeNumber(node.GetParentConnectorNumber(E)), "Parent Node for H element must be E");
      parentnode = node;//save H
      node = tree.AddNode(G, 0, J, 0);
      Assert.AreEqual(J, node.Value, "Element that is added must be equal to original element");
      Assert.AreEqual(parentnode2, node.GetParentNodeByParentNodeNumber(node.GetParentConnectorNumber(G)), "Parent Node for J element must be G");
      node = tree.AddNode(H, 0, J, 1);
      Assert.AreEqual(J, node.Value, "Element that is added must be equal to original element");
      Assert.AreEqual(parentnode, node.GetParentNodeByParentNodeNumber(node.GetParentConnectorNumber(H)), "Parent Node for J element must be H");
    }
    /// <summary>
    ///A test for GetNode
    ///</summary>
    public void GetNodeTestHelper(ref SortedTree<ClassAsTreeValue<string>> tree)
    {
      AddNodeTestHelper(ref tree);
      SortedTree<ClassAsTreeValue<string>>.SortedTreeNode node = tree.GetNode(A);
      Assert.IsTrue(node != null, "there should be one (A) element on the tree");
      Assert.AreEqual(node.Value, A);
    }
    private static void AreEqualGetFroeachRepresentationHelper(string expected, SortedTree<ClassAsTreeValue<string>> tree, string comment)
    {
      AreEqualToStringAfterCloneHelper(tree, comment);
      string actual;
      actual = GetForeachRepresentation(tree);
      Assert.AreEqual(expected, actual, "Foreach mismatch; " + comment);
    }
    private static void AreEqualToStringAfterCloneHelper(SortedTree<ClassAsTreeValue<string>> tree, string comment)
    {
      string expected = tree.ToString();
      string actual = ((SortedTree<ClassAsTreeValue<string>>)(((ICloneable)tree).Clone())).ToString();
      Assert.AreEqual(expected, actual, "ToString mismatch; " + comment);
    }
    /// <summary>
    /// A Test for GetNode.
    /// </summary>
    [TestMethod()]
    public void TestGetNode()
    {
      SortedTree<ClassAsTreeValue<string>> tree = new SortedTree<ClassAsTreeValue<string>>();
      GetNodeTestHelper(ref tree);
    }

    /// <summary>
    ///A test for AddNode
    ///</summary>
    public void AddNodeTestHelper(ref SortedTree<ClassAsTreeValue<string>> tree)
    {
      SortedTree<ClassAsTreeValue<string>>.SortedTreeNode node = tree.AddNode(A);
      Assert.AreEqual(node.Value, A, "values should be equal");
    }

    /// <summary>
    ///A test for AddNode
    ///</summary>
    [TestMethod()]
    public void TestAddNode()
    {
      SortedTree<ClassAsTreeValue<string>> tree = new SortedTree<ClassAsTreeValue<string>>();
      AddNodeTestHelper(ref tree);

    }
    /// <summary>
    /// Test of foreach and enumerator
    /// </summary>
    [TestMethod()]
    public void TestForeach()
    {
      SortedTree<ClassAsTreeValue<string>> tree = new SortedTree<ClassAsTreeValue<string>>();
      AddNodeWholeTreeTestHelper(ref tree);

      string expected = "CFJGDAKHIEB";
      string actual = GetForeachRepresentation(tree);
      Assert.AreEqual(expected, actual, "Problem with foreach statement: tree are not the same");
    }

    private static string GetForeachRepresentation(SortedTree<ClassAsTreeValue<string>> tree)
    {
      string actual = "";
      foreach (ClassAsTreeValue<string> values in tree)
      {
        actual += values.m_value;
      }
      return actual;
    }
    /// <summary>
    /// Test of Height function
    /// </summary>
    [TestMethod()]
    public void TestHeight()
    {
      SortedTree<ClassAsTreeValue<string>> tree = new SortedTree<ClassAsTreeValue<string>>();
      AddNodeWholeTreeTestHelper(ref tree);
      Assert.AreEqual(5, tree.Height, "Wrong Height of the tree");
    }
    /// <summary>
    /// Test of Count function
    /// </summary>
    [TestMethod()]
    public void TestCount()
    {
      SortedTree<ClassAsTreeValue<string>> tree = new SortedTree<ClassAsTreeValue<string>>();
      AddNodeWholeTreeTestHelper(ref tree);
      Assert.AreEqual(11, tree.Count, "Wrong count of elements");
    }
    /// <summary>
    /// Test of ToString function
    /// </summary>
    [TestMethod()]
    public void TestToString()
    {
      SortedTree<ClassAsTreeValue<string>> tree = new SortedTree<ClassAsTreeValue<string>>();
      AddNodeWholeTreeTestHelper(ref tree);

      string expected = "CAS.Lib.RTLib.Utils.Collections.Generic.SortedTree`1[CAS.RealTime.UnitTests.SortedTreeTest+ClassAsTreeValue`1[System.String]] elements:{(K(Height:5)[0:(A(Height:4)[0:(C(Height:1)[]1:(D(Height:3)[0:(F(Height:1)[]1:(G(Height:2)[0:(J(Height:1)[]]]]](B(Height:4)[0:(E(Height:3)[0:(H(Height:2)[0:(J(Height:1)[]]1:(I(Height:1)[]]]}";
      string actual = tree.ToString();
      Assert.AreEqual(expected, actual, "Wrong string representation");
    }
    /// <summary>
    /// Test of GetNodesFromLevel function
    /// </summary>
    [TestMethod()]
    public void TestGetNodesFromLevel()
    {
      SortedTree<ClassAsTreeValue<string>> tree = new SortedTree<ClassAsTreeValue<string>>();
      AddNodeWholeTreeTestHelper(ref tree);

      string expected = "";
      GetNodesFromLevelHelper(tree, expected, 0);

      expected = "CFJI";
      GetNodesFromLevelHelper(tree, expected, 1);

      expected = "GH";
      GetNodesFromLevelHelper(tree, expected, 2);

      expected = "DE";
      GetNodesFromLevelHelper(tree, expected, 3);

      expected = "AB";
      GetNodesFromLevelHelper(tree, expected, 4);

      expected = "K";
      GetNodesFromLevelHelper(tree, expected, 5);
    }

    private static void GetNodesFromLevelHelper(SortedTree<ClassAsTreeValue<string>> tree, string expected, int level)
    {
      string actual = "";
      foreach (SortedTree<ClassAsTreeValue<string>>.SortedTreeNode value in tree.GetNodesFromLevel(level))
      {
        actual += value.Value.ToString();
      }
      Assert.AreEqual(expected, actual, "Problem with GetNodesFromLevel level:" + level.ToString());
    }

    /// <summary>
    /// Tests the remove connection betweeen parent and child and move element to roots.
    /// </summary>
    [TestMethod()]
    public void TestRemoveConnectionBetweeenParentAndChildAndMoveElementToRoots()
    {

      //K
      //|
      //A     B
      //|\     \
      //C D     E
      // / \   / \
      // F  G  H  I
      //     \/
      //     J


      SortedTree<ClassAsTreeValue<string>> tree = new SortedTree<ClassAsTreeValue<string>>();
      AddNodeWholeTreeTestHelper(ref tree);

      string expected;
      tree.RemoveConnectionBetweeenParentAndChildAndMoveElementToRoots(D, G);

      //K
      //|
      //A     B
      //|\     \
      //C D     E
      // /     / \
      // F  G  H  I
      //     \/
      //     J
      expected = "CFDAKJHIEBG";

      string actual = GetForeachRepresentation(tree);
      Assert.AreEqual(expected, actual, "Problem with foreach statement: tree are not the same");

      expected = "";
      GetNodesFromLevelHelper(tree, expected, 0);

      expected = "CFJI";
      GetNodesFromLevelHelper(tree, expected, 1);

      expected = "DHG";
      GetNodesFromLevelHelper(tree, expected, 2);

      expected = "AE";
      GetNodesFromLevelHelper(tree, expected, 3);

      expected = "KB";
      GetNodesFromLevelHelper(tree, expected, 4);

      expected = "";
      GetNodesFromLevelHelper(tree, expected, 5);



      tree.RemoveConnectionBetweeenParentAndChildAndMoveElementToRoots(H, J);

      //K
      //|
      //A     B
      //|\     \
      //C D     E
      // /     / \
      // F  G  H  I
      //     \ 
      //     J
      expected = "CFDAKHIEBJG";

      actual = GetForeachRepresentation(tree);
      Assert.AreEqual(expected, actual, "Problem with foreach statement: tree are not the same");

      expected = "";
      GetNodesFromLevelHelper(tree, expected, 0);

      expected = "CFHIJ";
      GetNodesFromLevelHelper(tree, expected, 1);

      expected = "DEG";
      GetNodesFromLevelHelper(tree, expected, 2);

      expected = "AB";
      GetNodesFromLevelHelper(tree, expected, 3);

      expected = "K";
      GetNodesFromLevelHelper(tree, expected, 4);

      expected = "";
      GetNodesFromLevelHelper(tree, expected, 5);

    }

    /// <summary>
    /// Tests the remove value shallow.
    /// </summary>
    [TestMethod()]
    public void TestRemoveValueShallow()
    {
      //K
      //|
      //A     B
      //|\     \
      //C D     E
      // / \   / \
      // F  G  H  I
      //     \/
      //     J


      SortedTree<ClassAsTreeValue<string>> tree = new SortedTree<ClassAsTreeValue<string>>();
      AddNodeWholeTreeTestHelper(ref tree);

      string expected;
      string actual;
      tree.RemoveValue(D, true);

      //K
      //|
      //A     B
      //|      \
      //C       E
      //       / \
      // F  G  H  I
      //     \/
      //     J
      expected = "CAKJHIEBFG";
      actual = GetForeachRepresentation(tree);
      Assert.AreEqual(expected, actual, "Problem with foreach statement: tree are not the same");

      tree.RemoveValue(B, true);
      expected = "CAKFJGHIE";
      actual = GetForeachRepresentation(tree);
      Assert.AreEqual(expected, actual, "Problem with foreach statement: tree are not the same");

      tree.RemoveValue(J, true);
      expected = "CAKFGHIE";
      actual = GetForeachRepresentation(tree);
      Assert.AreEqual(expected, actual, "Problem with foreach statement: tree are not the same");

      tree.RemoveValue(G, true);
      expected = "CAKFHIE";
      actual = GetForeachRepresentation(tree);
      Assert.AreEqual(expected, actual, "Problem with foreach statement: tree are not the same");
    }

    /// <summary>
    /// Tests the remove value deep.
    /// </summary>
    [TestMethod()]
    public void TestRemoveValueDeep()
    {
      //K
      //|
      //A     B
      //|\     \
      //C D     E
      // / \   / \
      // F  G  H  I
      //     \/
      //     J


      SortedTree<ClassAsTreeValue<string>> tree = new SortedTree<ClassAsTreeValue<string>>();
      AddNodeWholeTreeTestHelper(ref tree);

      string expected;
      string actual;
      tree.RemoveValue(D, false);

      //K
      //|
      //A     B
      //|      \
      //C       E
      //       / \
      //       H  I
      //      /
      //     J
      expected = "CAKJHIEB";
      actual = GetForeachRepresentation(tree);
      Assert.AreEqual(expected, actual, "Problem with foreach statement: tree are not the same");

      tree.RemoveValue(B, false);
      expected = "CAK";
      actual = GetForeachRepresentation(tree);
      Assert.AreEqual(expected, actual, "Problem with foreach statement: tree are not the same");

      tree.RemoveValue(C, false);
      expected = "AK";
      actual = GetForeachRepresentation(tree);
      Assert.AreEqual(expected, actual, "Problem with foreach statement: tree are not the same");

      tree.RemoveValue(K, false);
      expected = "";
      actual = GetForeachRepresentation(tree);
      Assert.AreEqual(expected, actual, "Problem with foreach statement: tree are not the same");

      tree = new SortedTree<ClassAsTreeValue<string>>();
      AddNodeWholeTreeTestHelper(ref tree);
      tree.RemoveValue(J, false);
      expected = "CFGDAKHIEB";
      actual = GetForeachRepresentation(tree);
      Assert.AreEqual(expected, actual, "Problem with foreach statement: tree are not the same");

      tree = new SortedTree<ClassAsTreeValue<string>>();
      AddNodeWholeTreeTestHelper(ref tree);
      tree.RemoveValue(K, false);
      expected = "JHIEB";
      actual = GetForeachRepresentation(tree);
      Assert.AreEqual(expected, actual, "Problem with foreach statement: tree are not the same");
      tree.RemoveValue(J, false);
      expected = "HIEB";
      actual = GetForeachRepresentation(tree);
      Assert.AreEqual(expected, actual, "Problem with foreach statement: tree are not the same");


    }
    /// <summary>
    /// Tests the get subtree.
    /// </summary>
    [TestMethod()]
    public void TestGetSubtree()
    {
      //K
      //|
      //A     B
      //|\     \
      //C D     E
      // / \   / \
      // F  G  H  I
      //     \/
      //     J
      SortedTree<ClassAsTreeValue<string>> tree = new SortedTree<ClassAsTreeValue<string>>();
      SortedTree<ClassAsTreeValue<string>> tree2;
      AddNodeWholeTreeTestHelper(ref tree);

      string expected;
      string actual;
      tree2 = tree.GetSubtreeFromNode(A);

      //
      //
      //A     
      //|\     
      //C D     
      // / \    
      // F  G  
      //     \/
      //     J  
      expected = "CFJGDA";
      actual = GetForeachRepresentation(tree2);
      Assert.AreEqual(expected, actual, "Problem with foreach statement: tree are not the same");

      tree2 = tree.GetSubtreeFromNode(D);
      //  D    
      // / \  
      // F  G  
      //     \
      //     J
      expected = "FJGD";
      actual = GetForeachRepresentation(tree2);
      Assert.AreEqual(expected, actual, "Problem with foreach statement: tree are not the same");

      tree2 = tree2.GetSubtreeFromNode(D);
      //  D    
      // / \  
      // F  G  
      //     \
      //     J
      expected = "FJGD";
      actual = GetForeachRepresentation(tree2);
      Assert.AreEqual(expected, actual, "Problem with foreach statement: tree are not the same");


      tree2 = tree2.GetSubtreeFromNode(F);
      expected = "F";
      actual = GetForeachRepresentation(tree2);
      Assert.AreEqual(expected, actual, "Problem with foreach statement: tree are not the same");

      tree2 = tree.GetSubtreeFromNode(G);
      //  D    
      // / \  
      // F  G  
      //     \
      //     J
      expected = "JG";
      actual = GetForeachRepresentation(tree2);
      Assert.AreEqual(expected, actual, "Problem with foreach statement: tree are not the same");
    }
    /// <summary>
    /// Tests the test tree if can be connected.
    /// </summary>
    [TestMethod()]
    public void TestTestTreeIfCanBeConnected()
    {
      //K
      //|
      //A     B
      //|\     \
      //C D     E
      // / \   / \
      // F  G  H  I
      //     \/
      //     J
      SortedTree<ClassAsTreeValue<string>> tree = new SortedTree<ClassAsTreeValue<string>>();
      AddNodeWholeTreeTestHelper(ref tree);
      SortedTree<ClassAsTreeValue<string>> tree2 = tree.GetSubtreeFromNode(B);
      tree2.RemoveValue(J, false);
      bool result;
      result = tree.TestTreeIfCanBeConnected(J, 0, tree2, 0);
      Assert.AreEqual(false, result, "This trees cannot be connected");
      tree.RemoveValue(B, false);
      result = tree.TestTreeIfCanBeConnected(J, 0, tree2, 0);
      Assert.AreEqual(true, result, "This trees can be connected");
    }
    /// <summary>
    /// Tests the connect the tree to the node.
    /// </summary>
    [TestMethod()]
    public void TestConnectTheTreeToTheNode()
    {
      //K
      //|
      //A     B
      //|\     \
      //C D     E
      // / \   / \
      // F  G  H  I
      //     \/
      //     J
      SortedTree<ClassAsTreeValue<string>> tree = new SortedTree<ClassAsTreeValue<string>>();
      AddNodeWholeTreeTestHelper(ref tree);
      SortedTree<ClassAsTreeValue<string>> tree2 = tree.GetSubtreeFromNode(B);
      //tree2:
      //   B
      //   |
      //   E
      //  / \
      // H  I
      // |
      // J
      AreEqualGetFroeachRepresentationHelper("JHIEB", tree2, "prep step1");
      tree2.RemoveValue(J, false);
      AreEqualGetFroeachRepresentationHelper("HIEB", tree2, "prep step2");
      tree.RemoveValue(B, false);
      AreEqualGetFroeachRepresentationHelper("CFJGDAK", tree, "prep step3");

      //main test
      //K
      //|
      //A     
      //|\     
      //C D     
      // / \   
      // F  G  
      //     \
      //     J
      //    /
      //   B
      //   |
      //   E
      //  / \
      // H  I
      tree.ConnectTheTreeToTheNode(J, 0, tree2, 0);
      AreEqualGetFroeachRepresentationHelper("CFHIEBJGDAK", tree, "main test");
    }
    /// <summary>
    /// Tests the test node if can be connected.
    /// </summary>
    [TestMethod()]
    public void TestTestNodeIfCanBeConnected()
    {
      //K
      //|
      //A     B
      //|\     \
      //C D     E
      // / \   / \
      // F  G  H  I
      //     \/
      //     J
      SortedTree<ClassAsTreeValue<string>> tree = new SortedTree<ClassAsTreeValue<string>>();
      AddNodeWholeTreeTestHelper(ref tree);
      bool result;
      result = tree.TestNodeIfCanBeConnected(J, 0, B, 0);
      Assert.AreEqual(false, result, "This trees cannot be connected");
      tree.RemoveConnectionBetweeenParentAndChildAndMoveElementToRoots(H, J);
      result = tree.TestNodeIfCanBeConnected(J, 0, B, 0);
      Assert.AreEqual(true, result, "This trees can be connected");
    }
    /// <summary>
    /// Tests the connect the node to other node.
    /// </summary>
    [TestMethod()]
    public void TestConnectTheNodeToOtherNode()
    {
      //K
      //|
      //A     B
      //|\     \
      //C D     E
      // / \   / \
      // F  G  H  I
      //     \/
      //     J
      SortedTree<ClassAsTreeValue<string>> tree = new SortedTree<ClassAsTreeValue<string>>();
      AddNodeWholeTreeTestHelper(ref tree);
      tree.RemoveConnectionBetweeenParentAndChildAndMoveElementToRoots(H, J);
      tree.ConnectTheNodeToOtherNode(J, 0, B, 0);

      string expected;
      string actual;
      expected = "CFHIEBJGDAK";
      actual = GetForeachRepresentation(tree);
      Assert.AreEqual(expected, actual, "Problem with foreach statement: tree are not the same");
    }
    /// <summary>
    /// Tests the connect the node to other node.
    /// </summary>
    [TestMethod()]
    public void TestConnectTheNodeToOtherNode_Test2_RootTest()
    {
      //K
      //|
      //A     B
      //|\     \
      //C D     E
      // / \   / \
      // F  G  H  I
      //     \/
      //     J
      SortedTree<ClassAsTreeValue<string>> tree = new SortedTree<ClassAsTreeValue<string>>();
      AddNodeWholeTreeTestHelper(ref tree);
      AreEqualGetFroeachRepresentationHelper("CFJGDAKHIEB", tree, "after initialization");
      Assert.AreEqual(2, tree.GetRoots().Count, "there should be 2 roots");
      tree.AddNode(L);
      AreEqualGetFroeachRepresentationHelper("CFJGDAKHIEBL", tree, "after L adding");
      Assert.AreEqual(3, tree.GetRoots().Count, "there should be 3 roots");
      tree.AddNode(M);
      AreEqualGetFroeachRepresentationHelper("CFJGDAKHIEBLM", tree, "after M adding");
      Assert.AreEqual(4, tree.GetRoots().Count, "there should be 4 roots");
      tree.ConnectTheNodeToOtherNode(L, 0, M, 0);
      Assert.AreEqual(3, tree.GetRoots().Count, "there should be 3 roots");
      AreEqualGetFroeachRepresentationHelper("CFJGDAKHIEBML", tree, "after connecting L and M");
      tree.AddNode(N);
      Assert.AreEqual(4, tree.GetRoots().Count, "there should be 4 roots");
      AreEqualGetFroeachRepresentationHelper("CFJGDAKHIEBMLN", tree, "after N adding");
      tree.ConnectTheNodeToOtherNode(J, 0, L, 0);
      Assert.AreEqual(3, tree.GetRoots().Count, "there should be 3 roots");
      AreEqualGetFroeachRepresentationHelper("CFMLJGDAKHIEBN", tree, "after connecting J and L");
    }
    /// <summary>
    /// Tests the connect the node to other node.
    /// </summary>
    [TestMethod()]
    public void TestConnectTheNodeToOtherNode_Test2_RootTest2()
    {
      //K
      //|
      //A     B
      //|\     \
      //C D     E
      // / \   / \
      // F  G  H  I
      //     \/
      //     J
      SortedTree<ClassAsTreeValue<string>> tree = new SortedTree<ClassAsTreeValue<string>>();
      AddNodeWholeTreeTestHelper(ref tree);
      AreEqualGetFroeachRepresentationHelper("CFJGDAKHIEB", tree, "after initialization");
      Assert.AreEqual(2, tree.GetRoots().Count, "there should be 2 roots");
      tree.AddNode(L);
      AreEqualGetFroeachRepresentationHelper("CFJGDAKHIEBL", tree, "after L adding");
      Assert.AreEqual(3, tree.GetRoots().Count, "there should be 3 roots");
      tree.RemoveConnectionBetweeenParentAndChildAndMoveElementToRoots(K, A);
      Assert.AreEqual(4, tree.GetRoots().Count, "there should be 4 roots");
      tree.AddNode(M);
      Assert.AreEqual(5, tree.GetRoots().Count, "there should be 5 roots");
      //do tego dazymy:
      //K M
      //|/
      //L  (to jest splitter)
      //|
      //A     B
      //|\     \
      //C D     E
      // / \   / \
      // F  G  H  I
      //     \/
      //     J
      tree.ConnectTheNodeToOtherNode(L, 0, A, 0);
      Assert.AreEqual(4, tree.GetRoots().Count, "there should be 4 roots");
      tree.ConnectTheNodeToOtherNode(M, 0, L, 1);
      Assert.AreEqual(3, tree.GetRoots().Count, "there should be 3 roots");
      tree.ConnectTheNodeToOtherNode(K, 0, L, 0);
      Assert.AreEqual(3, tree.GetRoots().Count, "there should be 3 roots");
      AreEqualGetFroeachRepresentationHelper("CFJGDALKHIEBM", tree, "after all (after splitter adding)");

    }
    /// <summary>
    /// Tests the connect the node to other node.
    /// </summary>
    [TestMethod()]
    public void TestConnectTheNodeToOtherNode_Test2_RootTest3()
    {
      //K L
      //|/
      //A     
      //|
      //C
      SortedTree<ClassAsTreeValue<string>> tree = new SortedTree<ClassAsTreeValue<string>>();
      tree.AddNode(C);
      tree.AddNode(K);
      tree.AddNode(L);
      tree.AddNode(A);
      tree.ConnectTheNodeToOtherNode(A, 0, C, 0);
      tree.ConnectTheNodeToOtherNode(L, 0, A, 0);
      tree.ConnectTheNodeToOtherNode(K, 0, A, 1);
      AreEqualGetFroeachRepresentationHelper("CAKL", tree, "after all (after splitter adding)");
    }
    /// <summary>
    /// Tests the move node to roots.
    /// </summary>
    [TestMethod()]
    public void TestMoveNodeToRoots()
    {
      //K
      //|
      //A     B
      //|\     \
      //C D     E
      // / \   / \
      // F  G  H  I
      //     \/
      //     J
      SortedTree<ClassAsTreeValue<string>> tree = new SortedTree<ClassAsTreeValue<string>>();
      AddNodeWholeTreeTestHelper(ref tree);
      tree.MoveNodeToRoots(G);
      string expected;
      string actual;
      expected = "CFDAKJHIEBG";
      actual = GetForeachRepresentation(tree);
      Assert.AreEqual(expected, actual, "Problem with foreach statement: tree are not the same");
      tree.MoveNodeToRoots(J);
      expected = "CFDAKHIEBGJ";
      actual = GetForeachRepresentation(tree);
      Assert.AreEqual(expected, actual, "Problem with foreach statement: tree are not the same");
    }

  }
}
