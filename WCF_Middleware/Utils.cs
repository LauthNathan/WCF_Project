using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WCF_Service;

namespace WCF_Middleware {
    public static class Utils {
        public static bool FOUND_SECRET = false;
        public static string SECRET_CONTENT = "";
        public static string SECRET_FILENAME = "";
        public static string SECRET_KEY = "";
        public static string SECRET_CONFIDENCE = "";

        /// <summary>
        /// Transform a MSG object in JEEService msg object to make it understandable by JEE
        /// </summary>
        /// <param name="msg">The MSG object sent by the client</param>
        /// <returns>The JEEService msg object</returns>
        public static JEEService.msg ToJEEMessage(MSG msg) {
            JEEService.msg res = new JEEService.msg();
            res.appVersion = msg.appVersion;
            res.data = msg.data;
            res.info = msg.info;
            res.operationName = msg.operationName;
            res.operationVersion = msg.operationVersion;
            res.statutOp = msg.statut_Op;
            res.tokenApp = msg.tokenApp;
            res.tokenUser = msg.tokenUser;
            return res;
        }
    }
}
