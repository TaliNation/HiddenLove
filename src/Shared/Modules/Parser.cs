namespace HiddenLove.Shared.Modules
{
    public static class Parser
    {
        public static int? ToNullableInt(this string s)
        {
            int res;
            if (int.TryParse(s, out res)) 
                return res;

            return null;
        }

        public static int ToInt(this string s, int defaultValue = 0)
        {
            int res;
            if (int.TryParse(s, out res)) 
                return res;

            return defaultValue;
        }
    }
}