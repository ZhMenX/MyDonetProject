<script lang="ts" setup>
import { onMounted, reactive, ref, toRefs, inject } from "vue";
import api from "@/axios/axios.js";
import axios from "axios";
import { ElMessageBox } from "element-plus";
//获取音乐列表
let musicData = reactive({
  tablelist: [],
});
onMounted(() => {
  api({
    url: "/Musics/GetMusicList",
    method: "get",
  }).then((res: any) => {
    musicData.tablelist = res.data;
  });
});
//搜索框
const formInline = reactive({
  name: "",
});
//引入界面刷新
const reload: any = inject("reload");
//按歌曲名字进行查询
const onSubmit = () => {
  let user = {
    name: formInline.name,
  };
  api.post("Musics/GetMusicByName", user).then((res: any) => {
    musicData.tablelist = res.data;
  });
};
//编辑框
var dialogVisibleAdd = ref(false);
//新增处理
const formAdd = reactive({
  Name: "",
  Singer: "",
  Composer: "",
  Album: "",
  Duration: "",
  Pid: "",
  headPortrait: "",
});
const openAdd = () => {
  dialogVisibleAdd.value = true;
};
const onAdd = () => {
  let music = {
    Name: formAdd.Name,
    Singer: formAdd.Singer,
    Composer: formAdd.Composer,
    Album: formAdd.Album,
    Duration: formAdd.Duration,
    Pid: formAdd.Pid,
    headPortrait: formAdd.headPortrait,
  };
  api.post("Musics/AddMusic", JSON.stringify(music)).then((res: any) => {
    musicData.tablelist = res.data;
    dialogVisibleAdd.value = false;
    reload();
  });
};
//编辑
const onEdit = (row: any) => {
  console.log("操作的那条数据" + JSON.stringify(row.pid));
};

const handleClose = (done: () => void) => {
  ElMessageBox.confirm("Are you sure to close this dialog?")
    .then(() => {
      done();
    })
    .catch(() => {
      // catch error
    });
};
</script>

<template>
  <el-row class="tac">
    <el-col :span="24">
      <el-col :span="24">
        <el-form :inline="true" :model="formInline" class="demo-form-inline">
          <el-form-item label="">
            <el-input v-model="formInline.name" placeholder="请输入歌曲名字" />
          </el-form-item>
          <el-form-item>
            <el-button type="primary" @click="onSubmit">查询</el-button>
          </el-form-item>
          <el-form-item>
            <el-button type="success" @click="openAdd">新增</el-button>
          </el-form-item>
        </el-form>
      </el-col>
      <el-dialog
        v-model="dialogVisibleAdd"
        title="新增歌曲"
        width="55%"
        :before-close="handleClose"
      >
        <el-form :inline="true" class="demo-form-inline">
          <el-form-item label="歌曲名字：">
            <el-input v-model="formAdd.Name" placeholder="请输入歌曲名字" />
          </el-form-item>
          <el-form-item label="歌手名字：">
            <el-input v-model="formAdd.Singer" placeholder="请输入歌手名字" />
          </el-form-item>
          <el-form-item label="作曲名字：">
            <el-input v-model="formAdd.Composer" placeholder="请输入作曲名字" />
          </el-form-item>
          <el-form-item label="歌曲专辑：">
            <el-input v-model="formAdd.Album" placeholder="请输入歌曲专辑" />
          </el-form-item>
          <el-form-item label="歌曲时长：">
            <el-input v-model="formAdd.Duration" placeholder="请输入歌曲时长" />
          </el-form-item>
          <el-form-item label="外链接源：">
            <el-input v-model="formAdd.Pid" placeholder="请输入外链接地址" />
          </el-form-item>
          <el-form-item label="链接头像：">
            <el-input v-model="formAdd.headPortrait" placeholder="请输入链接头像" />
          </el-form-item>
        </el-form>
        <template #footer>
          <span class="dialog-footer">
            <el-button @click="dialogVisibleAdd = false">取消</el-button>
            <el-button type="primary" @click="onAdd"> 新增 </el-button>
          </span>
        </template>
      </el-dialog>
    </el-col>

    <el-col :span="24">
      <el-table
        :data="musicData.tablelist"
        ref="singleTableRef"
        height="550"
        style="width: 100%"
        highlight-current-row
      >
        <el-table-column prop="mid" label="mid" width="50" />
        <el-table-column prop="name" label="歌曲名称" />
        <el-table-column prop="singer" label="演唱者" />
        <el-table-column prop="album" label="专辑" />
        <el-table-column label="操作">
          <template #default="scope">
            <el-button type="primary" @click="onEdit(scope.row)">编辑</el-button>
            <el-button type="danger">删除</el-button>
          </template>
        </el-table-column>
      </el-table>
    </el-col>
  </el-row>
</template>

<style scoped></style>
