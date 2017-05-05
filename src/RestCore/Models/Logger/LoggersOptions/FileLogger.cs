using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestCore.Models.Logger
{
    public class FileLogger : ICustomLogger
    {
        void ICustomLogger.Save(Exception ex)
        {
            ///Save in File
        }

    }
}
