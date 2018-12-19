using BaseCore.Model;
using BaseCore.Untity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

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
            string sql = @" insert into Student(Age,Name) values (@Age,@Name);";          
            return DbContext.Execute(_p.GetConn(),sql, model);
        }
    }
}
