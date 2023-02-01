using DonetWeb2.Results;
using EFCoreModel.Entity;
using EFCoreService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DonetWeb2.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    //[Authorize]//表示这个控制器类下所有的操作方法都需要登录后才能访问
    [Authorize(Policy = "Music")]
    public class MusicsController : ControllerBase
    {
        private MusicService musicService;

        public MusicsController(MusicService musicService)
        {
            this.musicService = musicService;
        }

        //获取所有音乐列表
        [HttpGet]
        public ResponseResult GetMusicList()
        {
            List<Music> musics = musicService.GetAllMusics();

            return ResponseResult.Success(musics);

        }
        //按歌曲名字获取歌曲
        [HttpPost]
        public ResponseResult GetMusicByName(MusicRequest musicRequest) {
            List<Music> musics = musicService.GetMusicsByName(musicRequest.name);
            return ResponseResult.Success(musics);
        }
        //增加
        [HttpPost]
        public ResponseResult AddMusic(Music music)
        {
            musicService.AddMusic(music);

            return ResponseResult.Success("添加成功!");


        }

        //修改
        [HttpPost]
        public ResponseResult UpdateMusic(Music music)
        {
            musicService.UpdateMusic(music);

            return ResponseResult.Success("修改成功!");


        }
        //删除
        [HttpGet]
        public ResponseResult DeleteMusic(long mid)
        {
            musicService.DeleteMusic(mid);

            return ResponseResult.Success("删除成功!");


        }
    }
}
