using System;
using System.IO;

namespace Trilogic
{
    public class AppSettings
    {
        public static string DefaultSettingPath = "AppSetting.ini";
        public string DBHost { get; set; }
        public string DBUser { get; set; }
        public string DBPassword { get; set; }
        public string DBName { get; set; }
        public string DirectoryPath { get; set; }

        public AppSettings()
        {

        }

        public void ReadFromFile()
        {
            this.ReadFromFile(AppSettings.DefaultSettingPath);
        }

        public void ReadFromFile(string path)
        {
            using (FileStream file = File.OpenRead(path))
            {
                TextReader reader = new StreamReader(file);
                for(;;)
                {
                    string read = reader.ReadLine();
                    if (read == null)
                    {
                        break;
                    }

                    string[] split = read.Split('=');
                    if (split.Length <= 1)
                    {
                        continue;
                    }

                    switch (split[0])
                    {
                        case "dbhost":
                            this.DBHost = split[1];
                            break;
                        
                        case "dbname":
                            this.DBName = split[1];
                            break;
                        
                        case "dbuser":
                            this.DBUser = split[1];
                            break;
                        
                        case "dbpassword":
                            this.DBPassword = split[1];
                            break;
                        
                        case "directory":
                            this.DirectoryPath = split[1];
                            break;

                        default:
                            break;
                    }
                }
            }
        }

        public void WriteToFile()
        {
            this.WriteToFile(AppSettings.DefaultSettingPath);
        }

        public void WriteToFile(string path)
        {
            using (FileStream file = File.OpenWrite(path))
            {
                TextWriter writer = new StreamWriter(file);
                writer.WriteLine(string.Concat("dbhost=", this.DBHost));
                writer.WriteLine(string.Concat("dbuser=", this.DBUser));
                writer.WriteLine(string.Concat("dbassword=", this.DBPassword));
                writer.WriteLine(string.Concat("dbname=", this.DBName));
                writer.WriteLine(string.Concat("directory=", this.DirectoryPath));
                writer.Dispose();
            }
            
        }
    }
}

