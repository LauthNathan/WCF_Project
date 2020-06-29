using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCF_Service;

namespace WCF_Middleware {
    class DecryptService {
        public static JEEService.FileWebServiceClient svc = new JEEService.FileWebServiceClient();

        /// <summary>
        /// Sends all the decrypted files to the JEE Service
        /// </summary>
        public static void DecryptAction(MSG message) {
            MSG msg = message;
            char[] alphabet = new char[26] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

            int length = message.data.Length / 2;

            for (int i = 0; i < length; i += 2) {
                if (Utils.FOUND_SECRET) break;
                string name = message.data[i].ToString();
                string content = message.data[i + 1].ToString();
                GenKeys(alphabet, content, "", alphabet.Length, 4, name, msg);
            }
        }

        /// <summary>
        /// Generates the keys and calls JEE service (recursive function)
        /// </summary>
        public static void GenKeys(char[] set, string content, string key, int n, int k, string name, MSG msg) {
            if (k == 0) {
                string r = Decrypt(content, key);
                JEEService.msg res = Utils.ToJEEMessage(msg);
                res.data = new object[] { name, content, key };
                svc.fileCheck(res);
                return;
            }
            Parallel.For(0, n, i => {
                string newPrefix = key + set[i];
                GenKeys(set, content, newPrefix, n, k - 1, name, msg);
            });
        }

        /// <summary>
        /// Decrypts the file
        /// </summary>
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
