namespace OrderBase.Interfaces
{
    public interface IHasher
    {
        public (string,string) GenerateHash(string plainText);
        public bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt);

        //public bool IsMatch(string text, string salt, string hash);
    }
}
