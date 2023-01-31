
const routes = [
    {
        name: 'index',
        path: '/',
        component:() => import('@/views/Index.vue'),
        meta: {
            requireAuth: true,  // 添加该字段，表示进入这个路由是需要登录的
        }
    }, 
    {
        name: 'login',
        path: '/login',
        component:() => import('@/views/Login.vue'),
        meta: {
            requireAuth: true,  // 添加该字段，表示进入这个路由是需要登录的
        }
    },
    {
        name:'myMusic',
        path:'/myMusic',
        component:() => import('@/views/MyMusic.vue'),
        meta: {
            requireAuth: true,  // 添加该字段，表示进入这个路由是需要登录的
        }
    },
    {
        name:'musicHall',
        path:'/musicHall',
        component:() => import('@/views/MusicHall.vue'),
        meta: {
            requireAuth: true,  // 添加该字段，表示进入这个路由是需要登录的
        }
    },
    {
        name:'adminIndex',
        path:'/adminIndex',
        component:() => import('@/views/AdministratorView/AdminIndex.vue'),
        meta: {
            requireAdmin: true,  // 添加该字段，表示进入这个路由是需要登录的
        },
        redirect: "/admin",
        children:[
            {
                path:"/admin",
                name:"admin",
                component: ()=> import('@/views/AdministratorView/pages/index.vue'),
            },
            {
                path:"/user",
                name:"user",
                component: ()=> import('@/views/AdministratorView/pages/userList.vue'),
            },
            {
                path:"/role",
                name:"role",
                component: ()=> import('@/views/AdministratorView/pages/roleList.vue'),
            },
            {
                path:"/music",
                name:"music",
                component: ()=> import('@/views/AdministratorView/pages/musicList.vue'),
            }
        ]
    }

];
 
export default routes