public enum StatType
{
    AtackSpeed,
    Damage
}

public abstract class Stat
{
    TranslatableString Name { get; set; }
    int Level { get; set; }

    float Value { get; set; }

    public virtual float CalculateValue()
    {
        Value += 1;
        return Value;
    }

    public virtual void UpgradeStat(int levelsToAdd)
    {
        Level += levelsToAdd;
        CalculateValue();
    }
}
