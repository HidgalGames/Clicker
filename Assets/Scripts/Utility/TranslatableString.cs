using System.Collections.Generic;
using UnityEngine;

public enum Languages
{
    EN,
    RU
}

[CreateAssetMenu(fileName = "New String", menuName = "Translatable String")]
[System.Serializable]
public class TranslatableString : ScriptableObject
{
    public List<string> strings = new List<string>();

#if UNITY_EDITOR
    public bool bigText = false;
#endif

    public static implicit operator string(TranslatableString translatableString)
    {
        int languageId = PlayerPrefs.GetInt("language_id");
        if (translatableString.strings[languageId] != null && translatableString.strings[languageId] != string.Empty)
        {
            return translatableString.strings[languageId];
        }
        else if (translatableString.strings[(int)Languages.EN] != null && translatableString.strings[(int)Languages.EN] != string.Empty)
        {
            return translatableString.strings[(int)Languages.EN];
        }

        translatableString.strings[(int)Languages.EN] = "";
        return translatableString.strings[(int)Languages.EN];
    }
}
