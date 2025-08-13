using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeadEndDynamicXpath.Database
{
    public interface Database
    {

        IDbConnection GetDbConnection();
        IDbCommand GetDbCommand(string commandText, IDbConnection connection);
    }
}
