﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDP.AdoNet.Interfaces
{
    public interface IRepositoryDisconnected<T>
    {
        DataSet Create(DataSet data, SqlDataAdapter adapter, T obj);
        DataSet Update(DataSet data, SqlDataAdapter adapter, T obj);
        DataSet Delete(DataSet data, SqlDataAdapter adapter, int id);
        DataSet GetAll(SqlDataAdapter adapter);
        void Save(SqlDataAdapter adapter, DataSet dataSet);
    }
}
