namespace HiddenLove.Shared.Modules
{
    /// <summary>
    /// Conversion de chaine de caractères securisée
    /// </summary>
    public static class Parser
    {
        public static int? ToNullableInt(this string s)
        {
            if (int.TryParse(s, out int res))
                return res;

            return null;
        }

        /// <summary>Parsing en <see cref="int"/></summary>
        /// <param name="defaultValue">Valeur à retourner si la conversion échoue (0 par défaut)</param>
        public static int ToInt(this string s, int defaultValue = 0)
        {
            if (int.TryParse(s, out int res))
                return res;

            return defaultValue;
        }
    }
}