using System.Collections.Generic;
using CursedAmongUs.Modules;

namespace CursedAmongUs.Languages;

public static class Language
{
	public static string GetString(string s, Dictionary<string, string> replacementDic = null)
	{
		string str = ModTranslation.GetString(s);

		if (replacementDic != null)
		{
			foreach (var rd in replacementDic)
			{
				str = str.Replace(rd.Key, rd.Value);
			}
		}

		return str;
	}

	public static void Init()
	{
		ModTranslation.Load();
	}
}
