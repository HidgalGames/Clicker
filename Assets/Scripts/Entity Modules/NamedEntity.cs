using UnityEngine;
using UnityEngine.UI;

public class NamedEntity : MonoBehaviour
{
    public bool IsUnknown = false;
    public TranslatableString EntityName;

    private Text textField;

    void Awake()
    {
        if(!this.TryGetComponent<Text>(out textField))
        {
            textField = this.GetComponentInChildren<Text>();
        }
    }

    void Start()
    {
        if (textField)
        {
            textField.text = GetEntityName();
        }
    }

    public string GetEntityName()
    {
        if (IsUnknown)
        {
            return "?????";
        }

        return EntityName;
    }
}
