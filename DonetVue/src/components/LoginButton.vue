<template>
    <el-button type="primary" @click="Login" size="large">登录</el-button>
</template>

<script>

export default {
    data(){
        return{
            index:"首页",
            user:{
                userName:'zhmen',
                password:'123456'
            }
        }
    },
    methods:{
        Login(){
            this.$router.push('/login');
        }
    }
}
</script>

<style lang="scss" scoped>

</style>
