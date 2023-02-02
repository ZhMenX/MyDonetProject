<script lang="ts" setup>
import router from "../../router";
import { Document, Menu as IconMenu, Location, Setting } from "@element-plus/icons-vue";
const handleOpen = (key: string, keyPath: string[]) => {
  console.log(key, keyPath);
};
const handleClose = (key: string, keyPath: string[]) => {
  console.log(key, keyPath);
};
const goBack = () => {
  router.push({ path: "/" });
};
</script>
<template>
  <div class="common-layout">
    <el-container>
      <el-header class="common-header" height="100px">
        <el-page-header @back="goBack">
          <template #content>
            <span class="text-large font-600 mr-3"> 返回首页 </span>
          </template>
        </el-page-header>
        <h1 class="title">后台管理系统</h1>
      </el-header>
      <el-container>
        <el-aside class="common-aside" width="200px">
          <el-menu
            default-active="2"
            class="el-menu-vertical-demo"
            @open="handleOpen"
            @close="handleClose"
            router
          >
            <el-menu-item :index="'/music'">
              <el-icon><icon-menu /></el-icon>
              <span>音乐管理</span>
            </el-menu-item>
            <el-sub-menu index="2">
              <template #title>
                <el-icon><location /></el-icon>
                <span>登录管理</span>
              </template>
              <el-menu-item-group title="用户">
                <el-menu-item :index="'/user'">用户管理</el-menu-item>
              </el-menu-item-group>
              <el-menu-item-group title="权限">
                <el-menu-item :index="'/role'">角色管理</el-menu-item>
              </el-menu-item-group>
            </el-sub-menu>
          </el-menu>
        </el-aside>
        <el-main>
          <router-view></router-view>
        </el-main>
      </el-container>
    </el-container>
  </div>
</template>

<style>
.el-container {
  height: 100vh;
  width: 100%;
  overflow: hidden;
}
.common-header {
  background-color: azure;
}
.common-aside {
  background-color: azure;
}
.logo {
  width: 50px;
}
.title {
  text-align: center;
}
</style>
