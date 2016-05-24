using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace image_validator.classes
{
    // Reference: http://www.media.mit.edu/pia/Research/deepview/exif.html
    public class EXIF
    {
        int size = 0;

        public EXIF(string filepath)
        {
            try
            {
                FindEXIFData(File.OpenRead(filepath));
            }catch(Exception e)
            {
#if DEBUG
                Trace.WriteLine(e.Message + "\n" + e.StackTrace);
#else
                Console.WriteLine("ERROR: " + e.Message);
#endif
            }
        }

        private void FindEXIFData(FileStream fileStream)
        {
            bool foundStart = false;
            bool foundStream = false;
            bool foundEnd = false;
            long fileSize = fileStream.Length;
            while (fileStream.Position < fileSize)
            {
                switch (fileStream.ReadByte())
                {
                    case (int)Marker.STARTOFIMAGE:
                        foundStart = true;
                        break;
                    case (int)Marker.ENDOFIMAGE:
                        foundEnd = true;
                        break;
                    case (int)Marker.STARTOFSTREAM:
                        foundStream = true;
                        break;
                    case (int)Marker.APP0:
                        // JFIF Data
                        break;
                    case (int)Marker.APP1:
                        // EXIF Data
                        PopulateEXIFData(fileStream);
                        break;
                    default:
                        // Non-marker value
                        break;
                }
            }
            if (!foundStart || !foundEnd || !foundStream)
            {
                throw new Exception("ERROR: File does not contain EXIF data.");
            }

        }

        private void PopulateEXIFData(FileStream fileStream)
        {
            try
            {
                size = fileStream.ReadByte();

                byte[] exifPayload = new byte[size];
                fileStream.Read(exifPayload, 0, size);
            }
            catch (Exception e)
            {
#if DEBUG
                Trace.WriteLine(e.Message + "\n" + e.StackTrace);
#else
                Console.WriteLine("ERROR: " + e.Message + "\nPress any key to exit.");
                Console.ReadKey();
                Environment.Exit(0);
#endif
            }
        }
    }
}
