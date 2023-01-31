using EFCore;
using EFCoreModel.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreService
{
    public class MusicService : IMusicService
    {
        private readonly MyDbContext context;

        public MusicService(MyDbContext context)
        {
            this.context = context;
        }
        //增加
        public bool AddMusic(Music music)
        {

            context.Music.Add(music);
            context.SaveChanges();
            return true;

        }
        //删除
        public bool DeleteMusic(long id)
        {
            Music? music =  context.Music.Single(m => m.Mid== id);
     
            context.Music.Remove(music);
            context.SaveChanges();

            return true;

        }
        //查询所有

        public List<Music> GetAllMusics()
        {

            List<Music> musics = context.Music.Where(m =>m.Mid>1).ToList();

            return musics;
        }

        //按歌曲名字进行查询
        public List<Music> GetMusicsByName(string name)
        {
            List<Music> musics = context.Music.Where(m => m.Name.Contains(name)).ToList();
            return musics;
        }

        //修改
        public bool UpdateMusic(Music music)
        {
            context.Update(music);
            context.SaveChanges();
            return true;
        }
    }
}
