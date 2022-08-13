namespace Infrastructure.Helpers
{
    public static class AddressExtensions
    {
        public static string[] Decompose(this string streetAddress)
        {
            var arr = streetAddress.Split(',').ToArray();
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = arr[i].Trim();
            }
            return arr;
        }
    }
}
