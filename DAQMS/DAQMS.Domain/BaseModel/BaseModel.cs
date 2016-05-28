using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DAQMS.Domain.Models
{
    public class BaseModel
    {
        public BaseModel()
        {
            this.InsertTimestamp = DateTime.Now;
            this.UpdateTimestamp = DateTime.Now;
            this.DeletedDate = DateTime.Now;
            this.IsDelete = false;
            this.error_code = 0;

        }

        //public int Id { get; set; }
         [DataMember, DataColumn(true)]
        public string InsertUser { get; set; }
         [DataMember, DataColumn(true)]
        public DateTime InsertTimestamp { get; set; }

         [DataMember, DataColumn(true)]
        public string UpdateUser { get; set; }
         [DataMember, DataColumn(true)]
        public DateTime UpdateTimestamp { get; set; }

        public Boolean IsDelete { get; set; }

        public string DeletedBy { get; set; }
        public DateTime DeletedDate { get; set; }
        public string LoginUserID { get; set; }

         [DataMember, DataColumn(true)]
        public Int32 error_code { get; set; }
         
        [DataMember, DataColumn(true)]
        public Int32 rowsl { get; set; }
        
        #region NotMapped

        [NotMapped]
        public virtual String ActionLink { get; set; }
        [NotMapped]
        public virtual Boolean HasCreate { get; set; }
        [NotMapped]
        public virtual Boolean HasUpdate { get; set; }
        [NotMapped]
        public virtual Boolean HasDelete { get; set; }
        [NotMapped]
        public virtual String MessageType { get; set; }
        [NotMapped]
        public virtual String Message { get; set; }

        #endregion

    }

    public class BaseNotMapModel
    {
        public BaseNotMapModel()
        {
            this.CreatedDate = DateTime.Now;
            this.UpdatedDate = DateTime.Now;
            this.DeletedDate = DateTime.Now;
            this.IsDelete = false;

        }

        //public int Id { get; set; }
        [NotMapped]
        public Int32 CreatedBy { get; set; }
        [NotMapped]
        public DateTime CreatedDate { get; set; }
        [NotMapped]
        public Int32 UpdatedBy { get; set; }
        [NotMapped]
        public DateTime UpdatedDate { get; set; }
        [NotMapped]
        public Boolean IsDelete { get; set; }
        [NotMapped]
        public Int32 DeletedBy { get; set; }
        [NotMapped]
        public DateTime DeletedDate { get; set; }

        #region NotMapped

        [NotMapped]
        public virtual String ActionLink { get; set; }
        [NotMapped]
        public virtual Boolean HasCreate { get; set; }
        [NotMapped]
        public virtual Boolean HasUpdate { get; set; }
        [NotMapped]
        public virtual Boolean HasDelete { get; set; }

        #endregion

    }
}
