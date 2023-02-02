<template>
  <div id="app">
    <!-- 准备一个容器用来存放音乐播放器 -->
    <div id="aplayer"></div>
  </div>
</template>

<script>
import APlayer from "APlayer"; // 引入音乐插件
import "APlayer/dist/APlayer.min.css"; // 引入音乐插件的样式
import { ref, onMounted, reactive, inject } from "vue";
export default {
  name: "App",
  emits: ["inFocus", "submit"],
  setup(props, ctx) {
    ctx.emit("submit");
    return { ctx };
  },
  data() {
    return {
      mid: localStorage.getItem("muscid"),
      audio: [
        // 歌曲列表
        {
          name: localStorage.getItem("musicName"), // 歌曲名字
          artist: localStorage.getItem("musicSinger"), // 歌曲演唱者
          // 歌曲地址（这里用外链地址）
          url:
            "https://music.163.com/song/media/outer/url?id=" +
            localStorage.getItem("musicId"),
          cover:
            "http://p2.music.126.net/ZqJp0p4eDzCUCboH-WZJYA==/109951163326946996.jpg?param=130y130", // 歌曲头像

          lrc: "", // 歌词
          theme: "rgb(127, 218, 180)", // 播放这首歌曲时的主题色
        },
      ],
      info: {
        fixed: true, // 不开启吸底模式
        listFolded: true, // 折叠歌曲列表
        autoplay: true, // 开启自动播放
        preload: "auto", // 自动预加载歌曲
        loop: "all", // 播放循环模式、all全部循环 one单曲循环 none只播放一次
        order: "list", //  播放模式，list列表播放, random随机播放
      },
    };
  },

  mounted() {
    // 初始化播放器
    this.initAudio();
    console.log("这是路径" + this.audio[0].url);
    console.log("这是名字" + this.audio[0].name);
    console.log("这是歌手" + this.audio[0].artist);
    console.log("这是结果" + localStorage.getItem("musicName"));
  },
  methods: {
    initAudio() {
      // 创建一个音乐播放器实例，并挂载到DOM上，同时进行相关配置
      const ap = new APlayer({
        container: document.getElementById("aplayer"),
        audio: this.audio, // 音乐信息
        ...this.info, // 其他配置信息
      });
    },
    goodclick() {},
  },
};
</script>

<style lang="less" scoped>
#app {
  width: 100%;
  height: 100%;
  padding: 50px;
  #aplayer {
    width: 480px; // 定个宽度
  }
}
</style>
