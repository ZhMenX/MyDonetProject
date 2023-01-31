<script lang="ts">
//配置页面刷新
import { provide, ref, nextTick } from "vue";
export default {
  setup() {
    const isRouteActive = ref(true);
    const reload = () => {
      isRouteActive.value = false;
      nextTick(() => {
        isRouteActive.value = true;
      });
    };
    provide("reload", reload);
    return {
      isRouteActive,
      reload,
    };
  },
};
</script>

<template>
  <div id="app">
    <RouterView v-if="isRouteActive"></RouterView>
  </div>
</template>

<style scoped>
#app {
  margin: 0%;
  padding: 0%;
  left: 0%;
  top: 0%;
}
/*flex布局类名*/
</style>
