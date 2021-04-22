using UnityEngine;

public class LanguageChanger : MonoBehaviour
{
    public Languages GameLanguage = Languages.EN;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("language_id", (int)GameLanguage); // Test Only
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetInt("language_id", (int)GameLanguage);
    }
}
