using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace SWD392_PublicService.Services
{
    public class CaptchaService : ICaptchaService
    {
        private static readonly Random _random = new Random();

        public string GenerateCaptchaCode()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            char[] code = new char[6];
            for (int i = 0; i < code.Length; i++)
            {
                code[i] = chars[_random.Next(chars.Length)];
            }
            return new string(code);
        }

    }
}
