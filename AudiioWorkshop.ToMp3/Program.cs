using NAudio.MediaFoundation;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudiioWorkshop.ToMp3
{
    class Program
    {
        static void Main(string[] args)
        {
            MediaFoundationApi.Startup();
            // StringBuilder sb = new StringBuilder();
            foreach (var arg in args)
            {
                try
                {
                    if (Path.GetExtension(arg).ToLower() == ".wav" && File.Exists(arg))
                    {
                        string wavFile = arg;
                        string mp3File = wavFile.Substring(0, wavFile.Length - 4) + ".mp3";
                        bool result = ToMp3(wavFile, mp3File);
                        if (result)
                        {
                            File.Delete(wavFile);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                // sb.Append(arg).Append(",");
            }
            // Console.WriteLine(sb.ToString());

            //File.WriteAllText("log.txt", sb.ToString());
        }

        private static bool ToMp3(string wavFile, string mp3File)
        {
            using (var reader = new WaveFileReader(wavFile))
            {
                try
                {
                    MediaFoundationEncoder.EncodeToMp3(reader, mp3File);
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }
    }
}
