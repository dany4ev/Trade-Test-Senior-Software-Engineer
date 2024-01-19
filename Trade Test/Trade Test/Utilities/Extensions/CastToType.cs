namespace Trade_Test.Utilities.Extensions
{
    public static class CastToType
    {
        public static int AsInt(this object valueToConvert)
        {
            var result = 0;

            if (valueToConvert != null)
            {
                var valToConvert = valueToConvert.ToString();

                if (!string.IsNullOrEmpty(valToConvert))
                {
                    result = Convert.ToInt32(valueToConvert.ToString());
                }
            }

            return result;
        }

        public static Guid AsGuid(this object valueToConvert) =>
            valueToConvert != null ? Guid.Parse(valueToConvert.ToString()) : Guid.Empty;

        public static bool AsBoolean(this object valueToConvert) =>
           valueToConvert != null && valueToConvert.ToString().ToLower() == "true";
    }
}
