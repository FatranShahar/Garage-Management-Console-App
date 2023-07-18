using System;

public class ValueOutOfRangeException : Exception
{
    public float m_minValue { get; }
    public float m_maxValue { get; }
    public float m_actualValue { get; }
    public string m_valName { get; }
    public ValueOutOfRangeException(float minValue, float maxValue, float actValue, string name)
    {
        m_minValue = minValue;
        m_maxValue = maxValue;
        m_actualValue = actValue;
        m_valName = name;
    }
    public bool IsValidValue()
    {
        return (m_actualValue >= m_minValue && m_actualValue <= m_maxValue);
    }
    public string GetExceptionMessage()
    {
        return $"The value '{m_actualValue}' is out of range for the parameter '{m_valName}'. " +
               $"The valid range is [{m_minValue}, {m_maxValue}].";
    }
}