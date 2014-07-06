
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Pro.Interface;

namespace ClientProject
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get all dll files.
            const string extensionsPath = @"D:\DynamicLoadDLL_ModularApps\DynamicLoadDLL\Extensions";//will be replaced by your extension path.
            var pluginFiles = Directory.GetFiles(extensionsPath, "*.dll");
            
            // Load the assembly info.
            var loaders = (
                from file in pluginFiles
                let asm = Assembly.LoadFile(file)
                from type in asm.GetTypes()
                where typeof(ILoader).IsAssignableFrom(type)
                select (ILoader)Activator.CreateInstance(type)
                ).ToArray();
            // Call method of each loader
            foreach (var loader in loaders)
            {
                Console.WriteLine("{0}",loader.Message());
            }
            Console.ReadLine();
        }
    }
}
