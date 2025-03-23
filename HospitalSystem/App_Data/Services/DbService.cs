using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace HospitalSystem.Services
{
	public class DbService
	{
		private static DbService _instance;
		public readonly string _connectionString;

		private DbService()
		{
            var conn = ConfigurationManager.ConnectionStrings["InternSmallHospitalConnectionString"]; 
            if (conn == null)
            {
                throw new Exception("❌ Connection string 'InternSmallHospitalConnectionString' not found in web.config!");
            }
            _connectionString = conn.ConnectionString;
        }
		public static DbService Instance
		{
			get
			{
				if(_instance == null )
				{
					_instance = new DbService();
				}
				return _instance;
			}
		}
        public InternSmallHospitalConnectionString GetDbContext()
        {
            return new InternSmallHospitalConnectionString(); 
        }
    }

}
