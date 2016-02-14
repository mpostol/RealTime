using System;
using System.Runtime.InteropServices; 
namespace Utils
{
	/// <summary>
	/// 
	/// </summary>
	public class Conversions
	{
			[StructLayout(LayoutKind.Explicit)]
				public class Unia
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

			static public byte UshortHighByte(ushort wej)
			{
				/*kuku - wiem, ¿e g³upia nazwa ale jest to zmienna pomocnicza i nie wychodzi
				 * poza zasiêg tej klasy*/
				Unia kuku = new Unia();
				kuku.ushort_1=wej;
				return kuku.byte_2;
			}

			static public byte UshortLowByte(ushort wej)
			{
				Unia kuku = new Unia();
				kuku.ushort_1=wej;
				return kuku.byte_1;
			}

			static public byte ShortHighByte(short wej)
			{
				Unia kuku = new Unia();
				kuku.short_1=wej;
				return kuku.byte_2;
			}

			static public byte ShortLowByte(short wej)
			{
				Unia kuku = new Unia();
				kuku.short_1=wej;
				return kuku.byte_1;
			}

			static public void ArrayToUshort(byte[]tab,ref ushort wej)
			{
				Unia kuku = new Unia();
				kuku.byte_1=tab[0];
				kuku.byte_2=tab[1];
				wej=kuku.ushort_1;
			}

			static public void ArrayToUshort(byte[]tab, int index, ref ushort wej)
			{
				Unia kuku = new Unia();
				kuku.byte_1=tab[index];
				kuku.byte_2=tab[index+1];
				wej=kuku.ushort_1;
			}

			static public void ArrayToShort(byte[]tab,ref short wej)
			{
				Unia kuku = new Unia();
				kuku.byte_1=tab[0];
				kuku.byte_2=tab[1];
				wej=kuku.short_1;
			}

			static public void ArrayToShort(byte[]tab, int index, ref short wej)
			{
				Unia kuku = new Unia();
				kuku.byte_1=tab[index];
				kuku.byte_2=tab[index+1];
				wej=kuku.short_1;
			}

			static public void ArrayToUint(byte[]tab,ref uint wej)
			{
				Unia kuku = new Unia();
				kuku.byte_1=tab[0];
				kuku.byte_2=tab[1];
				kuku.byte_3=tab[2];
				kuku.byte_4=tab[3];
				wej=kuku.uint_1;
			}

			static public void ArrayToUint(byte[]tab, int index, ref uint wej)
			{
				Unia kuku = new Unia();
				kuku.byte_1=tab[index];
				kuku.byte_2=tab[index+1];
				kuku.byte_3=tab[index+2];
				kuku.byte_4=tab[index+3];
				wej=kuku.uint_1;
			}

			static public void ArrayToUintRevers(byte[]tab, int index, ref uint wej)
			{
				Unia kuku = new Unia();
				kuku.byte_4=tab[index];
				kuku.byte_3=tab[index+1];
				kuku.byte_2=tab[index+2];
				kuku.byte_1=tab[index+3];
				wej=kuku.uint_1;
			}
		
			static public void UintToArray(uint wej,byte[]tab)
			{
				Unia kuku = new Unia();
				kuku.uint_1=wej;
				tab[0]=kuku.byte_1;
				tab[1]=kuku.byte_2;
				tab[2]=kuku.byte_3;
				tab[3]=kuku.byte_4;
			}

			static public void UintToArray(uint wej, byte[]tab, int index)
			{
				Unia kuku = new Unia();
				kuku.uint_1=wej;
				tab[index]=kuku.byte_1;
				tab[index+1]=kuku.byte_2;
				tab[index+2]=kuku.byte_3;
				tab[index+3]=kuku.byte_4;
			}

			static public void UintToArrayRevers(uint wej, byte[]tab, int index)
			{
				Unia kuku = new Unia();
				kuku.uint_1=wej;
				tab[index]=kuku.byte_4;
				tab[index+1]=kuku.byte_3;
				tab[index+2]=kuku.byte_2;
				tab[index+3]=kuku.byte_1;
			}

			static public void ArrayToInt(byte[]tab,ref int wej)
			{
				Unia kuku = new Unia();
				kuku.byte_1=tab[0];
				kuku.byte_2=tab[1];
				kuku.byte_3=tab[2];
				kuku.byte_4=tab[3];
				wej=kuku.int_1;
			}

			static public void ArrayToInt(byte[]tab, int index, ref int wej)
			{
				Unia kuku = new Unia();
				kuku.byte_1=tab[index];
				kuku.byte_2=tab[index+1];
				kuku.byte_3=tab[index+2];
				kuku.byte_4=tab[index+3];
				wej=kuku.int_1;
			}

			static public void IntToArray(int wej,byte[]tab)
			{
				Unia kuku = new Unia();
				kuku.int_1=wej;
				tab[0]=kuku.byte_1;
				tab[1]=kuku.byte_2;
				tab[2]=kuku.byte_3;
				tab[3]=kuku.byte_4;
			}

			static public void IntToArray(int wej, byte[]tab, int index)
			{
				Unia kuku = new Unia();
				kuku.int_1=wej;
				tab[index]=kuku.byte_1;
				tab[index+1]=kuku.byte_2;
				tab[index+2]=kuku.byte_3;
				tab[index+3]=kuku.byte_4;
			}

		static public ushort BytesToUshort(byte HiByte, byte LoByte)
		{
			Unia kuku = new Unia();
			kuku.byte_1 = LoByte;
			kuku.byte_2 = HiByte;
			return kuku.ushort_1;
		}
		
		static public void ArrayToIntPtr(byte [] tab, ref IntPtr wsk)
		{
			Unia kuku = new Unia();
			kuku.byte_1=tab[0];
			kuku.byte_2=tab[1];
			kuku.byte_3=tab[2];
			kuku.byte_4=tab[3];
			wsk = kuku.wsk;
		}

		static public void ArrayToIntPtr(byte [] tab, ref IntPtr wsk, int index)
		{
			Unia kuku = new Unia();
			kuku.byte_1=tab[index];
			kuku.byte_2=tab[index+1];
			kuku.byte_3=tab[index+2];
			kuku.byte_4=tab[index+3];
			wsk = kuku.wsk;
		}
		
		public Conversions()
		{
			// 
			// TODO: Add constructor logic here
			//
		}
	}
}
