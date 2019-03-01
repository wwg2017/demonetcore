using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLibrary.Model
{
    public class ImgModel
    {
        public string FileName { get; set; }
        public string UploadDate { get; set; }
        public string Operator { get; set; }
        public string FilePath { get; set; }

        public int RecordID { get; set; }
    }
}
