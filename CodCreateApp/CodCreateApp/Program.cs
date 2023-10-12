using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace CodCreateApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var close = true;
            Console.WriteLine(Constants.Messages.Preference);
            string selectAction = Console.ReadLine();

            while (close)
            {
                if (selectAction == "1")
                {
                    var codes = CodeCreateAlgorithm(10);
                    foreach (var code in codes)
                    {
                        Console.WriteLine(code);
                    }
                }
                else if (selectAction == "2")
                {
                    Console.WriteLine(Constants.Messages.Control);
                    var code = Console.ReadLine();
                    var isPassable = !IsPassable(code);
                    if (isPassable == true)
                    {
                        Console.WriteLine(Constants.Messages.ControlTrue);
                    }
                    else
                    {
                        Console.WriteLine(Constants.Messages.ControlFalse);
                    }
                }
                else
                {
                    Console.WriteLine(Constants.Messages.Error);
                }

                Console.WriteLine(Constants.Messages.Close);

                var q = Console.ReadLine();
                if (q.ToUpper() == "N")
                {
                    Console.WriteLine(Constants.Messages.Preference);
                    selectAction = Console.ReadLine();
                }
                else
                {
                    close = false;
                }
            }
        }

        public static bool IsThere(string NewCode, List<string> CreateCodes)
        {
            var likeCode = CreateCodes.Where(x => x == NewCode).FirstOrDefault();

            if (likeCode == null)
            {
                return IsPassable(NewCode);
            }
            else
            {
                return false;
            }
        }

        public static bool IsPassable(string Code)
        {
            int leng = Code.Length;
            if (leng != 8)
            {
                return true;
            }

            var splidValid = "ACDEFGHKLMNPRTXYZ234579".ToCharArray();

            var splidCode = Code.ToUpper().ToCharArray();

            foreach (var chr in splidCode)
            {
                var has = splidValid.Where(s => s == chr).FirstOrDefault();
                if (has == null)
                {
                    return true;
                }

            }
            return false;
        }

        public static List<string> CodeCreateAlgorithm(int count)
        {
            var length = 8;
            List<string> codes = new List<string>();

            for (int i = 0; i < count; i++)
            {
                string codess = "ACDEFGHKLMNPRTXYZ234579";
                StringBuilder sb = new StringBuilder();
                using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
                {
                    byte[] uintBuffer = new byte[sizeof(uint)];
                    while (length-- > 0)
                    {
                        rng.GetBytes(uintBuffer);

                        uint num = BitConverter.ToUInt32(uintBuffer, 0);

                        sb.Append(codess[(int)(num % (uint)codess.Length)]);
                    }
                }

                var code = sb.ToString();
                while (IsThere(code, codes))
                {
                    length = 8;
                    sb = new StringBuilder();
                    using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
                    {
                        byte[] uintBuffer = new byte[sizeof(uint)];
                        while (length-- > 0)
                        {
                            rng.GetBytes(uintBuffer);
                            uint num = BitConverter.ToUInt32(uintBuffer, 0);
                            sb.Append(codess[(int)(num % (uint)codess.Length)]);
                        }
                        code = sb.ToString();
                    }
                };
                codes.Add(code);
            }
            return codes;
        }
    }
}

public static class Constants
{
    public class Messages
    {
        public static readonly string Preference = $"8 Haneli Kodlar oluşturmak için 1 / Kod kontrolü yapmak için 2 yazınız: ";
        public static readonly string Error = $"Hatalı bir giriş yaptınız.";
        public static readonly string Close = $"Uygulamayı kapatmak istiyor musunuz? (Y/N)";
        public static readonly string Control = $"Kontrolünü yapmak istediğiniz kodu yazınız: ";
        public static readonly string ControlTrue = $"Kod doğru bu Kod Daha Önce oluşturdunuz.: ";
        public static readonly string ControlFalse = $"Kod yanlış, Tekrar deneyiniz.";
    }
}