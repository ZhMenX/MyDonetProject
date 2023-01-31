<script lang="ts" setup>
import { ref, onMounted,reactive,inject } from 'vue'
import axios from 'axios'
import { ElMessage } from 'element-plus'
import router from '../router';
//登录对话框
var dialogFormVisible = ref(false)
const formLabelWidth = '140px'
const form = reactive({
  name: 'zhmen',
  password:'123456',
  region: '',
  date1: '',
  date2: '',
  delivery: false,
  type: [],
  resource: '',
  desc: '',
})

//登录方法
//对话框登录方法
function Login(){
  let user={
    userName:form.name,
    password:form.password
  }
  axios.post('Login/Login',user).then((res:any)=>{
    if(res!=null){
      ElMessage({
        message: '登录成功! 你好 '+res.data.data.userName,
        type: 'success',
      })
      //关闭对话框
      dialogFormVisible.value = false
      router.push({path:'/'})
      console.log(res.data)
      //使用localStorage存储token,isAdmin,用户信息
      localStorage.setItem("token",res.data.token)
      if (res.data.isAdmin==true) {
        localStorage.setItem("isAdmin",res.data.isAdmin)
      }
      
      localStorage.setItem("userName",res.data.data.userName)
    }


  }).catch((res:any)=>{
    ElMessage({
        message: res.data.errorMessage,
        type: 'success',
        onClose(){
          
        }
      })
  })
}
onMounted(()=>{
  dialogFormVisible.value=true
})
  
</script>
<template>

    <span style="padding-bottom: 10px;" >
        <!-- Form -->
        <!--<el-button text @click="dialogFormVisible = true">
          登录
        </el-button>
        -->
        <el-dialog v-model="dialogFormVisible" title="QQ音乐登录" draggable>
          <el-form :model="form">
            <el-form-item label="Name" :label-width="formLabelWidth">
              <el-input v-model="form.name" autocomplete="off" />
            </el-form-item>
            <el-form-item label="Password" :label-width="formLabelWidth">
              <el-input v-model="form.password" autocomplete="off" />
            </el-form-item>
            <span class="dialog-footer" style="position:relative;margin-left: 500px;">
                <el-button @click="dialogFormVisible = false" size="large" style="margin-right: 30px;">取消</el-button>
                <el-button type="primary" @click="Login" size="large">
                  登录
                </el-button>
              </span>              
          </el-form>
        </el-dialog>
    </span>

</template>



<style lang="scss" scoped>

</style>
