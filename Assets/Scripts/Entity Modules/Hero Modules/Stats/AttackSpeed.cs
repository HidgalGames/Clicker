public class AttackSpeed : Stat
{
    public TranslatableString Name { get; set; }
    public int Level { get; set; }
    public float Value { get; set; }

    public override float CalculateValue()
    {
        Value = 0.5f + ((Level - 1) * 0.1f);
        return Value;
    }
}
