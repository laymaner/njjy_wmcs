<template>
    <div class="mainContainer">
      <iframe id="tempHtml" ref="tempHtml" style="width: 100%; height: calc(100vh - 84px);" :src="html" frameborder="0" />
    </div>
  </template>
  
  <script lang="ts">
  import { Component, Vue } from "vue-property-decorator";
  import { Action, State } from "vuex-class";
  import searchMixin from "@/vue-custom/mixin/search";
  import actionMixin from "@/vue-custom/mixin/action-mixin";
  // 假设这里原本导入的 DialogForm 在新的代码中不再需要，可以注释掉或删除该行
  // import DialogForm from "./views/dialog-form.vue";
  import store from "./store/index";
  // 查询参数, table列 ★★★★★
  import { ASSEMBLIES, TABLE_HEADER } from "./config";
  import i18n from "@/lang";
  
  @Component({
    name: "sysmapmonitor",
    // 由于新的代码结构与之前的 mixins 不相关，可以考虑删除或调整 mixins 的使用
    // mixins: [searchMixin(TABLE_HEADER), actionMixin(ASSEMBLIES)],
    store,
    // 由于新的代码结构不再需要 DialogForm 组件，可以删除或注释掉 components 的配置
    // components: {
    //   DialogForm
    // }
  })
  export default class Index extends Vue {
    isActive: boolean = false;
  
    // 删除原有的 SEARCH_DATA 属性，因为新的代码结构不需要它
    // get SEARCH_DATA() {
    //   return {
    //     formProps: {
    //       "label-width": "75px",
    //       inline: true
    //     },
    //     formItem: {
    //       "mapConfig": {
    //         label: "mapConfig",
    //         rules: [],
    //         type: "input"
    //       },
    //     }
    //   };
    // }
  
    data() {
      return {
        html: '/public/display/index.html'
      };
    }
  
    mounted() {
  
    }
  
    methods: {
  
    }
  }
  </script>
  
  <style>
  
  </style>