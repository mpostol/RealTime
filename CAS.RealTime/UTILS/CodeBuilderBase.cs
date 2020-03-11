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
using System.Collections.Generic;
using System.Text;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.Collections.Specialized;
using System.Reflection;

namespace UAOOI.ProcessObserver.RealTime.Utils
{
  /// <summary>
  /// Base class for oder code builder classes
  /// </summary>
  public class CodeBuilderBase
  {
    private CompilerResults results;
    private string mErrorText = "";
    private StringBuilder _source = new StringBuilder();
    private bool isreadytouse = false;
    private StringCollection referencedAsseblies = new StringCollection();

    /// <summary>
    /// Create parameters for compiling
    /// </summary>
    /// <returns></returns>
    private static CompilerParameters CreateCompilerParameters(StringCollection referencedAsseblies)
    {
      //add compiler parameters and assembly references
      CompilerParameters compilerParams = new CompilerParameters();
      compilerParams.CompilerOptions = "/target:library /optimize";
      compilerParams.GenerateExecutable = false;
      compilerParams.GenerateInMemory = true;
      compilerParams.IncludeDebugInformation = false;
      foreach (string ref_as in referencedAsseblies)
        compilerParams.ReferencedAssemblies.Add(ref_as);
      //below default references
      compilerParams.ReferencedAssemblies.Add("mscorlib.dll");
      compilerParams.ReferencedAssemblies.Add("System.dll");
      compilerParams.ReferencedAssemblies.Add("System.Windows.Forms.dll");

      //add any aditional references needed
      //            foreach (string refAssembly in code.References)
      //              compilerParams.ReferencedAssemblies.Add(refAssembly);

      return compilerParams;
    }
    /// <summary>
    /// Compiles the code from the code string
    /// </summary>
    /// <param name="compiler"></param>
    /// <param name="parms"></param>
    /// <param name="source"></param>
    /// <returns></returns>
    private CompilerResults CompileCode(CSharpCodeProvider compiler, CompilerParameters parms, string source)
    {
      //actually compile the code
      CompilerResults results = compiler.CompileAssemblyFromSource(
                                  parms, source);
      //Do we have any compiler errors?
      if (results.Errors.Count > 0)
      {
        foreach (CompilerError error in results.Errors)
          WriteLine("Compile Error:" + error.ErrorText);
        return null;
      }
      return results;
    }
    /// <summary>
    /// Writes the output to the text box on the win form
    /// </summary>
    /// <param name="txt"></param>
    /// <param name="args"></param>
    private void WriteLine(string txt, params object[] args)
    {
      mErrorText += string.Format(txt, args) + "\r\n";
    }
    /// <summary>
    /// Compiles the c# into an assembly if there are no syntax errors
    /// </summary>
    /// <returns></returns>
    private CompilerResults CompileAssembly()
    {
      // create a compiler
      CSharpCodeProvider compiler = new CSharpCodeProvider();
      // get all the compiler parameters
      CompilerParameters parms = CreateCompilerParameters(referencedAsseblies);
      // compile the code into an assembly
      CompilerResults results = CompileCode(compiler, parms, _source.ToString());
      return results;
    }
    /// <summary>
    /// Performs the compilation.
    /// </summary>
    protected void PerformCompilation()
    {
      // compile the class into an in-memory assembly.
      // if it doesn't compile, show errors in the window
      results = CompileAssembly();
      if (results != null && results.CompiledAssembly != null)
      {
        isreadytouse = true;
      }
      else
      {
        isreadytouse = false;
      }
#if DEBUG
      Console.WriteLine("...........................\r\n");
      Console.WriteLine(Source.ToString());
#endif
    }
    /// <summary>
    /// Gets the compiled assembly.
    /// </summary>
    /// <value>The compiled assembly.</value>
    public Assembly CompiledAssembly()
    {
      // if the code compiled okay,
      // run the code using the new assembly (which is inside the results)
      if (Results != null && Results.CompiledAssembly != null)
      {
        return Results.CompiledAssembly;
      }
      else
      {
        throw new System.ArgumentException("Cannot Run The Code:" + SourceCode + "\r\n error:" + ErrorText);
      }
    }
    /// <summary>
    /// Gets the error text from last compilation or run.
    /// </summary>
    /// <value>The error text from last compilation or run.</value>
    public string ErrorText
    {
      get
      {
        return this.mErrorText;
      }
    }
    /// <summary>
    /// Gets the source code string builder object.
    /// </summary>
    /// <value>The source.</value>
    public StringBuilder Source
    {
      get { return _source; }
    }
    /// <summary>
    /// Gets the results of compilation.
    /// </summary>
    /// <value>The results.</value>
    public CompilerResults Results
    {
      get { return results; }
    }
    /// <summary>
    /// Gets a value indicating whether this instance is ready to use.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance is ready to use; otherwise, <c>false</c>.
    /// </value>
    public bool IsReadyToUse
    {
      get { return isreadytouse; }
    }
    /// <summary>
    /// Gets the referenced asseblies.
    /// </summary>
    /// <value>The referenced asseblies.</value>
    public StringCollection ReferencedAsseblies
    {
      get { return referencedAsseblies; }
    }
    /// <summary>
    /// Gets the source code.
    /// </summary>
    /// <value>The source code.</value>
    public string SourceCode
    {
      get
      {
        return _source.ToString();
      }
    }
  }
}
