using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malshinon.Models
{
    public class IntelReports
    {
        public int Id { get; set; }
        public int ReporterId { get; set; }
        public int TargetId { get; set; }
        public string Text { get; set; }
        public DateTime? TimeStamp { get; set; }
        public IntelReports(int reporterId,int targetId,string text,DateTime? timeStemp = null, int id = 0)
        {
            ReporterId = reporterId;
            TargetId = targetId;
            Text = text;
            TimeStamp = timeStemp;
            Id = id;
        }
    }
}
