namespace DomurTech.Messaging.Entities
{
    internal static class EmailConstants
    {
        public static string EmailKeyRegEx => @"\{\%[<" + EmailTokenName + @">]*\%\}";
        public static string EmailTokenName => "EmailTokenName";
    }
}
