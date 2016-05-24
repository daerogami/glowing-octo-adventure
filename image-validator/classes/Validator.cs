using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace image_validator.classes
{
    public static class Validator
    {
        internal static void RunValidation(List<string> imagePaths, List<string> xmlPaths)
        {
            Dictionary<string, FilePairValidationInfo> adventureData = new Dictionary<string, FilePairValidationInfo>();
            foreach (string imagePath in imagePaths)
            {
                var fileName = Path.GetFileNameWithoutExtension(imagePath);
                var imageValidation = new FilePairValidationInfo(ValidateImage(imagePath));
                if (!adventureData.ContainsKey(fileName))
                {
                    adventureData.Add(fileName, imageValidation);
                }else{
                    throw new Exception("There's an image with a duplicate name. That shouldn't be possible.");
                }
            }

            foreach (string xmlPath in xmlPaths)
            {
                var fileName = Path.GetFileNameWithoutExtension(xmlPath);
                if (!adventureData.ContainsKey(fileName))
                {
                    adventureData.Add(fileName, new FilePairValidationInfo());
                }
                else
                {
                    adventureData[fileName].SetXMLValidity(ValidateXMLAgainstEXIF(xmlPath, adventureData[fileName].ExifData));
                }
            }
            WriteAdventure(adventureData);
        }

        private static EXIF ValidateImage(string imagePath)
        {
            try
            {
                EXIF exifData = new EXIF(imagePath);
                // Building the EXIF object should be enough for now, further EXIF validation can take place here.
                return exifData;
            }
            catch (Exception e)
            {
#if DEBUG
                Trace.WriteLine(e.Message + "\n" + e.StackTrace);
#else
                Console.WriteLine("ERROR: " + e.Message);
#endif
                return null;
            }
        }

        private static bool ValidateXMLAgainstEXIF(string xmlPath, EXIF matchingImageData)
        {
            // Here we will check if the XML file's data (image type) matches the EXIF data's
            XmlDocument xDoc = new XmlDocument();
            try
            {
                xDoc.LoadXml(xmlPath);
            }catch(Exception e)
            {
#if DEBUG
                Trace.WriteLine(e.Message + "\n" + e.StackTrace);
#else
                Console.WriteLine("ERROR: " + e.Message);
#endif
                return false;
            }
            return true;
        }

        private static void WriteAdventure(Dictionary<string, FilePairValidationInfo> adventureData)
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach(FilePairValidationInfo fpvi in adventureData.OrderBy(a => a.Key).Select(a=>a.Value))
            {
                stringBuilder.AppendLine(fpvi.GetAdventureValidation());
            }

            FileStream fileStream = new FileStream(Constants.BASE_PATH + "adventure.txt", FileMode.Create);
            byte[] data = stringBuilder.ToString().GetBytesFromString();
            fileStream.Write(data, 0, data.Length);
            fileStream.Close();
        }

        public static byte[] GetBytesFromString(this string str)
        {
            return Encoding.ASCII.GetBytes(str);
        }
    }
}
