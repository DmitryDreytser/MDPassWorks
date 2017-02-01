using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;


namespace MDPassWorks
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] argv)
        {
            if (argv.Length == 0)
            {
                Console.WriteLine("MD password changer by Dmitry Dreytser, 2015");
                Console.WriteLine("Usage: {0} 1cv7.md [keys]", Path.GetFileName(Application.ExecutablePath));
                Console.WriteLine("Keys: ");
                Console.WriteLine("     -p:\"password\" - set new configuration password");
                Console.WriteLine("     -random - set new random configuration password");
                Console.WriteLine("     -et - extract tagstream, author logo and splash");
                Console.WriteLine("     -em - extract main metadata stream and global mdule");
                Console.WriteLine("     -em - extract main metadata stream and global mdule");
                Console.WriteLine("-------------------------------------------------------");
                Console.WriteLine("{0} 1cv7.md - remove configuration password", Path.GetFileName(Application.ExecutablePath));

                //Application.EnableVisualStyles();
                //Application.SetCompatibleTextRenderingDefault(false);
                //Application.Run(new Form1());
            }
            else
            {
               // Win32.AllocConsole();
                string mdfilename = argv[0];
                string password = string.Empty;
                bool extractTagXstream = false;
                bool extractMMS = false;

                foreach (string param in argv)
                {
                    if (param == mdfilename)
                        continue;

                    string[] parameter = {param};

                    if(param.Contains(":")) //параметры
                         parameter = param.Split(':');

                    string name = string.Empty;
                    string value = string.Empty;

                    if(parameter.Length == 2 )
                    {
                        name = parameter[0].ToLower();
                        value = parameter[1];
                    }
                    else
                        name = param;
                    
                    switch (name.ToLower())
                        {
                            case "-p":
                                {
                                    password = value;
                                    break;
                                }
                            case "-default":
                                {
                                    password = "deltal2011";
                                    break;
                                }
                            case "-random":
                                {
                                    password = randompassword();
                                    break;
                                }
                            case "-et":
                                {
                                    extractTagXstream = true;
                                    break;
                                }
                            case "-em":
                                {
                                    extractMMS = true;
                                    break;
                                }
                         }
                }

                Compound MD = new Compound(mdfilename);
                MD.SetPassword(password);

                Console.WriteLine("Конфигурация: {0}", mdfilename);
                Console.WriteLine("Автор: {0}", MD.AuthorFullName);
                Console.WriteLine("Защита: {0}", MD.IsEncrypted.ToString());

                if (MD.IsEncrypted)
                    Console.WriteLine("Установлен пароль: {0}", password);

                Console.WriteLine("CRC: {0}", MD.CRC);

                if (extractTagXstream)
                {
                    if (MD.AuthorLogo != null)
                        MD.AuthorLogo.Save("logo.bmp");
                    else
                        Console.WriteLine("No author logo found");

                    if (MD.AuthorSplash != null)
                        MD.AuthorSplash.Save("splash.bmp");
                    else
                        Console.WriteLine("No author splash found");

                    File.WriteAllText("TagStream.txt", MD.TagStream);
                }

                if (extractMMS)
                {
                    File.WriteAllText("Main Metadata Stream.txt",MD.MetaDataStream);
                    File.WriteAllText("Global module.txt", MD.GlobalModule);
                }

                MD.Save(mdfilename);
                MD = null;
                GC.WaitForPendingFinalizers();
                
            }

        }

        static string randompassword()
        {
            string password = Path.GetRandomFileName().Replace(".", "").Substring(0,10);
            return password;
        }
    }

    class Win32
    {
        /// <summary>
        /// Allocates a new console for current process.
        /// </summary>
        [DllImport("kernel32.dll")]
        public static extern Boolean AllocConsole();

        /// <summary>
        /// Frees the console.
        /// </summary>
        [DllImport("kernel32.dll")]
        public static extern Boolean FreeConsole();
    }
}
