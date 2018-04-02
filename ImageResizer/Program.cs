using System;
using System.Collections.Generic;
using ImageSharp;
using System.IO;
using System.Reflection;

namespace ImageResizer
{
    class Program
    {
        static Assembly Assembly => Assembly.GetExecutingAssembly();

        private static void Resize(string imagePath, IEnumerable<Size> sizes, string folder)
        {
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            foreach (var size in sizes)
            {
                Console.WriteLine($"Resizing {imagePath} to {size.Width}x{size.Height}");
                var image = Image.Load(imagePath);
                var resizedImage = image.Resize(size);
                resizedImage.Save(Path.Combine(folder, $"logo{size.Width}x{size.Height}.png"));
            }
        }

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine($"Usage: {Assembly.GetName().Name} <image file>");
                return;
            }

            var imagePath = args[0];

            Console.WriteLine("Enter image size! Finishing on empty line.");

            string input;
            List<Size> sizes = new List<Size>();
            while ((input = Console.ReadLine()) != null)
            {
                var size = Convert.ToInt32(input);
                sizes.Add(new Size(size, size));
            }

            Console.WriteLine("Resizing starts.");
            Resize(imagePath, sizes, Path.Combine(Path.GetDirectoryName(imagePath), "iOS"));
            Console.WriteLine("Done.");
        }
    }
}
