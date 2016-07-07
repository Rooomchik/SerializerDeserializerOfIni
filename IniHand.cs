using System;
using System.IO;
using System.Text;
using System.Collections;

namespace Ini
{
	public class IniStructure
	{
		private SortedList Categories = new SortedList();
		public IniStructure()
		{
			return; 
		}
		public bool AddCategory(string Name)
		{
			if (Name == "" | Categories.ContainsKey(Name))
				return false;
			if (Name.IndexOf('=') != -1
				| Name.IndexOf('[') != -1
				| Name.IndexOf(']') != -1) 
				return false;
			Categories.Add(Name, new SortedList());
			return true;
		}
		public string[] GetCategories()
		{
			string[] CatNames = new string[Categories.Count];
			IList KeyList = Categories.GetKeyList();
			int KeyCount = Categories.Count;
			for (int i = 0; i < KeyCount; i++)
			{
				CatNames[i] = KeyList[i].ToString();
			}
			return CatNames;
		}
		public string GetCategoryName(int Index)
		{
			if (Index < 0 | Index >= Categories.Count)
				return null;
			return Categories.GetKey(Index).ToString();
		}
		public bool AddValue(string CategoryName, string Key, string Value)
		{
			if (CategoryName == "" | Key == "")
				return false;
			if (Key.IndexOf('=') != -1
				| Key.IndexOf('[') != -1
				| Key.IndexOf(']') != -1	
				| Key.IndexOf(';') != -1
				| Key.IndexOf('#') != -1
				)
				return false;
			if (!Categories.ContainsKey(CategoryName))
				return false;
			SortedList Category = (SortedList)(Categories[CategoryName]);
			if (Category.ContainsKey(Key))
				return false;
			Category.Add(Key, Value);
			return true;
		}
		public string GetValue(string CategoryName, string Key)
		{
			if (CategoryName == "" | Key == "")
				return null;
			if (!Categories.ContainsKey(CategoryName))
				return null;
			SortedList Category = (SortedList)(Categories[CategoryName]);
			if (!Category.ContainsKey(Key))
				return null;
			return Category[Key].ToString();
		}
		public string GetValue(int CatIndex, int KeyIndex)
		{
			if (CatIndex < 0 | KeyIndex < 0
				|CatIndex >= Categories.Count)
				return null;
			SortedList Category = (SortedList)(Categories.GetByIndex(CatIndex));
			if (KeyIndex >= Category.Count)
				return null;
			return Category.GetByIndex(KeyIndex).ToString();
		}
		public string GetKeyName(int CatIndex, int KeyIndex)
		{
			if (CatIndex < 0 | KeyIndex < 0
				|CatIndex >= Categories.Count)
				return null;
			SortedList Category = (SortedList)(Categories.GetByIndex(CatIndex));
			if (KeyIndex >= Category.Count)
				return null;
			return Category.GetKey(KeyIndex).ToString();
		}
		public string[] GetKeys(string CategoryName)
		{
			SortedList Category = (SortedList)(Categories[CategoryName]);
			if (Category == null)
				return null;
			int KeyCount = Category.Count;
			string[] KeyNames = new string[KeyCount];
			IList KeyList = Category.GetKeyList();
			for (int i = 0; i < KeyCount; i++)
			{
				KeyNames[i] = KeyList[i].ToString();
			}
			return KeyNames;
		}
		public static bool WriteIni(IniStructure IniData, string Filename)
		{
			string DataToWrite = CreateData(IniData);
			return WriteFile(Filename, DataToWrite);
		}
		private static bool WriteFile(string Filename, string Data)
		{	
			try
			{
				FileStream IniStream = new FileStream(Filename,FileMode.Create);
				if (!IniStream.CanWrite)
				{
					IniStream.Close();
					return false;
				}
				StreamWriter writer = new StreamWriter(IniStream);
				writer.Write(Data);
				writer.Flush();
				writer.Close();
				IniStream.Close();
				return true;
			}
			catch
			{
				return false;
			}
		}
		private static string CreateData(IniStructure IniData)
		{
			return CreateData(IniData,"");
		}
		private static string CreateData(IniStructure IniData, string comment)
		{	
			int CategoryCount = IniData.GetCategories().Length;
			int[] KeyCountPerCategory = new int[CategoryCount];
			string Data = comment;
			string[] temp = new string[2]; 
			
			for (int i = 0; i < CategoryCount; i++) 
			{
				string CategoryName = IniData.GetCategories()[i];
				KeyCountPerCategory[i] = IniData.GetKeys(CategoryName).Length;
			}

			for (int catcounter = 0; catcounter < CategoryCount; catcounter++)
			{
				Data += "\r\n[" + IniData.GetCategoryName(catcounter) + "]\r\n"; 
				for (int keycounter = 0; keycounter < KeyCountPerCategory[catcounter]; keycounter++)
				{
					temp[0] = IniData.GetKeyName(catcounter, keycounter);
					temp[1] = IniData.GetValue(catcounter, keycounter);
					Data += temp[0] + "=" + temp[1] + "\r\n";
				}
			}
			return Data;
		}
		public static IniStructure ReadIni(string Filename)
		{
			string Data = ReadFile(Filename);
			if (Data == null)
				return null;

			IniStructure data = InterpretIni(Data);
			
			return data;
		}
		public static IniStructure InterpretIni(string Data)
		{
			IniStructure IniData = new IniStructure();
			string[] Lines = RemoveAndVerifyIni(DivideToLines(Data));
			if (Lines == null)
				return null;

			if (IsLineACategoryDef(Lines[0]) != LineType.Category)
			{
				return null;
			}
			string CurrentCategory = "";
			foreach (string line in Lines)
			{
				switch (IsLineACategoryDef(line))
				{
					case LineType.Category:	
						string NewCat = line.Substring(1,line.Length - 2);
						IniData.AddCategory(NewCat); 
						CurrentCategory = NewCat;
						break;
					case LineType.NotACategory: 
						string[] keyvalue = GetDataFromLine(line);
						IniData.AddValue(CurrentCategory, keyvalue[0], keyvalue[1]);
						break;
					case LineType.Faulty: 
						return null;
				}
			}
			return IniData;
		}
		private static string ReadFile(string filename)
		{		
			if (!File.Exists(filename))
				return null;
			StringBuilder IniData;
			try
			{
				FileStream IniStream = new FileStream(filename,FileMode.Open,FileAccess.Read);
				if (!IniStream.CanRead)
				{
					IniStream.Close();
					return null;
				}
				StreamReader reader = new StreamReader(IniStream);
				IniData = new StringBuilder();
				IniData.Append(reader.ReadToEnd());
				reader.Close();
				IniStream.Close();
				return IniData.ToString();
			}
			catch
			{
				return null;
			}
		}
		private static string[] GetDataFromLine(string Line)
		{
			int EqualPos = 0;
			EqualPos = Line.IndexOf("=", 0);
			if (EqualPos < 1)
			{
				return null;
			}
			string LeftKey = Line.Substring(0, EqualPos);
			string RightValue = Line.Substring(EqualPos + 1);
			
			string[] ToReturn = {LeftKey, RightValue};
			return ToReturn;
		}
		private enum LineType 
		{
			NotACategory,
			Category,
			Faulty,
			Empty,
			Ok
		}
		private static LineType IsLineACategoryDef(string Line)
		{
			if (Line.Length < 3)
				return LineType.NotACategory; 
            
			if (Line.Substring(0,1) == "[" & Line.Substring(Line.Length - 1, 1) == "]")
			{
				if (Line.IndexOf("=") != -1) 
					return LineType.Faulty;
				if (ContainsMoreThanOne(Line,'[') | ContainsMoreThanOne(Line, ']'))
					return LineType.Faulty;
				return LineType.Category;
			}
			return LineType.NotACategory;
		}
		private static string[] DivideToLines(string Data)
		{	
			string[] Lines = new string[Data.Length];
			int oldnewlinepos = 0;
			int LineCounter = 0;
			for (int i = 0; i < Data.Length; i++)
			{
				if (Data.ToCharArray(i,1)[0] == '\n')
				{
					Lines[LineCounter] = Data.Substring(oldnewlinepos, i - oldnewlinepos - 1);
					oldnewlinepos = i + 1;
					LineCounter++;
				}
			}
			Lines[LineCounter] = Data.Substring(oldnewlinepos, Data.Length - oldnewlinepos);
			string[] LinesTrimmed = new string[LineCounter + 1];
			for (int i = 0; i < LineCounter + 1; i++)
			{
				LinesTrimmed[i] = Lines[i];
			}
			return LinesTrimmed;
		}
		private static bool ContainsMoreThanOne(string Data, char verify)
		{	
			char[] data = Data.ToCharArray();
			int count = 0;
			foreach (char c in data)
			{
				if (c == verify)
					count++;
			}
			if (count > 1)
				return true;
			return false;
		}
		private static LineType LineVerify(string line)
		{		
			if (line == "")
				return LineType.Empty;
			int equalindex = line.IndexOf('=');
			if (equalindex == 0)
				return LineType.Faulty; 

			if (equalindex != -1) 
			{
				if (line.IndexOf('[', 0, equalindex) != -1
					| line.IndexOf(']', 0, equalindex) != -1
					| line.IndexOf(';', 0, equalindex) != -1
                    | line.IndexOf('#', 0, equalindex) != -1)
					return LineType.Faulty;
       		}
			return LineType.Ok;
		}
		private static string[] RemoveAndVerifyIni(string[] Lines)
		{
			string[] temp = new string[Lines.Length];
			int TempCounter = 0; 
			foreach (string line in Lines)
			{
				switch (LineVerify(line))
				{
					case LineType.Faulty:
						return null;
					case LineType.Ok:	
						temp[TempCounter] = line;
						TempCounter++;
						break;
					case LineType.Empty:
						continue;
				}
			}
			string[] OKLines = new string[TempCounter];
			for (int i = 0; i < TempCounter; i++)
			{
				OKLines[i] = temp[i];
			}
			return OKLines;
		}
	}
}