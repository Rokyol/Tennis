namespace Tennis.Helpers
{
    public class TorneoValidation
    {
        public static bool IsPowerOfTwo(int number)
        {
            return (number > 0) && ((number & (number - 1)) == 0);
        }
        
    }
}
