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
using System.IO;

namespace CAS.Lib.RTLib.Utils
{
  /// <summary>
  /// Compliler that perform compilation of any cs class from stream or string
  /// </summary>
  public class CSharpStreamCompiller:CodeBuilderBase
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="CSharpStreamCompiller"/> class.
    /// </summary>
    /// <param name="streamSource">The stream source of source code.</param>
    /// <param name="referencedAssemblies">The referenced assemblies list.</param>
    public CSharpStreamCompiller( Stream streamSource, string[] referencedAssemblies ):
      this(new StreamReader( streamSource ).ReadToEnd(),referencedAssemblies)
    {
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="CSharpStreamCompiller"/> class.
    /// </summary>
    /// <param name="stringSource">The source code in string.</param>
    /// <param name="referencedAssemblies">The referenced assemblies list.</param>
    public CSharpStreamCompiller( string stringSource, string[] referencedAssemblies )
    {
      string source = stringSource;
      Source.Append( source );
      ReferencedAsseblies.AddRange( referencedAssemblies );
      PerformCompilation();
    }
  }
}
