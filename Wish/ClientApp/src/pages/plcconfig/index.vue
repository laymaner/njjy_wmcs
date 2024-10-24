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

      <template #Heartbeat_Enabled="rowData">
        <el-switch :value="rowData.row.Heartbeat_Enabled === 'true' || rowData.row.Heartbeat_Enabled === true" disabled />
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
import { ASSEMBLIES, TABLE_HEADER, ConnTypeTypes,UsedFlagTypes } from "./config";
import i18n from "@/lang";

@Component({
    name: "plcconfig",
    mixins: [searchMixin(TABLE_HEADER), actionMixin(ASSEMBLIES)],
    store,
    components: {
        DialogForm
    }
})
export default class Index extends Vue {
    isActive: boolean = false;

    get SEARCH_DATA() {
        return {
            formProps: {
                "label-width": "75px",
                inline: true
            },
            formItem: {
                "Plc_Code":{
                    label: this.$t("plcconfig.Plc_Code"),
                    rules: [],
                    type: "input"
              },
                "Plc_Name":{
                    label: this.$t("plcconfig.Plc_Name"),
                    rules: [],
                    type: "input"
              },
                "IP_Address":{
                    label: this.$t("plcconfig.IP_Address"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "IP_Port":{
                    label: this.$t("plcconfig.IP_Port"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "ConnType":{
                    label: this.$t("plcconfig.ConnType"),
                    rules: [],
                    type: "select",
                    children: ConnTypeTypes,
                    props: {
                        clearable: true,
                        placeholder: this.$t("form.all")
                    }
                    ,isHidden: !this.isActive
              },
              //   "IsConnect":{
              //       label: this.$t("plcconfig.IsConnect"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
                "IsEnabled":{
                    label: this.$t("plcconfig.IsEnabled"),
                    rules: [],
                    type: "select",
                    children: UsedFlagTypes,
                    props: {
                        clearable: true,
                        placeholder: this.$t("form.all")
                    }
                    // ,isHidden: !this.isActive
              },
                "Heartbeat_DB":{
                    label: this.$t("plcconfig.Heartbeat_DB"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },

            }
        };
    }

     mounted() {

    }
}
</script>
