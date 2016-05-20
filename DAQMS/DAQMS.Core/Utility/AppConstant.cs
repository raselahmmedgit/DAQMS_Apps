using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAQMS.Core.Utility
{
    public class AppConstant
    {
        public static class Paths
        {
            public const string TemporaryFileUploadPath = "~/UploadFile/Temporary";
            public const string DownloadFilePath = "~/Download/";
        }

        public static class RegularExpressions
        {
            public const string PhoneNumber = @"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}";
        }

        public static class CacheKey
        {
            public const string DefaultCacheLifeTimeInMinute = "DefaultCacheLifeTimeInMinute";
            public const string AllAppMenu = "AllAppMenu";
            public const string AllGender = "AllGender";
            public const string AllUserRole = "AllUserRole";
        }

        public static class Messages
        {
            public const string UnhandelledError = "We are facing some problem while processing the current request. Please try again later.";
            public const string UnknownError = "We are facing some problem while processing the current request. Please try again later.";
            public const string NotFound = "Requested object not found.";
            public const string LongDescription = "Could not insert long description.";
            public const string IdNotProvided = "Id is not provided.";
            public static string SaveSuccess = "Saved successfully.";
            public static string UpdateSuccess = "Updated successfully.";
            public static string DeleteSuccess = "Deleted successfully.";
        }
    }
}
