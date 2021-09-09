using System;

namespace FinancialManager
{
    public class ConfigurationNotFoundException : Exception
    {
        public ConfigurationNotFoundException() : base() { }
        public ConfigurationNotFoundException(string message) : base(message) { }
        public ConfigurationNotFoundException(string message, Exception innerException) : base() { }
    }
}
