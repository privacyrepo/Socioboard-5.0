﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SocioboardDataServices.Helper
{
    public class SessionFactory
    {
        public static NHibernate.ISessionFactory sFactory;
        /// <summary>
        /// initializes the session for database
        /// </summary>
        /// 
        //  private static ILog logger = LogManager.GetLogger(typeof(SessionFactory));

        public static string configfilepath { get; set; }

        public static void Init()
        {

            try
            {
                NHibernate.Cfg.Configuration config = new NHibernate.Cfg.Configuration();
                string path = string.Empty;
                string wanted_path = System.IO.Directory.GetCurrentDirectory();
                if (string.IsNullOrEmpty(configfilepath))
                {
                    path = wanted_path + "\\hibernate.cfg.xml";
                }
                else
                {
                    path = configfilepath;
                }
                config.Configure(path);
                config.AddAssembly(Assembly.GetExecutingAssembly());//adds all the embedded resources .hbm.xml
                sFactory = config.BuildSessionFactory();
            }
            catch (Exception ex)
            {
                throw ex;
                //Console.Write(ex.StackTrace);
                //logger.Error(ex.Message);
            }
        }



        /// <summary>
        /// checks wheteher the session already exists. if not then creates it
        /// </summary>
        /// <returns></returns>
        public static NHibernate.ISessionFactory GetSessionFactory()
        {
            if (sFactory == null)
            {
                Init();
            }
            return sFactory;

        }

        /// <summary>
        /// creates a database connection and opens up a session
        /// </summary>
        /// <returns></returns>
        public static NHibernate.ISession GetNewSession()
        {
            return GetSessionFactory().OpenSession();
        }



    }
}
