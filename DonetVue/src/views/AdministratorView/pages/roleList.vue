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
  axios.post("Musics/GetMusicByName", user).then((res: any) => {
    musicData.tablelist = res.data;
  });
};
//编辑框
var dialogVisible = ref(false);
//新增
const onAdd = () => {
  dialogVisible.value = true;
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
            <el-button type="success" @click="onAdd">新增</el-button>
          </el-form-item>
        </el-form>
      </el-col>
      <el-dialog
        v-model="dialogVisible"
        title="Tips"
        width="30%"
        :before-close="handleClose"
      >
        <span>This is a message</span>
        <template #footer>
          <span class="dialog-footer">
            <el-button @click="dialogVisible = false">Cancel</el-button>
            <el-button type="primary" @click="dialogVisible = false"> Confirm </el-button>
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
