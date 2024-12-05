namespace Shared.Helper
{
    public static class Constants
    {
        // Constants Strings
        public const string PermissionAccess = "access";
        public const string PermissionDenied = "denied";
        public const string PermissionView = "view";

        // Logger
        public const string LoggerId = "LoggerUserId";
        public const string LoggerName = "LoggerUserName";
        public const string LoggerPhoto = "LoggerUserPhoto";

        // Botões
        public const string Close = "Fechar";
        public const string Save = "Gravar";

        public const string Active = "Ativo";
        public const string Inactive = "Inativo";

        // Sites 
        public const string SiteStatesIBGE = "https://servicodados.ibge.gov.br/api/v1/localidades/estados";
        public static string SiteCitiesIBGE(string Abbreviation)
        {
            return $"https://servicodados.ibge.gov.br/api/v1/localidades/estados/{Abbreviation}/municipios";
        }

        public static string ViaCep(string cep)
        {
            return $"https://viacep.com.br/ws/{cep}/json/";
        }
    }
}
