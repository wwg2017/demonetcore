using BaseCore.Cache;
using BaseCore.ImageDo;
using BaseCore.Log;
using BaseCore.Model;
using BusinessLibrary.Business;
using Quartz.Impl;
using DemoNetCore;
//using log4net;
//using log4net.Config;
//using log4net.Core;
//using log4net.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.DrawingCore.Imaging;
using System.IO;
using System.Text;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;
using System.Collections.Specialized;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLibrary
{
    public class HomeController : Controller
    {       
        private  readonly IStudentService _studentsverVice;
        private readonly MemoryCacheHelper _cache;
        //private ILog log = LogManager.GetLogger(Startup.repository.Name, typeof(HomeController));
        public HomeController(IStudentService studentsverVice, MemoryCacheHelper cache)
        {            
            _studentsverVice = studentsverVice;
            _cache = cache;
        }
        public IActionResult Index()
         {


            Image();




            List<Demo> list = new List<Demo>();
            Demo de = new Demo();

            de.Id = 1;
            de.Name = "1";
            de.M=2;

            list.Add(de);
            Demo des = new Demo();
            des.Name = "2";
            des.Id = 2;
            des.M = 2;
            list.Add(des);
            Demo desd = new Demo();
            desd.Name = "1";
            desd.Id = 1;
            desd.M = 2;
            list.Add(desd);
            var kli = list.GroupBy(p => new { p.Id,p.Name} ).Select(k =>new Demo {Id= k.Key.Id,Name= k.Key.Name ,M= k.Sum(n=>n.M) }).ToList();






            var d = DateTime.Now.ToString("dd");

            List<int> klist = new List<int>();
     
            klist.Add(1); klist.Add(2);
            List<int> klists = new List<int>();
            var kp = klist.Where(p => p == 1 || p == 2).ToList().Count();
            //RedisHelper redis = new RedisHelper(1);
            //#region String
            //string str = "wwg123";
            //redis.StringSet("strKey", str);
            //var strget = redis.StringGet("strKey");
            //Demo demo = new Demo()
            //{
            //    Id = 1,
            //    Name = "123"
            //};
            //redis.StringSet("redis_string_model", demo);
            //var model = redis.StringGet<Demo>("redis_string_model");

            // for (int i = 0; i < 10; i++)
            //{
            //    redis.StringIncrement("StringIncrement", 2);
            //}
            //for (int i = 0; i < 10; i++)
            //{
            //    redis.StringDecrement("StringIncrement");
            //}
            //redis.StringSet("redis_string_model1", demo, TimeSpan.FromSeconds(10));//过期时间
            //#endregion

            #region List
            //for (int i = 0; i < 10; i++)
            //{
            //    redis.ListRightPush("list", i);
            //}
            //for (int i = 10; i < 20; i++)
            //{
            //    redis.ListLeftPush("list", i);
            //}
            //var length = redis.ListLength("list");
            //var leftpop = redis.ListLeftPop<string>("list");
            //var rightPop = redis.ListRightPop<string>("list");

            //var list = redis.ListRange<int>("list");

            //redis.ListRightPush("listdes", 89);
            //  List<Demo> de = new List<Demo>();       
            //  de.Add(new Demo { Id = 1, Name = "1" });
            //  de.Add(new Demo { Id = 31, Name = "31" });
            //  de.Add(new Demo { Id = 21, Name = "21" });
            //  redis.ListRightPush("listde", de);
            //  redis.StringSet("hlisdts",de);
            //var listhists=  redis.StringGet<List<Demo>>("hlisdts");
            #endregion

            //Thread.Sleep(10000);
            //Stream s = new FileStream(@"H:\包含\CIB)YET_4S5Z8KRCS(X)9_7.jpg", FileMode.Create);
            ////Images im = new Images();
            //// im.Compress(s);

            MemoryCacheHelper cache = new MemoryCacheHelper();
            //var data = "wenwanguang";
            //var key = "key1";
            //object temp = _cache.Get(key);
            //if (temp == null)
            //    cache.Set(key, data, DateTime.Now - DateTime.Now.AddDays(-1), false);
            //data = "rere";
            //temp = _cache.Get(key);


            //F f = new F();
            // var key = "key1";
            //object temp = _cache.Get(key);
            //if (temp == null)
            //    cache.Set(key, f, DateTime.Now - DateTime.Now.AddDays(-1), false);
            //f.A = "rere";
            //temp = _cache.Get(key);


            string [] data = { "1","5"};
            var key = "key1";
            object temp = _cache.Get(key);
            if (temp == null)
                cache.Set(key, data, DateTime.Now - DateTime.Now.AddDays(-1), false);
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = "d";
            }
              temp = _cache.Get(key);


            //    log.Info("wwg");  
            LoggerManager.Info("444");

           //定时任务

            return View();
        }
        public class Demo
        {
            public int M { get; set; }
            public int Id { get; set; }
            public string Name { get; set; }
        }
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
        public IActionResult Message()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
        public IActionResult Map()
        {
            ViewData["Map"] = "Your Map page.";

            return View();
        }
        public IActionResult Save(Student model)
        {
            var k = _studentsverVice.Insert(model);

            var m = "rwrewrwer" + "<br/>" + 8943543435;
            return Json(m);
        }
        /// <summary>
        /// 数字验证码
        /// </summary>
        /// <returns></returns>
        public FileContentResult NumberVerifyCode()
        {
            string code = VerifyCodeHelper.GetSingleObj().CreateVerifyCode(VerifyCodeHelper.VerifyCodeType.NumberVerifyCode);
            byte[] codeImage = VerifyCodeHelper.GetSingleObj().CreateByteByImgVerifyCode(code, 100, 40);
            return File(codeImage, @"image/jpeg");
        }

        /// <summary>
        /// 字母验证码
        /// </summary>
        /// <returns></returns>
        public FileContentResult AbcVerifyCode()
        {
            string code = VerifyCodeHelper.GetSingleObj().CreateVerifyCode(VerifyCodeHelper.VerifyCodeType.AbcVerifyCode);
            var bitmap = VerifyCodeHelper.GetSingleObj().CreateBitmapByImgVerifyCode(code, 100, 40);
            MemoryStream stream = new MemoryStream();
            bitmap.Save(stream, ImageFormat.Png);
            return File(stream.ToArray(), "image/png");
        }

        /// <summary>
        /// 混合验证码
        /// </summary>
        /// <returns></returns>
        public FileContentResult MixVerifyCode()
        {
            string code = VerifyCodeHelper.GetSingleObj().CreateVerifyCode(VerifyCodeHelper.VerifyCodeType.MixVerifyCode);
            var bitmap = VerifyCodeHelper.GetSingleObj().CreateBitmapByImgVerifyCode(code, 100, 40);
            MemoryStream stream = new MemoryStream();
            bitmap.Save(stream, ImageFormat.Gif);
            return File(stream.ToArray(), "image/gif");
        }
        public class F
        {
            public string A { get; set; }
        }
        public void Start()
        {
            QuartzSampleApp.StartTask().GetAwaiter().GetResult();
        }
        public void Image()
        {
            var path = @"D:\image\wwg.png";//size=595*842
                         ImageHelper.ImageMaxCutByCenter(path, @"D:\image\wwg2.png", 1024, 768, 75);//size=1024*768保存后的图片
                         //ImageHelper.ImageMaxCutByCenter(path, @"d:\a\文字设计003.png", 768, 1024, 75);//size=768*1024
                         //ImageHelper.ImageScalingToRange(path, @"d:\a\文字设计004.png", 1024, 768, 75);//size=542*768
                         //ImageHelper.ImageScalingToRange(path, @"d:\a\文字设计005.png", 768, 1024, 75);//size=724*1024
                         //ImageHelper.ImageScalingByOversized(path, @"d:\a\文字设计006.png", 640, 320, 75);//size=226*320
                         //ImageHelper.ImageScalingByOversized(path, @"d:\a\文字设计007.png", 320, 640, 75);//size=320*453
            
             var qrcodeSavePath = @"d:\image\logos.jpg";//保存后的图片
                         var qrcodeLogoPath = @"d:\image\logo.jpg";
                         var qrcodeWhiteBorderPixelVal = 5;
                         var qrcodeText = "高堂明镜悲白发，朝如青丝暮成雪！";
                         ImageHelper.QRCoder(qrcodeText, qrcodeSavePath, qrcodeLogoPath, qrcodeWhiteBorderPixelVal);
                         var result = ImageHelper.QRDecoder(qrcodeSavePath);
                         //Console.WriteLine(result);
            
             var imageInfo = ImageHelper.GetImageInfo(qrcodeLogoPath);
                       
        }
    }
}
