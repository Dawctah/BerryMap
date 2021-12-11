using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public static class Data
    {
        public static object LoadData<T, Ex>(object obj, string filePath, Action errorCatch)
            where Ex : Exception
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream fileStream = new FileStream(filePath, FileMode.Open);
                obj = (T)formatter.Deserialize(fileStream);
                fileStream.Close();
            }
            catch (Ex)
            {
                errorCatch?.Invoke();
            }

            return obj;
        }
    }
}
