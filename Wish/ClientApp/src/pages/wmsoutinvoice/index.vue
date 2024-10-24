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
    name: "wmsoutinvoice",
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
                "cvCode":{
                    label: "cvCode",
                    rules: [],
                    type: "input"
              },
                "cvName":{
                    label: "客供名称",
                    rules: [],
                    type: "input"
              },
                "cvType":{
                    label: "cvType",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "docTypeCode":{
                    label: "单据类型",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "externalOutNo":{
                    label: "外部出库单号",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "fpNo":{
                    label: "成品编码",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "inOutTypeNo":{
                    label: "inOutTypeNo",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "invoiceNo":{
                    label: "发货单号",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "invoiceStatus":{
                    label: "发货单状态",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "proprietorCode":{
                    label: "proprietorCode",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "sourceBy":{
                    label: "数据来源",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "ticketNo":{
                    label: "工单号",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "whouseNo":{
                    label: "whouseNo",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "projectNo":{
                    label: "项目号",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "waveNo":{
                    label: "waveNo",
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
