namespace Utils
{
    public static class Utilities
    {
        public static int GetSign(int value)
        {
            if(value < 0)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }
    }
}
