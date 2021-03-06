﻿using BaseCore.Log;
using BaseCore.Model;
using BaseCore.Untity;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;
using Dapper;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLibrary
{
    public class StudentService : IStudentService
    {              
        public ConfigHelper _p=null;
          
        public StudentService(ConfigHelper p)
        {
            _p = p;
        }
        public int Insert(Student model)
        {           
            Addlist();
            string sql = @" insert into Student(Age,Name) values (@Age,@Name);";
            using (var conn = _p.GetConn())
            {
                return DbContext.Execute(conn, sql, model);
            }
        }
        public void Addlist()
        {
            List<Student> list = new List<Student>();

            //for (int i = 0; i <50000; i++)
            //{
            //    Student stnew = new Student();
            //    stnew.Age = i.ToString();
            //    stnew.Name = "张";
            //    list.Add(stnew);
            //}
            Task.Run(() => {
                InsertCSVE(1);
            });

            Task.Run(() => {
                InsertCSVE(3);
            });
            Task.Run(() => {
                InsertCSVE(2);
            });
            Task.Run(() => {
                InsertCSVE(6);
            });
        }



        public int InsertCSVE(int k)
        {
            //RowColum();
            //string sql = @" update   Student set Age=4 where id=17872123;";
            using (MySqlConnection conn = (MySqlConnection)_p.GetConn())
            {              
                var sqlTransaction = conn.BeginTransaction();
                DataTable dt = new DataTable
                {
                    TableName = "student"
                };
                dt.Columns.Add("name");
                dt.Columns.Add("age");

                for (int i = 1; i <= 5000; i++)
                {
                    dt.Rows.Add(new Object[] { k, k });
                }
                var sw = new Stopwatch();
                sw.Start();
                //conn.Execute(sql, null, sqlTransaction);
                var guid = Guid.NewGuid().ToString();
                CSVEx.ToCsv(dt, guid);
                CSVEx.BulkLoad(conn, dt, guid);
                sw.Stop();
                var p = sw.ElapsedMilliseconds;
                sqlTransaction.Commit();
                CSVEx.DeleteUpTemp("student"+ guid);
                return 1;
                //return DbContext.Execute(conn, sql, model);
            }
        }
        public int InsertCSVEs()
        {
            //RowColum();
            string sql = @" update   Student set Age=4 where id=17872123;";
            using (MySqlConnection conn = (MySqlConnection)_p.GetConn())
            {
                var sqlTransaction = conn.BeginTransaction();
                DataTable dt = new DataTable
                {
                    TableName = "student"
                };
                dt.Columns.Add("name");
                dt.Columns.Add("age");

                for (int i = 1; i <= 5000; i++)
                {
                    dt.Rows.Add(new Object[] { 2, 3 });
                }
                var sw = new Stopwatch();
                sw.Start();
                conn.Execute(sql, null, sqlTransaction);
                var guid = Guid.NewGuid().ToString();
                CSVEx.ToCsv(dt, guid);
                CSVEx.BulkLoad(conn, dt, guid);
                sw.Stop();
                var p = sw.ElapsedMilliseconds;
                sqlTransaction.Commit();
                return 1;
                //return DbContext.Execute(conn, sql, model);
            }
        }
        /// <summary>
        /// 效率也差次之
        /// </summary>
        /// <param name="list"></param>
        public void InsertTable(List<Student>list)
        {            
            Stopwatch sp = new Stopwatch();
            sp.Start();
            string sqlInsert = @" insert into Student(Age,Name) value (@Age,@Name);";
            DataTable dt = new DataTable();
            dt.Columns.Add("Age", typeof(string));
            dt.Columns.Add("Name", typeof(string));
            using (var mConnection = (MySqlConnection)_p.GetConn())
            {
                MySqlTransaction transaction = mConnection.BeginTransaction();
                var mySqlDataAdapter = new MySqlDataAdapter();
                mySqlDataAdapter.InsertCommand = new MySqlCommand(sqlInsert, mConnection);
                mySqlDataAdapter.InsertCommand.Parameters.Add("@Age", MySqlDbType.String, 20, "Age");
                mySqlDataAdapter.InsertCommand.Parameters.Add("@Name", MySqlDbType.String, 20, "Name");
                mySqlDataAdapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
                foreach (var item in list)
                {
                    DataRow row = dt.NewRow();
                    row["Age"] = item.Age;
                    row["Name"] = item.Name;
                    dt.Rows.Add(row);
                }
                mySqlDataAdapter.UpdateBatchSize = 10000;
                mySqlDataAdapter.Update(dt);
                transaction.Commit();
                sp.Stop();
                LoggerManager.Info("时间监控："+sp.ElapsedMilliseconds+ "毫秒");
            }           
        }
        /// <summary>
        /// 效率最差
        /// </summary>
        /// <param name="list"></param>
        public void InsertDapper(List<Student> list)
        {
            Stopwatch sp = new Stopwatch();
            sp.Start();
            using (IDbConnection conn =_p.GetConn())
            {
                using (var transaction = conn.BeginTransaction())
                {                    
                    var strSql = new StringBuilder();                 
                    try
                    {

                        strSql.Append(" insert into Student(Age,Name) value (@Age,@Name);");
                        conn.Execute(strSql.ToString(), list, transaction);
                        transaction.Commit();                       
                    }
                    catch (Exception ex)
                    {

                    }                  
                }
            }
            sp.Stop();
            LoggerManager.Info("时间监控：" + sp.ElapsedMilliseconds + "毫秒");
        }
        /// <summary>
        /// 效率最高
        /// </summary>
        /// <param name="list"></param>
        public void InsertBuilder(List<Student> list)
        {
            var strSql = new StringBuilder();          
            strSql.Append("insert into Student(Age,Name) value ");
            foreach (var item in list)
            {
                strSql.AppendFormat("('{0}','{1}'),", item.Age, item.Name);          
            }
            var sql = strSql.ToString();
            sql = sql.Substring(0, sql.Length - 1);
            Stopwatch sp = new Stopwatch();
            sp.Start();
            using (IDbConnection conn = _p.GetConn())
            {
                using (var transaction = conn.BeginTransaction())
                {                    
                    try
                    {                      
                        conn.Execute(sql, null, transaction);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            sp.Stop();
            LoggerManager.Info("时间监控：" + sp.ElapsedMilliseconds + "毫秒");
        }
    }
}
