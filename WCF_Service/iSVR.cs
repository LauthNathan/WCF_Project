using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace WCF_Service {

    [ServiceContract(
        Name = "IComposantService",
        Namespace = "http://project-exia.fr")]
    public interface iSVR {

        [OperationContract]
        MSG m_service(MSG message);

    }

    [DataContract]
    public class InputData {
        [DataMember]
        public string fileName { get; set; }
        [DataMember]
        public string fileContent { get; set; }
    }


    public struct MSG {
        public bool statut_Op { get; set; }
        public string info { get; set; }
        public InputData[] data { get; set; }
        public string operationName { get; set; }
        public string tokenApp { get; set; }
        public string tokenUser { get; set; }
        public string appVersion { get; set; }
        public string operationVersion { get; set; }
    }
}
