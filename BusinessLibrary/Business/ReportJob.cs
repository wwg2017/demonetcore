using BaseCore.Log;
using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLibrary.Business
{
    public class ReportJob : IJob
    {      
        public async Task Execute(IJobExecutionContext context)
        {
            await Task.Run(() => { LoggerManager.Info("999"); });
        }
    }
}
