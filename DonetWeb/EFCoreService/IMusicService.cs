using EFCoreModel.Entity;

namespace EFCoreService
{
    public interface IMusicService
    {
        //获取所有歌曲
        List<Music> GetAllMusics();

        //增
        bool AddMusic(Music music);

        //修改
        bool UpdateMusic(Music music);

        //删除
        bool DeleteMusic(long id);

        //按歌曲名字获取
        List<Music> GetMusicsByName(string name);

    }
}