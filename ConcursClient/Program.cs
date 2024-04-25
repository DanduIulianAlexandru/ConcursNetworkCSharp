using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ConcursForms;
using ConcursNetwork.rpcprotocol;
using ConcursServices;

namespace ConcursClient {
static class Program {
    private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase
        .GetCurrentMethod()?.DeclaringType);
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main() {
        Run();
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        IConcursServices server = new ConcursServicesRpcProxy("127.0.0.1", 55555);
        ConcursClientCtrl ctrl = new ConcursClientCtrl(server);
        Form1 login = new Form1(ctrl);
        
        Application.Run(login);
    }
    
    private static void Run() {
        Uri configUri = new Uri("C:\\Users\\dandu\\OneDrive\\Desktop\\MPP\\a lot\\ConcursNetwork\\log4net.config");
        log4net.Config.XmlConfigurator.Configure(configUri);
        logger.Debug("Started program");
    }
}
}