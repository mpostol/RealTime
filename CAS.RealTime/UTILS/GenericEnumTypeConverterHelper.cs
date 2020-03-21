//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace UAOOI.ProcessObserver.RealTime.Utils
{
  /// <summary>
  /// Generic Enum TypeConverter Helper
  /// </summary>
  public class GenericEnumTypeConverterHelper<EnumType> : StringConverter
  {
    private static SortedList<string, object> EnumAsSortedList = new SortedList<string, object>();

    static GenericEnumTypeConverterHelper()
    {
      Array EnumValues = Enum.GetValues(typeof(EnumType));
      foreach (object val in EnumValues)
      {
        EnumAsSortedList.Add(((EnumType)val).ToString(), val);
      }
    }

    /// <summary>
    /// Gets the value from string.
    /// </summary>
    /// <param name="Name">The name.</param>
    /// <returns></returns>
    public static object GetValueFromString(string Name)
    {
      return EnumAsSortedList[Name];
    }

    /// <summary>
    /// Gets the name from value.
    /// </summary>
    /// <param name="Value">The value.</param>
    /// <returns></returns>
    public static string GetNameFromValue(object Value)
    {
      return ((EnumType)Value).ToString();
    }

    /// <summary>
    /// Gets a value indicating whether this object supports a standard set of values that can be picked from a list.
    /// </summary>
    /// <param name="context">An <see cref="ITypeDescriptorContext"/> that provides a format context. </param>
    /// <returns>Always returns <b>true</b> - means show a combo box </returns>
    public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
    {
      //true means show a combo-box
      return true;
    }

    /// <summary>
    /// Gets a value indicating whether the list of standard values returned from the GetStandardValues method is an exclusive list.
    /// </summary>
    /// <param name="context">An <see cref="ITypeDescriptorContext"/> that provides a format context. </param>
    /// <returns>Always returns <b>true</b> - means it limits to list</returns>
    public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
    {
      //true will limit to list. false will show the list, but allow free-form entry
      return true;
    }

    /// <summary>
    /// Gets a collection of standard values for the data type this validator is designed for.
    /// </summary>
    /// <param name="context">An <see cref="ITypeDescriptorContext"/> that provides a format context. </param>
    /// <returns>A <see cref="TypeConverter.StandardValuesCollection"/>  that holds a standard set of valid values </returns>
    public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
    {
      return new StandardValuesCollection(Enum.GetNames(typeof(EnumType)));
    }
  }
}