<script lang="ts">
import menuList from "../components/Menu.vue";
import { ref, onMounted, reactive, inject } from "vue";
import type { TabsPaneContext } from "element-plus";
import api from "@/axios/axios.js";
import { ElMessage, ElMessageBox } from "element-plus";
import type { Action } from "element-plus";
import router from "../router";
import player from "../components/Aplayer.vue";
// Import Swiper Vue.js components
import { Swiper, SwiperSlide } from "swiper/vue";

// Import Swiper styles
import "swiper/css";

import "swiper/css/pagination";
import "swiper/css/navigation";

// import required modules
import { Pagination, Navigation } from "swiper";
// Import Swiper styles
import "swiper/css";

export default {
  components: {
    menuList,
    player,
    Swiper,
    SwiperSlide,
  },
  setup(prop) {
    //Tab切换
    const activeNameNum = ref("1");
    const currentDate = ref(new Date());
    const handleClickNum = (tab: TabsPaneContext, event: Event) => {};
    //歌单列表获取
    let tableData = reactive({
      list: [],
      musicId: "",
    });
    //配置页面刷新
    const reload: any = inject("reload");
    let timer = reactive({ value: 1 });
    const onSwiper = (swiper: any) => {
      console.log(swiper);
    };
    const onSlideChange = () => {
      console.log("slide change");
    };
    onMounted(() => {
      //检查是否登录
      var token = localStorage.getItem("token");

      if (token === null) {
        ElMessageBox.alert("欢迎来的QQ音乐,请登录！", "Hello", {
          confirmButtonText: "OK",
          center: true,
          callback: (action: Action) => {
            ElMessage({
              type: "info",
              message: "正在去登录...",
              onClose: function () {
                router.push({ path: "/login" });
              },
            });
          },
        });
      }
      //登录后即可获取数据
      if (token != null) {
        api({
          url: "/Musics/GetMusicList",
          method: "get",
        }).then((res: any) => {
          tableData.list = res.data;
        });
      }
    });

    const goodclick = () => {
      //reload();
    };
    const rowBack = (row: any, column: any, cell: any, event: any) => {
      console.log(row);
      console.log(column);
      console.log(cell);
      console.log(event);
    };
    //播放
    const onPlayer = (row: any) => {
      console.log("操作的那条数据" + JSON.stringify(row.singer));
      tableData.musicId = row.pid;
      localStorage.setItem("musicId", row.pid);
      localStorage.setItem("musicName", JSON.stringify(row.name));
      localStorage.setItem("musicSinger", JSON.stringify(row.singer));
      localStorage.setItem("musicheadPortrait", JSON.stringify(row.headPortrait));
      //timer.value = new Date().getTime();
      reload();
    };
    return {
      activeNameNum,
      handleClickNum,
      tableData,
      rowBack,
      goodclick,
      onPlayer,
      onSwiper,
      onSlideChange,
      modules: [Pagination, Navigation],
      currentDate,
      timer,
    };
  },
};
</script>

