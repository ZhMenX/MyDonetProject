import { createRouter } from "vue-router";

const router = createRouter
var token = localStorage.getItem('token')
router.beforeEach(async(to, from, next) => {
    if (to.matched.some(route => route.meta.requiredAuth)){  //判断该路由是否需要登录权限
        if (token === null){
            next({
                path: '/login',
                query: {redirect: to.fullPath}, // 将跳转的路由path作为参数，登录成功后跳转到该路由
            })
        }
        else{
            next();
        }
    }else if(to.matched.some(route => route.meta.requireAdmin)){ //判断该路由是否需要管理员权限
        var isadmin = localStorage.getItem("isAdmin")
        if(isadmin === null){
            next({
                path: '/',
                query: {redirect: to.fullPath}, // 将跳转的路由path作为参数，登录成功后跳转到该路由
            })
        }
    }
    else {
        next();
    }
})