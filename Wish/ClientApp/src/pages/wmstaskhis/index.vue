<template>
    <card>
        <wtm-search-box :ref="searchRefName" :events="searchEvent" :formOptions="SEARCH_DATA" :needCollapse="true" :isActive.sync="isActive" />
        <!-- 操作按钮 -->
        <wtm-but-box :assembly="assembly" :action-list="actionList" :selected-data="selectData" :events="actionEvent" />
        <!-- 列表 -->
        <wtm-table-box :attrs="{...searchAttrs, actionList}" :events="{...searchEvent, ...actionEvent}">

        </wtm-table-box>
        <!-- 弹出框 -->
        <dialog-form :is-show.sync="dialogIsShow" :dialog-data="dialogData" :status="dialogStatus" @onSearch="onHoldSearch" />
        <!-- 导入 -->
        <upload-box :is-show.sync="uploadIsShow" @onImport="onImport" @onDownload="onDownload" />
    </card>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { Action, State } from "vuex-class";
import searchMixin from "@/vue-custom/mixin/search";
import actionMixin from "@/vue-custom/mixin/action-mixin";
import DialogForm from "./views/dialog-form.vue";
import store from "./store/index";
// 查询参数, table列 ★★★★★
import { ASSEMBLIES, TABLE_HEADER,  } from "./config";
import i18n from "@/lang";

@Component({
    name: "wmstaskhis",
    mixins: [searchMixin(TABLE_HEADER), actionMixin(ASSEMBLIES)],
    store,
    components: {
        DialogForm
    }
})
export default class Index extends Vue {
    isActive: boolean = false;

    @Action
    getDicByStatus;
    // @State
    getDicByStatusData : any=[];

    get SEARCH_DATA() {
        return {
            formProps: {
                "label-width": "75px",
                inline: true
            },
            formItem: {
              "wmsTaskNo":{
                    label: this.$t("wmstaskhis.wmsTaskNo"),
                    rules: [],
                    type: "input"
                    // ,isHidden: !this.isActive
              },
              "palletBarcode":{
                    label: this.$t("wmstaskhis.palletBarcode"),
                    rules: [],
                    type: "input"
                    // ,isHidden: !this.isActive
              },
              //   "feedbackStatus":{
              //       label: this.$t("wmstaskhis.feedbackStatus"),
              //       rules: [],
              //       type: "input"
              // },
                "frLocationNo":{
                    label: this.$t("wmstaskhis.frLocationNo"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
              //   "frLocationType":{
              //       label: this.$t("wmstaskhis.frLocationType"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
              //   "loadedType":{
              //       label: this.$t("wmstaskhis.loadedType"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
                "orderNo":{
                    label: this.$t("wmstaskhis.orderNo"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                
              //   "proprietorCode":{
              //       label: this.$t("wmstaskhis.proprietorCode"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
              //   "regionNo":{
              //       label: this.$t("wmstaskhis.regionNo"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
                "roadwayNo":{
                    label: this.$t("wmstaskhis.roadwayNo"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "stockCode":{
                    label: this.$t("wmstaskhis.stockCode"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "taskStatus":{
                    label: this.$t("wmstaskhis.taskStatus"),
                    rules: [],
                    type: "select",
                    children: this.getDicByStatusData,
                    props: {
                        clearable: true,
                        placeholder: this.$t("form.all")
                    }
              },
                "taskTypeNo":{
                    label: this.$t("wmstaskhis.taskTypeNo"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "toLocationNo":{
                    label: this.$t("wmstaskhis.toLocationNo"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
              //   "toLocationType":{
              //       label: this.$t("wmstaskhis.toLocationType"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
              //   "whouseNo":{
              //       label: this.$t("wmstaskhis.whouseNo"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
                
              //   "wmstaskType":{
              //       label: this.$t("wmstask.wmsTaskType"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
            }
        };
    }

     mounted() {
      this.getDicByStatus().then((data) => {
          if(data.status==="0"){
            this.getDicByStatusData = data.data.items; // 将获取的数据存储到数组中
          }else{
            this.getDicByStatusData =[];
          }
      });
    }
}
</script>
