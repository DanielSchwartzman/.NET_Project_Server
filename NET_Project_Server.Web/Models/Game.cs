using System.ComponentModel.DataAnnotations;

namespace NET_Project_Server.Web.Models
{
    public class Game
    {
        [Key]
        public int? Gid { get; set; }
        public int? Pid { get; set; }
        public bool Winner { get; set; }
        public DateTime Date { get; set; }
    }
}
