<script lang="ts" setup>
import { onMounted, reactive, ref, toRefs, inject } from "vue";
import api from "@/axios/axios.js";
import axios from "axios";
import { ElMessageBox } from "element-plus";
//获取用户列表
let musicData = reactive({
  tablelist: [],
});
onMounted(() => {
  api({
    url: "/Roles/SearchAllRoles",
    method: "get",
  }).then((res: any) => {
    musicData.tablelist = res.data;
  });
});
//搜索框
const formInline = reactive({
  name: "",
});
//新增
const formAdd = reactive({
  roleName: "",
});
//引入界面刷新
const reload: any = inject("reload");
//按用户名字进行查询
const onSubmit = () => {
  if (formInline.name != "") {
    api.get("Roles/GetRoleByName?roleName=" + formInline.name).then((res: any) => {
      musicData.tablelist = res.data;
    });
  } else {
    api({
      url: "/Roles/SearchAllRoles",
      method: "get",
    }).then((res: any) => {
      musicData.tablelist = res.data;
    });
  }
};
//新增框
var dialogVisible = ref(false);

//新增
const onAdd = () => {
  api.post("Roles/CreateRole", formAdd.roleName).then((res: any) => {
    dialogVisible.value = false;
    reload();
  });
};
//编辑框
var dialogVisibleEdit = ref(false);
const formEdit = reactive({
  userName: "",
  newPassword: "",
  currentPassword: "",
  email: "",
});
//编辑
const onEdit = (row: any) => {
  dialogVisibleEdit.value = true;
  console.log("操作的那条数据" + JSON.stringify(row));
  formEdit.userName = row.userName;
  formEdit.email = row.email;
  let user = {
    userName: formEdit.userName,
    newPassword: "",
    currentPassword: "",
    email: formEdit.email,
  };
  api.post("Users/UpdateUser", user).then((res: any) => {
    dialogVisible.value = false;
    reload();
  });
};
//删除用户
const onDelete = (row: any) => {
  api.get("Roles/DelRole?roleName=" + row.name).then((res: any) => {
    reload();
  });
};

const handleClose = (done: () => void) => {
  ElMessageBox.confirm("确定要关闭嘛")
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
            <el-input v-model="formInline.name" placeholder="请输入角色名字" />
          </el-form-item>
          <el-form-item>
            <el-button type="primary" @click="onSubmit">查询</el-button>
          </el-form-item>
          <el-form-item>
            <el-button type="success" @click="dialogVisible = true">新增</el-button>
          </el-form-item>
        </el-form>
      </el-col>
      <el-dialog
        v-model="dialogVisible"
        title="新增角色"
        width="30%"
        :before-close="handleClose"
      >
        <el-form :inline="true" :model="formAdd" class="demo-form-inline">
          <el-form-item label="名字：">
            <el-input v-model="formAdd.roleName" placeholder="请输入角色" />
          </el-form-item>
        </el-form>
        <template #footer>
          <span class="dialog-footer">
            <el-button @click="dialogVisible = false">取消</el-button>
            <el-button type="primary" @click="onAdd"> 添加 </el-button>
          </span>
        </template>
      </el-dialog>
      <el-dialog
        v-model="dialogVisibleEdit"
        title="修改用户"
        width="30%"
        :before-close="handleClose"
      >
        <el-form :inline="true" :model="formEdit" class="demo-form-inline">
          <el-form-item label="名字：">
            <el-input v-model="formEdit.userName" placeholder="请输入用户名" />
          </el-form-item>
          <el-form-item label="邮箱：">
            <el-input v-model="formEdit.email" placeholder="请输入邮箱" />
          </el-form-item>
        </el-form>
        <template #footer>
          <span class="dialog-footer">
            <el-button @click="dialogVisibleEdit = false">取消</el-button>
            <el-button type="primary" @click="onEdit"> 修改 </el-button>
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
        <el-table-column prop="id" label="id" width="50" />
        <el-table-column prop="name" label="Name" />
        <el-table-column prop="normalizedName" label="NormalizedName" />
        <el-table-column label="操作">
          <template #default="scope">
            <el-button type="danger" @click="onDelete(scope.row)">删除</el-button>
          </template>
        </el-table-column>
      </el-table>
    </el-col>
  </el-row>
</template>

<style scoped></style>
