using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioWorkshop.Recorder
{
    public class RecordedFile
    {
        public RecordedFile(string fileName, int seconds)
        {
            this.pkey = Guid.NewGuid();
            this.fileName = fileName;
            this.seconds = seconds;
            this.whenCreated = DateTime.Now.AddSeconds(-seconds);
            this.tag = "";
        }

        public Guid pkey { get; set; }
        public string fileName { get; set; }
        public int seconds { get; set; }
        public DateTime whenCreated { get; set; }
        public string tag { get; set; }
    }
}
