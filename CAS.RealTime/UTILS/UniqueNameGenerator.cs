//<summary>
//  Title   : Class that is responsible for generating unique names
//  System  : Microsoft Visual C# .NET 2008
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//
//  20080704 mzbrzezny: new function that checks the name and if name exists it returns suggested name.
//  200805:  mzbrzezny:  created
//
//  Copyright (C)2008, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto://techsupp@cas.eu
//  http://www.cas.eu
//</summary>

using System;
using System.Collections.Generic;

namespace UAOOI.ProcessObserver.RealTime.Utils
{
  /// <summary>
  /// Class that is responsible for generating unique names
  /// this list contains the list of names, no name can be the same. 
  /// This class can generate unique names based on the template.
  /// Client can add names to the list and class engine is responsible that no names are the same
  /// </summary>
  public class UniqueNameGenerator
  {
    private List<string> mList = new List<string>();
    private readonly string prefix;
    int counter = 0;
    /// <summary>
    /// the exception thrown by UniqueNameGenerator
    /// </summary>
    [Serializable]
    public class UniqueNameGeneratorException : Exception
    {
      internal UniqueNameGeneratorException(string message)
        : base(message)
      { }
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="UniqueNameGenerator"/> class.
    /// </summary>
    /// <param name="Prefix">The prefix.</param>
    public UniqueNameGenerator(string Prefix)
    {
      prefix = Prefix;
    }
    /// <summary>
    /// Checks if name exists.
    /// </summary>
    /// <param name="Name">The name.</param>
    /// <returns>true if name exists in generator</returns>
    public bool CheckIfNameExists(string Name)
    {
      return mList.Contains(Name);
    }
    /// <summary>
    /// Checks the name and if name exists it returns suggested name.
    /// </summary>
    /// <param name="Name">The name.</param>
    /// <returns>the suggested name</returns>
    public string CheckIfNameExistsAndReturnSuggestedName(string Name)
    {
      int counter = 0;
      if (!mList.Contains(Name))
        return Name;
      else
      {
        string newname = "";
        do { newname = Name + "_" + (++counter).ToString(); }
        while (mList.Contains(newname));
        return newname;
      }
    }
    /// <summary>
    /// Adds the name.
    /// </summary>
    /// <param name="NewName">The new name.</param>
    /// <param name="ThrowExceptionIfNameExistOnTheListOfPreviousNames">if set to <c>true</c> [throw exception if name exist on the list of previous names].</param>
    public void AddName(string NewName, bool ThrowExceptionIfNameExistOnTheListOfPreviousNames)
    {
      lock (this)
      {
        if (mList.Contains(NewName))
        {
          if (ThrowExceptionIfNameExistOnTheListOfPreviousNames)
            throw new UniqueNameGeneratorException("This generator already contains such name");
          else
            return;
        }
        mList.Add(NewName);
      }
    }
    /// <summary>
    /// Adds the name.
    /// </summary>
    /// <param name="NewName">The new name.</param>
    public void AddName(string NewName)
    {
      AddName(NewName, true);
    }
    /// <summary>
    /// Removes the name.
    /// </summary>
    /// <param name="NameToBeRemoved">The name to be removed.</param>
    public void RemoveName(string NameToBeRemoved)
    {
      mList.Remove(NameToBeRemoved);
    }
    /// <summary>
    /// Generates the new name.
    /// </summary>
    /// <returns>new name</returns>
    public string GenerateNewName()
    {
      string nametobereturned;
      lock (this)
      {
        do
        {
          nametobereturned = String.Format(prefix + "{0}", counter++);
        } while (mList.Contains(nametobereturned));
        mList.Add(nametobereturned);
      }
      return nametobereturned;
    }
  }
}
