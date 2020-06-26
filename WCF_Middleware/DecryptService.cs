using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCF_Service;

namespace WCF_Middleware {
    class DecryptService {
        public static void DecryptAction(MSG message) {
            char[] alphabet = new char[26] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

            int length = message.data.Length / 2;

            for (int i = 0; i < length; i += 2) {
                string name = message.data[i].ToString();
                string content = message.data[i + 1].ToString();
                GenKeys(alphabet, content, "", alphabet.Length, 4);
            }

            /*Parallel.For(0, message.data.Length, i => {
                string name = message.data[0].ToString();
                string content = message.data[1].ToString();
                GenKeys(alphabet, content, "", alphabet.Length, 4);
            });
            return new MSG();*/
        }

        public static void GenKeys(char[] set, string content, string prefix, int n, int k) {
            if (k == 0) {
                string r = Decrypt(content, prefix);
                Console.WriteLine(r);
                return;
            }
            Parallel.For(0, n, i => {
                string newPrefix = prefix + set[i];
                GenKeys(set, content, newPrefix, n, k - 1);
            });
        }

        public static string Decrypt(string content, string key) {
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            byte[] contentBytes = encoding.GetBytes(content);
            byte[] keyBytes = encoding.GetBytes(key);
            byte[] resultBytes = new byte[contentBytes.Length];

            for (int i = 0; i < contentBytes.Length; i++) {
                resultBytes[i] = (byte)(contentBytes[i] ^ keyBytes[i % keyBytes.Length]);
            }

            return encoding.GetString(resultBytes);
        }
    }
}
