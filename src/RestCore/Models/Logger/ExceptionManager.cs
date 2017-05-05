using RestCore.Models.Config;
using System;
using System.Threading.Tasks;

namespace RestCore.Models.Logger
{
    //Try to use Open/Close Principle and Singleton Pattern
    public class ExceptionManager
    {
        private static ExceptionManager instance = null;
        private ICustomLogger logger;

        private ExceptionManager()
        {
            try
            {
                if (!string.IsNullOrEmpty(ConfigOptions.Logger))
                    logger = (ICustomLogger)Activator.CreateInstance(Type.GetType(ConfigOptions.Logger));
            }
            catch
            {
                //Need control
            }

            //Force to have some Logger
            if (logger == null)
                logger = new DBLogger();
        }

        public static ExceptionManager Instance
        {
            get
            {
                if (instance == null)
                {
                    if (instance == null)
                        instance = new ExceptionManager();
                }
                return instance;
            }
        }

        async public void ControlException(Exception ex)
        {
            //You decide if you want save or not...
            await Task.Run(() => logger.Save(ex)); 
        }

        public bool IsThrowable(Exception ex)
        {
            //Your decide when throw...
            return false;
        }
    }
}