<template>
  <div class="common-layout">
    <el-container style="height: auto; width: 100%">
      <el-container class="z_container">
        <el-header style="height: 142px">
          <div class="leader">
            <span class="menu">
              <menuList></menuList>
            </span>
          </div>
          <!--tab歌单推荐-->
          <div style="margin: 0%; padding: 0%">
            <el-tabs
              v-model="activeNameNum"
              class="demo-tabs"
              @tab-click=""
              tab-position="top"
              type="border-card"
              style="width: 100%"
            >
              <div style="margin-left: 480px">
                <span>
                  <p style="color: black; font-size: xx-large; align-items: center">
                    歌单推荐
                  </p>
                </span>
              </div>
              <el-tab-pane name="1">
                <template v-slot:label>
                  <span>首页</span>
                </template>
                <template #default>
                  <!--这是首页歌曲推荐 :pagination="true"-->
                  <div style="margin-top: 70px">
                    <swiper :modules="modules">
                      <swiper-slide
                        ><ul>
                          <li>
                            <img
                              src="https://qpic.y.qq.com/music_cover/mgDVmicyKxZ16zUS0YqmTPktr1eoIeiaPbxfWfLiaHcVWTJOfn7S0ND8pzFyHicEC4cd/600?n=1"
                              style="width: 205px"
                            />
                            <p style="width: 224px">【(0.8x降速】当emo慢下来时</p>
                            <a>播放量：1789.7万</a>
                          </li>

                          <li>
                            <img
                              src="https://qpic.y.qq.com/music_cover/3sZgibm04hICGbleGFmh9STkeNmVibbbvOsPicnNTLAWChNzUIxyfFvQA/300?n=1"
                              style="width: 205px"
                            />
                            <p style="width: 224px">难忘经典：回放磁带里的青春岁月</p>
                            <a>播放量：1789.7万</a>
                          </li>

                          <li>
                            <img
                              src="https://qpic.y.qq.com/music_cover/qySHBaLckrOIYib7GXCWmDbG7K4GRBmyC88HLWlrTRMSK6CmFAIm43UZuTj4vBxhP/300?n=1"
                              style="width: 205px"
                            />
                            <p style="width: 224px">音综封神现场：顶级编唱震撼人心</p>
                            <a>播放量：1.2万</a>
                          </li>

                          <li>
                            <img
                              src="https://qpic.y.qq.com/music_cover/jiaJRGwJnWdhpt92PaG8l5bqEwiaL1o3ueVOfLzyOmz7trhIicib9wibEgA/300?n=1"
                              style="width: 205px"
                            />
                            <p style="width: 224px">耳机控收藏，享受另类孤独情绪</p>
                            <a>播放量：30.0万</a>
                          </li>
                          <li>
                            <img
                              src="https://qpic.y.qq.com/music_cover/hlGJXnBgr8xrqs7IPIpEkb090bdZM8hAb955FkgzmGLZdaMv6hLF0w/300?n=1"
                              style="width: 205px"
                            />
                            <p style="width: 224px">续相思·唱段风月往事</p>
                            <a>播放量：31.0万</a>
                          </li>
                        </ul></swiper-slide
                      >
                      <swiper-slide
                        ><ul>
                          <li>
                            <img
                              src="https://qpic.y.qq.com/music_cover/gaSSCRswoq7NlpHA8vK1PuRiaKlew1z7prD5vzyP1Ju3QYeiclfG0P4Q/300?n=1"
                              style="width: 205px"
                            />
                            <p style="width: 224px">那些年一属于周杰伦的R&B时代</p>
                            <a>播放量：1789.7万</a>
                          </li>

                          <li>
                            <img
                              src="https://qpic.y.qq.com/music_cover/ZS2nmh1hWV2OESzO7MBLDzA7xl3Tf4Tz26sXvTRlkFjvRpaETbicHZQ/300?n=1"
                              style="width: 205px"
                            />
                            <p style="width: 224px">明明很想念，却不敢再去打扰你</p>
                            <a>播放量：1789.7万</a>
                          </li>

                          <li>
                            <img
                              src="https://qpic.y.qq.com/music_cover/qySHBaLckrOIYib7GXCWmDbG7K4GRBmyCFoDqHm3R8zjnYLooxtEj63hvgB1vE8TT/300?n=1"
                              style="width: 205px"
                            />
                            <p style="width: 224px">车载DJ热歌：轻松一路Fun肆嗨！</p>
                            <a>播放量：1.2万</a>
                          </li>

                          <li>
                            <img
                              src="https://qpic.y.qq.com/music_cover/p5Qmf0LevmgOiaQdIJJAYy7DHMB7uj0beWibibmmLqEeia18MHVEd0w5zQ/300?n=1"
                              style="width: 205px"
                            />
                            <p style="width: 224px">海外爆火神曲·nternet Hits</p>
                            <a>播放量：30.0万</a>
                          </li>
                          <li>
                            <img
                              src="https://qpic.y.qq.com/music_cover/3sZgibm04hICGbleGFmh9SbW1XTBkGx4g6xz7GeGJYdJujuSjmDGwLQ/300?n=1g"
                              style="width: 210px"
                            />
                            <p style="width: 224px">『8090青春』唤醒记忆的时代乐章</p>
                            <a>播放量：30.0万</a>
                          </li>
                        </ul></swiper-slide
                      >
                    </swiper>
                  </div>
                </template>
              </el-tab-pane>
              <el-tab-pane name="2">
                <template v-slot:label>
                  <span>排行榜</span>
                </template>
                <template #default>
                  <div>
                    <div>
                      <!--这是排行榜-->
                      <el-table
                        :data="tableData.list"
                        height="500px"
                        style="width: 100%"
                        @row-click="rowBack"
                      >
                        <el-table-column prop="name" label="歌曲名称" />
                        <el-table-column prop="singer" label="演唱者" />
                        <el-table-column prop="composer" label="作曲者" />
                        <el-table-column prop="album" label="专辑" />
                        <el-table-column prop="duration" label="时长" width="80px" />
                        <el-table-column label="" width="100px">
                          <template #default="scope">
                            <el-button text type="primary" @click="onPlayer(scope.row)"
                              >播放</el-button
                            >
                          </template>
                        </el-table-column>
                      </el-table>
                    </div>
                  </div>
                </template>
              </el-tab-pane>
              <el-tab-pane name="3">
                <template v-slot:label>
                  <span>网络歌曲</span>
                </template>
                <template #default>
                  <div>
                    <!--这是首页歌曲推荐-->
                    <div>
                      <el-table :data="tableData.list" height="500px" style="width: 100%">
                        <el-table-column prop="name" label="歌曲名称" />
                        <el-table-column prop="singer" label="演唱者" />
                        <el-table-column prop="composer" label="作曲者" />
                        <el-table-column prop="album" label="专辑" />
                        <el-table-column prop="duration" label="时长" width="80px" />
                        <el-table-column label="" width="100px">
                          <template #default="scope">
                            <el-button text type="primary" @click="onPlayer(scope.row)"
                              >播放</el-button
                            >
                          </template>
                        </el-table-column>
                      </el-table>
                    </div>
                  </div>
                </template>
              </el-tab-pane>
              <el-tab-pane name="4">
                <template v-slot:label>
                  <span>经典</span>
                </template>
                <template #default>
                  <div>
                    <!--这是首页歌曲推荐-->
                    <div>
                      <el-table :data="tableData.list" height="500px" style="width: 100%">
                        <el-table-column prop="name" label="歌曲名称" />
                        <el-table-column prop="singer" label="演唱者" />
                        <el-table-column prop="composer" label="作曲者" />
                        <el-table-column prop="album" label="专辑" />
                        <el-table-column prop="duration" label="时长" width="80px" />
                        <el-table-column label="" width="100px">
                          <template #default="scope">
                            <el-button text type="primary" @click="onPlayer(scope.row)"
                              >播放</el-button
                            >
                          </template>
                        </el-table-column>
                      </el-table>
                    </div>
                  </div>
                </template>
              </el-tab-pane>
            </el-tabs>
          </div>
        </el-header>
        <el-main> </el-main>
        <el-footer>
          <div>
            <!--音乐播放器-->
            <player @click.native="goodclick" :key="timer.value"></player>
          </div>
        </el-footer>
      </el-container>
    </el-container>
  </div>
</template>
<style scoped>
.el-container {
  width: 100%;
  padding: 0;
  margin: 10px;
  height: 100vh;
}
.time {
  font-size: 12px;
  color: #999;
}

.bottom {
  margin-top: 13px;
  line-height: 12px;
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.button {
  padding: 0;
  min-height: auto;
}

.image {
  width: 20%;
  height: 20%;
  display: block;
}

.z_container {
  width: 100%;
}
.leader {
  margin-top: 0%;
  width: 1200px;
  height: 142px;
}
.logo {
  height: 53px;
  width: 170px;
}
.menu {
  height: auto;
  width: auto;
}
.el-row {
  margin-bottom: 20px;
}
.el-row:last-child {
  margin-bottom: 0;
}
.el-col {
  border-radius: 4px;
}

.grid-content {
  border-radius: 4px;
  min-height: 36px;
}
.search {
  height: 64px;
  width: 220px;
}
.demo-tabs > .el-tabs__content {
  padding: 32px;
  color: #6b778c;
  font-size: 32px;
  font-weight: 600;
}
.el-carousel__item h3 {
  display: flex;
  color: #475669;
  opacity: 0.75;
  line-height: 300px;
  margin: 0;
}

.el-carousel__item:nth-child(2n) {
  background-color: #99a9bf;
}

.el-carousel__item:nth-child(2n + 1) {
  background-color: #d3dce6;
}
.swiper {
  width: 1520px;
  height: 100%;
}

.swiper-slide {
  width: 1520px;
  height: 340px;
  float: left;
  text-align: center;
  font-size: 18px;
  background: #fff;
  /* Center slide text vertically */
  display: -webkit-box;
  display: -ms-flexbox;
  display: -webkit-flex;
  display: flex;
  -webkit-box-pack: center;
  -ms-flex-pack: center;
  -webkit-justify-content: center;
  justify-content: center;
  -webkit-box-align: center;
  -ms-flex-align: center;
  -webkit-align-items: center;
  align-items: center;
}

.swiper-slide img {
  display: block;
  object-fit: cover;
  float: left;
  margin: 0px 0px;
  margin-right: 15px;
  padding: 0 0;
}
/* 图片容器 */

.swiper-wrapper .swiper-slide ul li p {
  font-size: 14px;
  line-height: 20px;
  color: #000;
  text-decoration: none;
  /* padding-top: 10px; */
  margin-bottom: 10px;
}

.swiper-wrapper .swiper-slide ul li p:hover {
  color: #31c27c;
  text-decoration: none;
  cursor: pointer;
}

.swiper-wrapper .swiper-slide ul {
  width: 1520px;
  height: 340px;
  padding: 0;
  float: left;
}

.swiper-wrapper .swiper-slide ul li {
  list-style: none;
  float: left;
}

.swiper-wrapper .swiper-slide ul li a {
  color: #999;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
  height: 22px;
  padding-top: -20px;
}

.swiper-wrapper .swiper-slide ul li a:hover {
  cursor: pointer;
}
</style>
