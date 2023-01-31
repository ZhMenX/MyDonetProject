using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreModel.Entity
{
    public class Music
    {

        [Key]
        public int Mid { get; set; }
        //歌曲名字
        public string Name { get; set; }

        //演唱者
        public string Singer { get; set; }

        //作曲者
        public string Composer { get; set; }

        //专辑
        public string Album { get; set; }
        //时长
        public string Duration { get; set; }

        //外链ID
        public int Pid { get; set; }

    }
}
