using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.IO.Compression;


namespace GraphAlgorithms
{
    class FileManager
    {

      

       

        public static string PATH = "data.txt";

        public static int GetNumberOfNodes(string[] s)
        {
            int number = 0;
            foreach (var line in s)
            {
                if (!line.Contains(','))
                {
                    number++;
                }
                else
                {
                    break;
                }
            }
            return number;
        }

        public static List<Edge> GetEdges(string[] s, int V)
        {
            List<Edge> Edges = new List<Edge>();

            for (int i = V; i < s.Length; i++)
            {
                Edges.Add(GetEdge(s[i]));
            }

            return Edges;
        }

        public static Edge GetEdge(string e)
        {
            e = e.Replace("n", string.Empty);
            string[] s = e.Split(",");

            return new Edge(Int32.Parse(s[0]) - 1, Int32.Parse(s[1]) - 1, Int32.Parse(s[2]));
        }

        public static bool FileExists()
        {
            return File.Exists(PATH);
        }

        //public void CreateFile(string s)
        //{

        //    HuffmanTree.Build(s);
        //    BitArray encoded = HuffmanTree.Encode(s);
        //    byte[] resultBytes = new byte[(encoded.Length - 1) / 8 + 1];
        //    encoded.CopyTo(resultBytes, 0);
        //    File.WriteAllBytes(PATH, resultBytes);
        //}

        //public string ReadCompressedFile()
        //{
        //    byte[] lines3 = File.ReadAllBytes(PATH);
        //    BitArray encoded2 = new BitArray(lines3);
        //    return HuffmanTree.Decode(encoded2);
        //}

        public static string CompressString(string text)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(text);
            var memoryStream = new MemoryStream();
            using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Compress, true))
            {
                gZipStream.Write(buffer, 0, buffer.Length);
            }

            memoryStream.Position = 0;

            var compressedData = new byte[memoryStream.Length];
            memoryStream.Read(compressedData, 0, compressedData.Length);

            var gZipBuffer = new byte[compressedData.Length + 4];
            Buffer.BlockCopy(compressedData, 0, gZipBuffer, 4, compressedData.Length);
            Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gZipBuffer, 0, 4);
            return Convert.ToBase64String(gZipBuffer);
        }

        public static void SaveFile(string data)
        {
            data = CompressString(data);
            File.WriteAllText(FileManager.PATH, data);
        }

        public static string DecompressString(string compressedText)
        {
            byte[] gZipBuffer = Convert.FromBase64String(compressedText);
            using (var memoryStream = new MemoryStream())
            {
                int dataLength = BitConverter.ToInt32(gZipBuffer, 0);
                memoryStream.Write(gZipBuffer, 4, gZipBuffer.Length - 4);

                var buffer = new byte[dataLength];

                memoryStream.Position = 0;
                using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
                {
                    gZipStream.Read(buffer, 0, buffer.Length);
                }

                return Encoding.UTF8.GetString(buffer);
            }
        }

        public static string GetData()
        {
            string decoded = File.ReadAllText(FileManager.PATH);
            return FileManager.DecompressString(decoded);
        }


    }
}
