// <copyright file="AppSettings.cs" company="Etersoul">
// This code is part of Trilogic Data Project.
// </copyright>
// <author>William</author>
namespace Trilogic.Utility
{
    using System;
    using System.IO;

    /// <summary>
    /// App settings.
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// The default setting path.
        /// </summary>
        private static string defaultSettingPath = "AppSetting.ini";

        /// <summary>
        /// Initializes a new instance of the <see cref="Trilogic.Utility.AppSettings"/> class.
        /// </summary>
        public AppSettings()
        {
        }

        /// <summary>
        /// Gets the default setting path.
        /// </summary>
        /// <value>The default setting path.</value>
        public static string DefaultSettingPath
        {
            get
            {
                return defaultSettingPath;
            }
        }

        /// <summary>
        /// Gets or sets the DB host.
        /// </summary>
        /// <value>The DB host.</value>
        public string DBHost { get; set; }

        /// <summary>
        /// Gets or sets the DB user.
        /// </summary>
        /// <value>The DB user.</value>
        public string DBUser { get; set; }

        /// <summary>
        /// Gets or sets the DB password.
        /// </summary>
        /// <value>The DB password.</value>
        public string DBPassword { get; set; }

        /// <summary>
        /// Gets or sets the name of the DB.
        /// </summary>
        /// <value>The name of the DB.</value>
        public string DBName { get; set; }

        /// <summary>
        /// Gets or sets the directory path.
        /// </summary>
        /// <value>The directory path.</value>
        public string DirectoryPath { get; set; }

        /// <summary>
        /// Reads from file.
        /// </summary>
        public void ReadFromFile()
        {
            this.ReadFromFile(AppSettings.DefaultSettingPath);
        }

        /// <summary>
        /// Reads from file.
        /// </summary>
        /// <param name="path">The path.</param>
        public void ReadFromFile(string path)
        {
            using (FileStream file = File.OpenRead(path))
            {
                TextReader reader = new StreamReader(file);
                for (;;)
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

        /// <summary>
        /// Writes to file.
        /// </summary>
        public void WriteToFile()
        {
            this.WriteToFile(AppSettings.DefaultSettingPath);
        }

        /// <summary>
        /// Writes to file.
        /// </summary>
        /// <param name="path">The path.</param>
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