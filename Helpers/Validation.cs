namespace BookShop.Helpers
{
    public class Validation
    {
        public static bool VALIDATE(List<string> INPUTS)
        {
            var valid = false;
            foreach (var input in INPUTS)
            {
                if (string.IsNullOrWhiteSpace(input))
                {
                    valid = false;
                    break;
                }
                else
                {
                    valid = true;
                }
            }
            return valid;
        }
    }
}