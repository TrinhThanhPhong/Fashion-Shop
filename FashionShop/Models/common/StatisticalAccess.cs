using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FashionShop.Models.common
{
    public class StatisticalAccess
    {
        public static string strConnect = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
        //public long Today { get; set; }
        //public long Yesterday { get; set; }
        //public long ThisWeek { get; set; }
        //public long LastWeek { get; set; }
        //public long ThisMonth { get; set; }
        //public long LastMonth { get; set; }
        //public long Total { get; set; }
        public static StatisticalViewModel Statistical()
        {
            using (var connect = new SqlConnection(strConnect))
            {
                var item = connect.QueryFirstOrDefault<StatisticalViewModel>("sp_Statistical", commandType: CommandType.StoredProcedure);
                return item;
            }
        }
    }
}