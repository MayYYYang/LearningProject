using DesignPattern.Patterns.Decorator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reflection;
using ConsoleApplication1;
using Newtonsoft.Json;

namespace DesignPattern
{
    public class VeevaVaultConfigs
    {
        public int TotalPageCount { get; set; }
        public int SearchCount { get; set; }

        public string ExcludeStatus { get; set; }
        public string IncludeDocumentType { get; set; }
        public string SuccessStatus { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            DBMain db = new DBMain();
            db.Main();

        }

    }
    public class MeetingCampaignStateDTO
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string SubName { get; set; }
        public string URL { get; set; }
        public DateTime StartTime { get; set; }
        public string ShowTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Comments { get; set; }
        public int Order { get; set; }
        //List<MeetingCampaignStateDTO> list = new List<MeetingCampaignStateDTO> { };
        //var test = new MeetingCampaignStateDTO {
        //    Type = "Game",
        //    Name = "拼图游戏",
        //    SubName = "拼图游戏",
        //    StartTime = DateTime.UtcNow,
        //    EndTime = DateTime.UtcNow.AddDays(5),
        //    ShowTime="",
        //    Comments="",
        //    Order=1
        //};
        //var test1 = new MeetingCampaignStateDTO
        //{
        //    Type = "Survey",
        //    Name = "投票",
        //    SubName = "投票",
        //    StartTime = DateTime.UtcNow,
        //    EndTime = DateTime.UtcNow.AddDays(5),
        //    ShowTime = "",
        //    Comments = "",
        //    Order = 2
        //};
        //var test2 = new MeetingCampaignStateDTO
        //{
        //    Type = "Gift",
        //    Name = "礼品兑换",
        //    SubName = "礼品兑换",
        //    StartTime = DateTime.UtcNow,
        //    EndTime = DateTime.UtcNow.AddDays(5),
        //    ShowTime = "",
        //    Comments = "",
        //    Order = 3
        //};
        //list.Add(test);
        //list.Add(test1);
        //list.Add(test2);
        //var ss = JsonConvert.SerializeObject(list);
    }
}
