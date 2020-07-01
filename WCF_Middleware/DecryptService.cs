using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using WCF_Service;

namespace WCF_Middleware {
    class DecryptService {
        public static JEEService.FileWebServiceClient svc = new JEEService.FileWebServiceClient();

        /// <summary>
        /// Sends all the decrypted files to the JEE Service
        /// </summary>
        public static bool DecryptAction(MSG message) {
            MSG msg = message;
            char[] alphabet = new char[26] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

            int length = message.data.Length / 2;

            for (int i = 0; i < length; i += 2) {
                if (Utils.FOUND_SECRET) break;
                string name = message.data[i].ToString();
                string content = message.data[i + 1].ToString();
                GenKeys(alphabet, content, "", alphabet.Length, 4, name, msg);
            }
            return Utils.FOUND_SECRET;
        }

        /// <summary>
        /// Generates the keys and calls JEE service (recursive function)
        /// </summary>
        public static void GenKeys(char[] set, string content, string key, int n, int k, string name, MSG msg) {
            if (Utils.FOUND_SECRET == false) {
                if (k == 0) {
                    if (Utils.FOUND_SECRET == false) {
                        string r = Decrypt(content, key);
                        JEEService.msg res = Utils.ToJEEMessage(msg);
                        System.Text.UnicodeEncoding encoding = new System.Text.UnicodeEncoding();
                        byte[] rr = encoding.GetBytes(r);
                        //sbyte[] x = (sbyte[])Array.ConvertAll(rr, b => unchecked((sbyte)b));
                        string rrr = "";
                        for (int i=0; i<rr.Length; i++) {
                            if (i==rr.Length-1) {
                                rrr += rr[i].ToString();
                            } else {
                                rrr += rr[i].ToString() + ",";
                            }
                        }


                        res.data = new object[] { name, rrr, key };
                    
                        try {
                            svc.fileCheck(res);    
                        } catch (FaultException ex) {
                            Console.WriteLine(ex);
                        }
                    }
                    
                    return;
                }
                Parallel.For(0, n, (i, state) => {
                    if (Utils.FOUND_SECRET) state.Break();
                    string newPrefix = key + set[i];
                    GenKeys(set, content, newPrefix, n, k - 1, name, msg);
                });
            }
        }


        /// <summary>
        /// Decrypts the file
        /// </summary>
        public static string Decrypt(string content, string key) {
            System.Text.UnicodeEncoding encoding = new System.Text.UnicodeEncoding();
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
