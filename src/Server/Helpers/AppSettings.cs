namespace HiddenLove.Server.Helpers
{
    /// <summary>
    /// Classe contenant les paramètres globaux de l'application
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// Clef secrete de génération des JSON Web Token
        /// </summary>
        public string Secret { get; set; }
    }
}