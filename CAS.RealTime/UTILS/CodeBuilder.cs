//<summary>
//  Title   : Code Builder using Code Dom 
//  System  : Microsoft Visual C# .NET 2008
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//
//  20080503: mzbrzezny: created
//
//  Copyright (C)2008, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto://techsupp@cas.eu
//  http://www.cas.eu
//</summary>

using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.CSharp;

namespace UAOOI.ProcessObserver.RealTime.Utils
{
  /// <summary>
  /// Code Builder using Code Dom
  /// </summary>
  public class CodeBuilder: CodeBuilderBase
  {
    #region Private members
    private string mOutputText = "";
    #endregion private members
    #region private

    /// <summary>
    /// Runs the Calculate method in our on-the-fly assembly
    /// </summary>
    /// <param name="results"></param>
    private void RunCode(CompilerResults results)
    {
      Assembly executingAssembly = results.CompiledAssembly;
      try
      {
        //cant call the entry method if the assembly is null
        if (executingAssembly != null)
        {
          object assemblyInstance = executingAssembly.CreateInstance("ExpressionEvaluator.Calculator");
          //Use reflection to call the static Main function

          Module[] modules = executingAssembly.GetModules(false);
          Type[] types = modules[0].GetTypes();

          //loop through each class that was defined and look for the first occurrance of the entry point method
          foreach (Type type in types)
          {
            MethodInfo[] mis = type.GetMethods();
            foreach (MethodInfo mi in mis)
            {
              if (mi.Name == "Calculate")
              {
                object result = mi.Invoke(assemblyInstance, null);
                mOutputText = result.ToString();
              }
            }
          }
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine("An exception occurred while executing the script: {0}", ex.Message);
      }

    }
    CodeMemberField FieldVariable(string fieldName, string typeName, MemberAttributes accessLevel)
    {
      CodeMemberField field = new CodeMemberField(typeName, fieldName);
      field.Attributes = accessLevel;
      return field;
    }
    CodeMemberField FieldVariable(string fieldName, Type type, MemberAttributes accessLevel)
    {
      CodeMemberField field = new CodeMemberField(type, fieldName);
      field.Attributes = accessLevel;
      return field;
    }
    /// <summary>
    /// Very simplistic getter/setter properties
    /// </summary>
    /// <param name="propertyName">Name of the property.</param>
    /// <param name="internalName">Name of the internal.</param>
    /// <param name="type">The type.</param>
    /// <returns></returns>
    CodeMemberProperty MakeProperty(string propertyName, string internalName, Type type)
    {
      CodeMemberProperty myProperty = new CodeMemberProperty();
      myProperty.Name = propertyName;
      myProperty.Comments.Add(new CodeCommentStatement(String.Format("The {0} property is the returned result", propertyName)));
      myProperty.Attributes = MemberAttributes.Public;
      myProperty.Type = new CodeTypeReference(type);
      myProperty.HasGet = true;
      myProperty.GetStatements.Add(
        new CodeMethodReturnStatement(
          new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), internalName)));

      myProperty.HasSet = true;
      myProperty.SetStatements.Add(
        new CodeAssignStatement(
                    new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), internalName),
          new CodePropertySetValueReferenceExpression()));

      return myProperty;
    }
    /// <summary>
    /// Main driving routine for building a class
    /// </summary>
    void BuildClass(string expression)
    {
      StringWriter sw = new StringWriter(Source);

      //Declare your provider and generator
      CSharpCodeProvider codeProvider = new CSharpCodeProvider();
      ICodeGenerator generator = codeProvider.CreateGenerator(sw);
      CodeGeneratorOptions codeOpts = new CodeGeneratorOptions();

      CodeNamespace myNamespace = new CodeNamespace("ExpressionEvaluator");
      myNamespace.Imports.Add(new CodeNamespaceImport("System"));
      myNamespace.Imports.Add(new CodeNamespaceImport("System.Windows.Forms"));

      //Build the class declaration and member variables			
      CodeTypeDeclaration classDeclaration = new CodeTypeDeclaration();
      classDeclaration.IsClass = true;
      classDeclaration.Name = "Calculator";
      classDeclaration.Attributes = MemberAttributes.Public;
      classDeclaration.Members.Add(FieldVariable("answer", typeof(double), MemberAttributes.Private));

      //default constructor
      CodeConstructor defaultConstructor = new CodeConstructor();
      defaultConstructor.Attributes = MemberAttributes.Public;
      defaultConstructor.Comments.Add(new CodeCommentStatement("Default Constructor for class", true));
      defaultConstructor.Statements.Add(new CodeSnippetStatement("//TODO: implement default constructor"));
      classDeclaration.Members.Add(defaultConstructor);

      //property
      classDeclaration.Members.Add(this.MakeProperty("Answer", "answer", typeof(double)));

      //Our Calculate Method
      CodeMemberMethod myMethod = new CodeMemberMethod();
      myMethod.Name = "Calculate";
      myMethod.ReturnType = new CodeTypeReference(typeof(double));
      myMethod.Comments.Add(new CodeCommentStatement("Calculate an expression", true));
      myMethod.Attributes = MemberAttributes.Public;
      myMethod.Statements.Add(new CodeAssignStatement(new CodeSnippetExpression("Answer"), new CodeSnippetExpression(expression)));
      //myMethod.Statements.Add(new CodeSnippetExpression("MessageBox.Show(String.Format(\"Answer = {0}\", Answer))"));
      myMethod.Statements.Add(
          new CodeMethodReturnStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "Answer")));
      classDeclaration.Members.Add(myMethod);
      //write code
      myNamespace.Types.Add(classDeclaration);
      generator.GenerateCodeFromNamespace(myNamespace, sw, codeOpts);
      sw.Flush();
      sw.Close();
    }

    #endregion private
    #region public
    /// <summary>
    /// Initializes a new instance of the <see cref="CodeBuilder"/> class.
    /// </summary>
    /// <param name="expression">The expression to be calculated.</param>
    public CodeBuilder(string expression)
    {
      // build the class using codedom
      BuildClass(expression);

      PerformCompilation();
    }

    /// <summary>
    /// Runs the code.
    /// </summary>
    public void RunCode()
    {
      // if the code compiled okay,
      // run the code using the new assembly (which is inside the results)
      if (Results != null && Results.CompiledAssembly != null)
      {
        // run the evaluation function
        RunCode(Results);
      }
      else
      {
        throw new Exception("Cannot Run The Code:"+SourceCode+"\r\n error:"+ErrorText);
      }

    }
    /// <summary>
    /// Gets the output text from last run.
    /// </summary>
    /// <value>The output text from last run.</value>
    public string OutputTextFromLastRun
    {
      get
      {
        return this.mOutputText;
      }
    }
    #endregion public
  }
}
