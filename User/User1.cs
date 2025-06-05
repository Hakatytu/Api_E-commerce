using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace StoreApi2.User
{
    public class User1
    {
        public Guid Id { get; init; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string Cpf { get; private set; }
        public bool Active { get; private set; }
        
        public User1(string name, string email, string password, string cpf) 
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            Password = password;
            Cpf = cpf;
            Active = true;
        }
               

        public void UpdateUser1(string name, string email, string password, string cpf) 
        {
            Name = name;
            Email = email;
            Password = password;
            Cpf = cpf;

        }

        public void DeleteUser1()
        {
            Active = false;
        }

        
        public static bool ValidacaoCpf(string cpf)
    {

        string cpfLimpo = new string(cpf.Where(char.IsDigit).ToArray());


            if (cpfLimpo.Length != 11)
            {
                return false;
            }

        
        if (new string(cpfLimpo[0], 11) == cpfLimpo)
        {
            return false;
        }

        int[] multiplicadoresDV1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int somaDV1 = 0;

        for (int i = 0; i < 9; i++)
        {
            somaDV1 += (cpfLimpo[i] - '0') * multiplicadoresDV1[i];
        }

        int restoDV1 = somaDV1 % 11;
        int digitoVerificador1Calculado = (restoDV1 < 2) ? 0 : (11 - restoDV1);

        if (digitoVerificador1Calculado != (cpfLimpo[9] - '0'))
        {
            return false;
        }

        int[] multiplicadoresDV2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int somaDV2 = 0;

        for (int i = 0; i < 10; i++)
        {
            somaDV2 += (cpfLimpo[i] - '0') * multiplicadoresDV2[i];
        }

        int restoDV2 = somaDV2 % 11;
        int digitoVerificador2Calculado = (restoDV2 < 2) ? 0 : (11 - restoDV2);
        
        if (digitoVerificador2Calculado != (cpfLimpo[10] - '0'))
        {
            return false;
        }

        return true;
    }
    }
}
