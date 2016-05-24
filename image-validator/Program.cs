using image_validator.classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace image_validator
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> imagePaths;
            List<string> xmlPaths;

            imagePaths = Directory.EnumerateFiles(Constants.IMAGES_PATH).ToList();
            xmlPaths = Directory.EnumerateFiles(Constants.XMLS_PATH).ToList();

            Console.WriteLine("The following images were found:" + "\n\t" + string.Join("\n\t", imagePaths.ToArray()));
            Console.WriteLine("The following xml files were found:" + "\n\t" + string.Join("\n\t", xmlPaths.ToArray()));

            Console.WriteLine("\nRunning validation...");
            Validator.RunValidation(imagePaths, xmlPaths);

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}
