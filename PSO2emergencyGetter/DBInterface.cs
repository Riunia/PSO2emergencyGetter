using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;

namespace PSO2emergencyGetter
{
    interface IPostgreSQL
    {
        int connect();
        int disconnect();
        object command(string que);
        object ParmCommand(string que, List<NpgsqlParameter> param);

    }
}
