<template>
    <card>
        <wtm-search-box :ref="searchRefName" :events="searchEvent" :formOptions="SEARCH_DATA" :needCollapse="true" :isActive.sync="isActive" />
        <!-- 操作按钮 -->
        <wtm-but-box :assembly="assembly" :action-list="actionList" :selected-data="selectData" :events="actionEvent" />
        <!-- 列表 -->
        <wtm-table-box :attrs="{...searchAttrs, actionList}" :events="{...searchEvent, ...actionEvent}">
      <template #IsEnabled="rowData">
        <el-switch :value="rowData.row.IsEnabled === 'true' || rowData.row.IsEnabled === true" disabled />
      </template>

      <template #Exec_Flag="rowData">
        <el-switch :value="rowData.row.Exec_Flag === 'true' || rowData.row.Exec_Flag === true" disabled />
      </template>

      <template #IsOnline="rowData">
        <el-switch :value="rowData.row.IsOnline === 'true' || rowData.row.IsOnline === true" disabled />
      </template>


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
    name: "deviceconfig",
    mixins: [searchMixin(TABLE_HEADER), actionMixin(ASSEMBLIES)],
    store,
    components: {
        DialogForm
    }
})
export default class Index extends Vue {
    isActive: boolean = false;

    @Action
    getDicByPlcStep;
    // @State
    getDicByPlcStepData : any=[];
    @Action
    getDicByWcsStep;
    // @State
    getDicByWcsStepData: any=[];
    @Action
    getDicByMode;
    // @State
    getDicByModeData: any=[];

    get SEARCH_DATA() {
        return {
            formProps: {
                "label-width": "75px",
                inline: true
            },
            formItem: {
                "Device_Code":{
                    label: this.$t("deviceconfig.Device_Code"),
                    rules: [],
                    type: "input"
              },
              //   "Device_Name":{
              //       label: this.$t("deviceconfig.Device_Name"),
              //       rules: [],
              //       type: "input"
              // },
              //   "WarehouseId":{
              //       label: this.$t("deviceconfig.WarehouseId"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
                "Plc2WcsStep":{
                    label: this.$t("deviceconfig.Plc2WcsStep"),
                    rules: [],
                    // type: "input"
                    type: "select",
                    children: this.getDicByPlcStepData,
                    props: {
                        clearable: true,
                        placeholder: this.$t("form.all")
                    }
                    // ,isHidden: !this.isActive
              },
                "Wcs2PlcStep":{
                    label: this.$t("deviceconfig.Wcs2PlcStep"),
                    rules: [],
                    type: "select",
                    children: this.getDicByWcsStepData,
                    props: {
                        clearable: true,
                        placeholder: this.$t("form.all")
                    }
                    // ,isHidden: !this.isActive
              },
                "Mode":{
                    label: this.$t("deviceconfig.Mode"),
                    rules: [],
                    type: "select",
                    children: this.getDicByModeData,
                    props: {
                        clearable: true,
                        placeholder: this.$t("form.all")
                    }
                    ,isHidden: !this.isActive
              },
              //   "Config":{
              //       label: this.$t("deviceconfig.Config"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },

            }
        };
    }

     mounted() {
      this.getDicByPlcStep().then((data) => {
          if(data.status==="0"){
            this.getDicByPlcStepData = data.data.items; // 将获取的数据存储到数组中
          }else{
            this.getDicByPlcStepData =[];
          }
      });

      this.getDicByWcsStep().then((data) => {
          if(data.status==="0"){
            this.getDicByWcsStepData = data.data.items; // 将获取的数据存储到数组中
          }else{
            this.getDicByWcsStepData =[];
          }
      });

      this.getDicByMode().then((data) => {
          if(data.status==="0"){
            this.getDicByModeData = data.data.items; // 将获取的数据存储到数组中
          }else{
            this.getDicByModeData =[];
          }
      });
    }
}
</script>
