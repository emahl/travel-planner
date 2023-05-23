namespace Domain.Exceptions;

public class MissingConfigurationSetting : Exception
{
    public MissingConfigurationSetting(string configKey)
        : base($"{configKey} was not found in configuration") { }
}
