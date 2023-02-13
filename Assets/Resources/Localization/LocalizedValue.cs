public readonly struct LocalizedValue
{
    public readonly string key;
    public readonly string value;
    public readonly LocalizationSystem.Language language;

    public LocalizedValue(string key, string value, LocalizationSystem.Language language)
    {
        this.key = key;
        this.value = value;
        this.language = language;
    }

    public override string ToString() => value;
}