using ConsoleApplication1.Tests;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var connection =     System.Configuration.ConfigurationManager.ConnectionStrings["Test"].ConnectionString;

                Console.WriteLine(connection);                
                var retval = new DataTable();
                retval = Dal.GetADataTable(connection);

                if ((args.Count() > 1) && args.Contains("-input"))
                {
                    // run sql from file.
                    string fn = args[1];
                    TestStuff.createDatFilesFromSql(connection, fn);
                    Console.WriteLine(String.Join(", ", args));
                }
                else
                {
                    TestSerialize(retval);
                }

                //Data data = new Data();
                //data.MakeDataTables();

                //DataSet ds = data.dataSet;
                //ds.Tables[0].Rows[0].SetField(1, "FUHello Mercurial CI");
                //Console.WriteLine("serial in");

                // serialize
                //string set = "what the ddddd";
                //byte[] content = Serialize(set);
                //byte[] content = Serialize(ds);

                //File.WriteAllBytes(".\\out.ds2.dat", content);
                //byte[] dat = File.ReadAllBytes(".\\out.ds.dat");
                //DataSet ds3 = DeSerialize(dat);           

                //DataSet ds2 = DeSerialize(content);
                //string val = ds2.Tables[0].Rows[0][1].ToString();
                //Console.WriteLine("serial out {0}", val);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

        }

        static void TestSerialize(object src)
        {
            // out
            string fn = ".\\test.serial.dat";
            byte[] datOut = Serialize(src);
            File.WriteAllBytes(fn, datOut);

            // in
            byte[] datIn = File.ReadAllBytes(fn);
            object ds3 = DeSerialize(datIn);


        }

        //static DataSet DeSerialize(byte[] content)
        static object DeSerialize(byte[] content)
        {
            //var set = new DataSet();
            var set = new object();
            try
            {
                //var content = StringToBytes(s);
                var formatter = new BinaryFormatter();
                using (var ms = new MemoryStream(content))
                {
                    using (var ds = new DeflateStream(ms, CompressionMode.Decompress, true))
                    {
                        //set = (DataSet)formatter.Deserialize(ds);
                        set = (object)formatter.Deserialize(ds);
                        Console.WriteLine(string.Format("result({0})", set));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return set;
        }

        //static byte[] Serialize(string set)
        //static byte[] Serialize(DataSet set)
        static byte[] Serialize(object set)
        {
            byte[] content = null;

            try
            {
                var formatter = new BinaryFormatter();
                
                using (var ms = new MemoryStream())
                {
                    using (var ds = new DeflateStream(ms, CompressionMode.Compress, true))
                    {
                        //object set = null;
                        //string set = "what the fuck";
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
