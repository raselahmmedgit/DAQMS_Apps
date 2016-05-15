using System;
using System.Data;
using System.Reflection;

namespace DAQMS.Domain
{
    public class MapperBase
    {
        public static MapperBase objMaper;


        public static MapperBase GetInstance()
        {
            if (objMaper == null)
            {
                objMaper = new MapperBase();
                return objMaper;
            }
            else
            {
                return objMaper;
            }
        }


        public virtual EntityBase MapItem(EntityBase obj, DataRow drContent)
        {

            PropertyInfo[] properties = obj.GetType().GetProperties();

            foreach (PropertyInfo property in properties)
            {
                object[] attributes = property.GetCustomAttributes(typeof(DataColumn), true);

                if (attributes.GetLength(0) > 0)
                {

                    if (((DataColumn)attributes[0]).IsDataColumn())
                    {
                        if (property.PropertyType == typeof(System.String))
                        {
                            property.SetValue(obj, drContent[property.Name] as string, null);
                        }
                        else if (property.PropertyType == typeof(System.Int32))
                        {
                            property.SetValue(obj, drContent[property.Name] == DBNull.Value ? 0 : Convert.ToInt32(drContent[property.Name]), null);
                        }
                        else if (property.PropertyType == typeof(System.Boolean))
                        {
                            property.SetValue(obj, drContent[property.Name] == DBNull.Value ? false : Convert.ToBoolean(drContent[property.Name]), null);
                        }
                        else if (property.PropertyType == typeof(System.DateTime))
                        {
                            property.SetValue(obj, drContent[property.Name] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(drContent[property.Name]), null);
                        }

                    }

                }

            }

            return obj;

        }

    }
}
