using System.Collections.Generic;
using UnityEngine;

public enum Languages
{
	EN,
	RU
}

[System.Serializable]
public class TranslatableString
{
	public List<string> strings = new List<string>();

	public static implicit operator string(TranslatableString translatableString)
	{
		int languageId = PlayerPrefs.GetInt("language_id");
        if (translatableString.strings[languageId] != null)
        {
			return translatableString.strings[languageId];
		}
		else if(translatableString.strings[(int)Languages.EN] != null)
        {
			return translatableString.strings[(int)Languages.EN];
		}

		translatableString.strings[(int)Languages.EN] = "";
		return translatableString.strings[(int)Languages.EN];
	}
}
