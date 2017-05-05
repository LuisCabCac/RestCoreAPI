using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestCore.Models.Logger
{
    interface ICustomLogger
    {
        void Save(Exception ex);
    }
}
