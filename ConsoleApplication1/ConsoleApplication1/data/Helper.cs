using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace ConsoleApplication1.data
{
    static class Helper
    {
        public static byte[] Serialize(object set)
        {
            byte[] content = null;

            try
            {
                var formatter = new BinaryFormatter();

                using (var ms = new MemoryStream())
                {
                    using (var ds = new DeflateStream(ms, CompressionMode.Compress, true))
                    {
                        formatter.Serialize(ds, set);
                    }
                    ms.Position = 0;
                    content = ms.GetBuffer();

                    string result = System.Text.Encoding.UTF8.GetString(content);
                    //Console.WriteLine(string.Format("result({0})", result) );
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return content;

        }

    }
}
