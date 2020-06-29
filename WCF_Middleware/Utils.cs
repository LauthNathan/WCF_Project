using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WCF_Service;

namespace WCF_Middleware {
    class Utils {
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
