namespace HiddenLove.Server.Helpers
{
    /// <summary>
    /// Paramètres globaux de l'application
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// Clef de génération des JSON Web Token
        /// </summary>
        public string Secret { get; set; }
    }
}