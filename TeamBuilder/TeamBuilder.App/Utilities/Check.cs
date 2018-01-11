namespace TeamBuilder.App.Utilities
{
    using System;

    public static class Check
    {
        public static void CheckLength(int length, string[] array)
        {
            if (length != array.Length)
            {
                throw new FormatException(Constants.ErrorMessages.InvalidArgumentsCount);
            }
        }
    }
}
