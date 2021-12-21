using CloudHub.Domain.Exceptions;

namespace CloudHub.Domain.DTO
{
    internal class ValidationUtils
    {

        internal static void Mandatory(object parameter, string name, bool checkIfEmptyString = true)
        {
            if (parameter == null) { throw new MissingParameterException(name); }
            if (checkIfEmptyString && string.IsNullOrWhiteSpace(parameter.ToString())) { throw new MissingParameterException(name); }
        }

        public static void ValidDateTime(string date, string name)
        {
            if (date == null) { return; }
            bool valid = DateTime.TryParse(date, out _);
            if (valid == false) { throw new MissingParameterException(name); }
        }

        public static void ValidDateOnly(string date, string name)
        {
            if(date == null) { return; }
            bool valid = DateOnly.TryParse(date, out _);
            if (valid == false) { throw new MissingParameterException(name); }
        }

        internal static void ValidEnumValue<T>(object value, string name)
        {
            if (Enum.IsDefined(typeof(T), value) == false) { throw new MissingParameterException(name); }
        }
    }
}
