using CloudHub.Domain.Exceptions;
using System.Net.Mail;

namespace CloudHub.API.Commons
{
    internal class ValidationUtils
    {

        internal static void Mandatory(object parameter, string name, bool checkIfEmptyString = true)
        {
            if (parameter == null) { throw new MissingParameterException(name); }
            if (checkIfEmptyString && string.IsNullOrWhiteSpace(parameter.ToString())) { throw new MissingParameterException(name); }
        }

        internal static void ValidEmail(string email, string name)
        {
            if (email == null) { return; }
            try
            {
                MailAddress mailAddress = new(email);
            }
            catch (FormatException)
            {
                throw new MissingParameterException($"Field error [{name}]: Invalid email");
            }
        }

        internal static void MinLength(string password, int minLength, string name)
        {
            if (password == null) { return; }
            if (password.Length < minLength) { throw new MissingParameterException($"Field error [{name}]: Minimum length = ({minLength})"); }
        }

        public static void ValidDateTime(string date, string name)
        {
            if (date == null) { return; }
            bool valid = DateTime.TryParse(date, out _);
            if (valid == false) { throw new MissingParameterException(name); }
        }

        public static void ValidDateOnly(string date, string name)
        {
            if (date == null) { return; }
            bool valid = DateOnly.TryParse(date, out _);
            if (valid == false) { throw new MissingParameterException(name); }
        }

        internal static void ValidEnumValue<T>(object value, string name)
        {
            if (Enum.IsDefined(typeof(T), value) == false) { throw new MissingParameterException(name); }
        }
    }
}
