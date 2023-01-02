using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace KTS_Testing_System.Extensions
{
    public static class ObjectExt
    {
        public static T1 CopyFrom<T1, T2>(this T1 destinationObj, T2 sourceObj)
            where T1 : class
            where T2 : class
        {

            if (sourceObj == null)
                return null;

            PropertyInfo[] srcFields = sourceObj.GetType().GetProperties(
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty);

            PropertyInfo[] destFields = destinationObj.GetType().GetProperties(
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty);

            foreach (var srcField in srcFields)
            {
                string property_name = srcField.Name;
                //if (srcField.Name == "party_code")
                //    property_name = "RECEIPT_TIMESTAMP";

                //PURPOSE OF THIS IF STATEMENT IS TO CHECK WHETHER PROPERTY TO BE COPIED IS A COLUMN OR CLASS / COLLECTION, IF COLUMN THEN CONTINUE WITH COPYING ELSE IF CLASS / COLLECTION THEN CONTINUE AND FETCH NEXT RECORD
                //if (srcField.Name == srcField.PropertyType.Name)
                if (srcField.Name.Contains(srcField.PropertyType.Name))
                    continue;

                var destField = destFields.FirstOrDefault(x => x.Name == property_name);

                //if (destField != null && destField.CanWrite && !destField.PropertyType.IsClass)
                if (destField != null && destField.CanWrite)
                {
                    if (destField.PropertyType == typeof(Nullable<DateTime>) || destField.PropertyType.Name == "DateTime")
                    {
                        if (srcField.PropertyType == typeof(string))
                        {
                            string srcTime = srcField.GetValue(sourceObj, null).ToString();
                            DateTime dt = DateTime.Parse(srcTime);
                            destField.SetValue(destinationObj, dt, null);
                        }
                        else
                        {
                            destField.SetValue(destinationObj, srcField.GetValue(sourceObj, null), null);
                        }
                    }
                    else
                    {
                        if (property_name == "Nearest_pv_code")
                        {
                            Console.WriteLine(srcField.GetValue(sourceObj, null));
                            Console.WriteLine(destField.GetValue(destinationObj, null));
                        }

                        destField.SetValue(destinationObj, srcField.GetValue(sourceObj, null), null);
                    }
                }
            }

            return destinationObj;
        }

    }
}
