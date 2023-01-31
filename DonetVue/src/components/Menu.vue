<script lang="ts" setup>
import { ref } from "vue";
import { reactive } from "vue";
import { ElMessage } from "element-plus";
import { inject } from "vue";
import router from "../router";
const formInline = reactive({
  user: "",
  region: "",
});

const onSubmit = () => {
  console.log("submit!");
};
const activeIndex = ref("1");
const handleSelect = (key: string, keyPath: string[]) => {
  console.log(key, keyPath);
};

//引入界面刷新
const reload: any = inject("reload");
//注销登录
function logout() {
  localStorage.removeItem("token");
  localStorage.removeItem("isAdmin");
  localStorage.removeItem("userInfo");
  ElMessage({
    message: "正在注销...",
    type: "success",
    onClose() {
      //刷新界面
      reload();
    },
  });
}
//获取用户信息
var userName = localStorage.getItem("userName");
var admin = localStorage.getItem("isAdmin");

//进入后台
function access() {
  router.push({ path: "/adminIndex" });
}
</script>
<template>
  <div class="common-layout">
    <el-menu
      :default-active="activeIndex"
      class="el-menu-demo"
      mode="horizontal"
      :ellipsis="false"
      style="margin-right: 50px"
      @select="handleSelect"
      :router="true"
    >
      <el-menu-item>
        <el-image
          style="width: 100px; height: 58px"
          src="https://y.qq.com/mediastyle/yqq/img/logo.png?max_age=2592000"
          fit="contain"
      /></el-menu-item>
      <div class="flex-grow" />
      <el-menu-item :index="'/'"> 音乐馆 </el-menu-item>
      <el-menu-item :index="'/myMusic'">我的音乐</el-menu-item>
      <el-menu-item>
        <el-form
          :inline="true"
          :model="formInline"
          class="demo-form-inline"
          style="margin-top: 10px"
          size="large"
        >
          <el-form-item>
            <el-input v-model="formInline.user" placeholder="搜索音乐、作者" />
          </el-form-item>
          <el-form-item>
            <el-button type="primary" @click="onSubmit" size="large">搜索</el-button>
          </el-form-item>
        </el-form>
      </el-menu-item>
    </el-menu>
    <span style="height: 55px">
      <span style="padding-bottom: 10px">
        <span v-if="userName != null"> 你好！{{ userName }} </span>
        <!-- Form -->
        <el-button text @click="logout" v-if="userName != null"> 注销 </el-button>
        <span class="back">
          <el-button text v-if="admin != null" @click="access">
            您是管理员，可以进入后台
          </el-button>
        </span>
      </span>
    </span>
  </div>
</template>

<style scoped>
.flex-grow {
  flex-grow: 1;
}
.back {
  margin-left: 950px;
}
</style>
