using System;
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

        public static string FormatCNPJCPF(string CnpjCpf)
        {
            CnpjCpf = WithoutMask(CnpjCpf);

            if (CnpjCpf.Length > 11)
                return CnpjCpf = FormatCNPJ(CnpjCpf);
            else
                return CnpjCpf = FormatCPF(CnpjCpf);
        }

        public static string FormatCNPJ(string CNPJ)
        {
            return Convert.ToUInt64(CNPJ).ToString(@"00\.000\.000\/0000\-00");
        }

        public static string FormatCPF(string CPF)
        {
            return Convert.ToUInt64(CPF).ToString(@"000\.000\.000\-00");
        }

        public static string WithoutMask(string Codigo)
        {
            return Codigo.Replace(".", string.Empty).Replace("-", string.Empty).Replace("/", string.Empty);
        }
    }
}