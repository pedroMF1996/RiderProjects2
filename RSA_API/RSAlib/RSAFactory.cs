namespace RSAlib
{
    public static class RSAFactory
    {
        public static T CreateRSA<T>() where T:RSA,new()
        {
            return new T(); 
        }
    }
}