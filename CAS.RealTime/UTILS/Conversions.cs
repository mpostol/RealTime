//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using System.Runtime.InteropServices;

namespace Utils
{
  /// <summary>
  /// Converter class
  /// </summary>
  /// <remarks>use the <seealso cref="BitConverter"/> and <seealso cref="Convert"/> to provide conversion</remarks>
  [Obsolete("BitConverter.GetBytes")]
  public static class Conversions
  {
    [StructLayout(LayoutKind.Explicit)]
    private class Unia
    {
      [FieldOffset(0)]
      public byte byte_1;
      [FieldOffset(1)]
      public byte byte_2;
      [FieldOffset(2)]
      public byte byte_3;
      [FieldOffset(3)]
      public byte byte_4;
      [FieldOffset(0)]
      public short short_1;
      [FieldOffset(0)]
      public ushort ushort_1;
      [FieldOffset(0)]
      public int int_1;
      [FieldOffset(0)]
      public uint uint_1;
      [FieldOffset(0)]
      public IntPtr wsk;
    }

    /// <summary>
    /// Ushorts the high byte.
    /// </summary>
    /// <param name="wej">The wej.</param>
    /// <returns>System.Byte.</returns>
    public static byte UshortHighByte(ushort wej)
    {
      /*kuku - wiem, ¿e g³upia nazwa ale jest to zmienna pomocnicza i nie wychodzi
       * poza zasiêg tej klasy*/
      Unia kuku = new Unia
      {
        ushort_1 = wej
      };
      return kuku.byte_2;
    }
    /// <summary>
    /// Ushorts the low byte.
    /// </summary>
    /// <param name="wej">The wej.</param>
    /// <returns>System.Byte.</returns>
    public static byte UshortLowByte(ushort wej)
    {
      Unia kuku = new Unia
      {
        ushort_1 = wej
      };
      return kuku.byte_1;
    }
    /// <summary>
    /// Shorts the high byte.
    /// </summary>
    /// <param name="wej">The wej.</param>
    /// <returns>System.Byte.</returns>
    public static byte ShortHighByte(short wej)
    {
      Unia kuku = new Unia
      {
        short_1 = wej
      };
      return kuku.byte_2;
    }
    /// <summary>
    /// Shorts the low byte.
    /// </summary>
    /// <param name="wej">The wej.</param>
    /// <returns>System.Byte.</returns>
    public static byte ShortLowByte(short wej)
    {
      Unia kuku = new Unia
      {
        short_1 = wej
      };
      return kuku.byte_1;
    }
    /// <summary>
    /// Converts to ushort.
    /// </summary>
    /// <param name="tab">The tab.</param>
    /// <param name="wej">The wej.</param>
    public static void ArrayToUshort(byte[] tab, ref ushort wej)
    {
      Unia kuku = new Unia
      {
        byte_1 = tab[0],
        byte_2 = tab[1]
      };
      wej = kuku.ushort_1;
    }
    /// <summary>
    /// Converts to ushort.
    /// </summary>
    /// <param name="tab">The tab.</param>
    /// <param name="index">The index.</param>
    /// <param name="wej">The wej.</param>
    public static void ArrayToUshort(byte[] tab, int index, ref ushort wej)
    {
      Unia kuku = new Unia
      {
        byte_1 = tab[index],
        byte_2 = tab[index + 1]
      };
      wej = kuku.ushort_1;
    }
    /// <summary>
    /// Converts to short.
    /// </summary>
    /// <param name="tab">The tab.</param>
    /// <param name="wej">The wej.</param>
    public static void ArrayToShort(byte[] tab, ref short wej)
    {
      Unia kuku = new Unia
      {
        byte_1 = tab[0],
        byte_2 = tab[1]
      };
      wej = kuku.short_1;
    }
    /// <summary>
    /// Converts to short.
    /// </summary>
    /// <param name="tab">The tab.</param>
    /// <param name="index">The index.</param>
    /// <param name="wej">The wej.</param>
    public static void ArrayToShort(byte[] tab, int index, ref short wej)
    {
      Unia kuku = new Unia
      {
        byte_1 = tab[index],
        byte_2 = tab[index + 1]
      };
      wej = kuku.short_1;
    }
    /// <summary>
    /// Converts to uint.
    /// </summary>
    /// <param name="tab">The tab.</param>
    /// <param name="wej">The wej.</param>
    public static void ArrayToUint(byte[] tab, ref uint wej)
    {
      Unia kuku = new Unia
      {
        byte_1 = tab[0],
        byte_2 = tab[1],
        byte_3 = tab[2],
        byte_4 = tab[3]
      };
      wej = kuku.uint_1;
    }
    /// <summary>
    /// Converts to uint.
    /// </summary>
    /// <param name="tab">The tab.</param>
    /// <param name="index">The index.</param>
    /// <param name="wej">The wej.</param>
    public static void ArrayToUint(byte[] tab, int index, ref uint wej)
    {
      Unia kuku = new Unia
      {
        byte_1 = tab[index],
        byte_2 = tab[index + 1],
        byte_3 = tab[index + 2],
        byte_4 = tab[index + 3]
      };
      wej = kuku.uint_1;
    }
    /// <summary>
    /// Converts to uintrevers.
    /// </summary>
    /// <param name="tab">The tab.</param>
    /// <param name="index">The index.</param>
    /// <param name="wej">The wej.</param>
    public static void ArrayToUintRevers(byte[] tab, int index, ref uint wej)
    {
      Unia kuku = new Unia
      {
        byte_4 = tab[index],
        byte_3 = tab[index + 1],
        byte_2 = tab[index + 2],
        byte_1 = tab[index + 3]
      };
      wej = kuku.uint_1;
    }
    /// <summary>
    /// Converts to array.
    /// </summary>
    /// <param name="wej">The wej.</param>
    /// <param name="tab">The tab.</param>
    public static void UintToArray(uint wej, byte[] tab)
    {
      Unia kuku = new Unia
      {
        uint_1 = wej
      };
      tab[0] = kuku.byte_1;
      tab[1] = kuku.byte_2;
      tab[2] = kuku.byte_3;
      tab[3] = kuku.byte_4;
    }
    /// <summary>
    /// Converts to array.
    /// </summary>
    /// <param name="wej">The wej.</param>
    /// <param name="tab">The tab.</param>
    /// <param name="index">The index.</param>
    public static void UintToArray(uint wej, byte[] tab, int index)
    {
      Unia kuku = new Unia
      {
        uint_1 = wej
      };
      tab[index] = kuku.byte_1;
      tab[index + 1] = kuku.byte_2;
      tab[index + 2] = kuku.byte_3;
      tab[index + 3] = kuku.byte_4;
    }
    /// <summary>
    /// Converts to arrayrevers.
    /// </summary>
    /// <param name="wej">The wej.</param>
    /// <param name="tab">The tab.</param>
    /// <param name="index">The index.</param>
    public static void UintToArrayRevers(uint wej, byte[] tab, int index)
    {
      Unia kuku = new Unia
      {
        uint_1 = wej
      };
      tab[index] = kuku.byte_4;
      tab[index + 1] = kuku.byte_3;
      tab[index + 2] = kuku.byte_2;
      tab[index + 3] = kuku.byte_1;
    }
    /// <summary>
    /// Converts to int.
    /// </summary>
    /// <param name="tab">The tab.</param>
    /// <param name="wej">The wej.</param>
    public static void ArrayToInt(byte[] tab, ref int wej)
    {
      Unia kuku = new Unia
      {
        byte_1 = tab[0],
        byte_2 = tab[1],
        byte_3 = tab[2],
        byte_4 = tab[3]
      };
      wej = kuku.int_1;
    }
    /// <summary>
    /// Converts to int.
    /// </summary>
    /// <param name="tab">The tab.</param>
    /// <param name="index">The index.</param>
    /// <param name="wej">The wej.</param>
    public static void ArrayToInt(byte[] tab, int index, ref int wej)
    {
      Unia kuku = new Unia
      {
        byte_1 = tab[index],
        byte_2 = tab[index + 1],
        byte_3 = tab[index + 2],
        byte_4 = tab[index + 3]
      };
      wej = kuku.int_1;
    }
    /// <summary>
    /// Converts to array.
    /// </summary>
    /// <param name="wej">The wej.</param>
    /// <param name="tab">The tab.</param>
    public static void IntToArray(int wej, byte[] tab)
    {
      Unia kuku = new Unia
      {
        int_1 = wej
      };
      tab[0] = kuku.byte_1;
      tab[1] = kuku.byte_2;
      tab[2] = kuku.byte_3;
      tab[3] = kuku.byte_4;
    }
    /// <summary>
    /// Converts to array.
    /// </summary>
    /// <param name="wej">The wej.</param>
    /// <param name="tab">The tab.</param>
    /// <param name="index">The index.</param>
    public static void IntToArray(int wej, byte[] tab, int index)
    {
      Unia kuku = new Unia
      {
        int_1 = wej
      };
      tab[index] = kuku.byte_1;
      tab[index + 1] = kuku.byte_2;
      tab[index + 2] = kuku.byte_3;
      tab[index + 3] = kuku.byte_4;
    }
    /// <summary>
    /// Converts to ushort.
    /// </summary>
    /// <param name="HiByte">The hi byte.</param>
    /// <param name="LoByte">The lo byte.</param>
    /// <returns>System.UInt16.</returns>
    public static ushort BytesToUshort(byte HiByte, byte LoByte)
    {
      Unia kuku = new Unia
      {
        byte_1 = LoByte,
        byte_2 = HiByte
      };
      return kuku.ushort_1;
    }
    /// <summary>
    /// Converts to intptr.
    /// </summary>
    /// <param name="tab">The tab.</param>
    /// <param name="wsk">The WSK.</param>
    public static void ArrayToIntPtr(byte[] tab, ref IntPtr wsk)
    {
      Unia kuku = new Unia
      {
        byte_1 = tab[0],
        byte_2 = tab[1],
        byte_3 = tab[2],
        byte_4 = tab[3]
      };
      wsk = kuku.wsk;
    }
    /// <summary>
    /// Converts to intptr.
    /// </summary>
    /// <param name="tab">The tab.</param>
    /// <param name="wsk">The WSK.</param>
    /// <param name="index">The index.</param>
    public static void ArrayToIntPtr(byte[] tab, ref IntPtr wsk, int index)
    {
      Unia kuku = new Unia
      {
        byte_1 = tab[index],
        byte_2 = tab[index + 1],
        byte_3 = tab[index + 2],
        byte_4 = tab[index + 3]
      };
      wsk = kuku.wsk;
    }
  }
}
