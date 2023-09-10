namespace ShortLink.Script
{
    public static class GenerateCode
    {
        public static string GenCode()
        {
            Guid uniqueCode = Guid.NewGuid();
            string code = uniqueCode.ToString().Replace("-", "").Substring(0, 8);
            return code;
        }
    }
}
