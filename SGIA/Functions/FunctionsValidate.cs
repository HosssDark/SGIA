using System.Text.RegularExpressions;

namespace Functions
{
    public static class FunctionsValidate
    {
        public static bool ValidateEmail(string Email)
        {
            var Regex = new Regex(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");

            if (Regex.IsMatch(Email))
                return true;
            else

                return false;
        }
    }
}