public class Damage : Stat
{
    public TranslatableString Name { get; set; }
    public int Level { get; set; }
    public float Value { get; set; }

    public override float CalculateValue()
    {
        Value = 1 + ((Level - 1) * 3);
        return Value;
    }
}
