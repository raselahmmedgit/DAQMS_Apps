using System;
using System.Data;
using System.Runtime.Serialization;

namespace DAQMS.Domain
{
    [DataContract]
    public class EntityBase
    {
         
        public string OperationMode { set; get; }
        public bool IsValid{set;get;}        
       
       
    }
}
