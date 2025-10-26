using Microsoft.Extensions.Configuration;
using RaGae.Projects.RCC.Core;
using System.Configuration;
using System.Globalization;

namespace RaGae.Projects.RCC.Programmer
{
    internal static class Program
    {
        public static IConfiguration Configuration;

        private static Firmware firmware;
        private static DudeConfig dudeConfig;
        public static List<CubeColor> cubeColor;

        public static Firmware Firmware
        {
            get => firmware;
            set => firmware = value ?? throw new ArgumentNullException(nameof(value), StringResource.ErrorFirmwareConfigNullAssignment);
        }

        public static DudeConfig DudeConfig
        {
            get => dudeConfig;
            set => dudeConfig = value ?? throw new ArgumentNullException(nameof(value), StringResource.ErrorAVRDudeConfigNullAssignment);
        }

        public static List<CubeColor> CubeColor
        {
            get;
            set;
        }

        public static string TempPath
        {
            get;
            set;
        } = Path.Combine(Path.GetTempPath(), "RCC_" + Guid.NewGuid().ToString());

        public static string TempZIPPath
        {
            get;
            set;
        } = Path.Combine(TempPath, "firmware.zip");


        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();

            //var culture = CultureInfo.GetCultureInfo("en-US");
            //CultureInfo.DefaultThreadCurrentCulture = culture;
            //CultureInfo.DefaultThreadCurrentUICulture = culture;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.SetHighDpiMode(HighDpiMode.SystemAware);

            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            using (SplashScreen splash = new())
            {
                splash.ShowDialog();
                if (splash.DialogResult != DialogResult.OK)
                {
                    Application.Exit();
                    return;
                }
            }
            Application.Run(new FormMain());
        }
        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, StringResource.ErrorUnhandledThreadException);
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show((e.ExceptionObject as Exception)?.Message, StringResource.ErrorUnhandledUIException);
        }
    }
}