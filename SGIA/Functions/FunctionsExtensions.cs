using System;
using System.Collections.Generic;
using System.Text;

namespace Functions
{
    public static class FunctionsExtensions
    {
        public static string FormatCnpjCfp(this string CnpjCfp)
        {
            return FunctionsValidate.FormatCNPJCPF(CnpjCfp);
        }
    }
}