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
    name: "wmsinreceiptiqcrecordhis",
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
                "areaNo":{
                    label: "楼号",
                    rules: [],
                    type: "input"
              },
                "badDescription":{
                    label: "不良说明",
                    rules: [],
                    type: "input"
              },
                "badOptions":{
                    label: "不良选项",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "badSolveType":{
                    label: "不良处理方式",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "binNo":{
                    label: "库位编码",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "docTypeCode":{
                    label: "docTypeCode",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "erpWhouseNo":{
                    label: "ERP仓库",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "externalInNo":{
                    label: "外部入库单号",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "inNo":{
                    label: "WMS入库单号",
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
                "iqcRecordNo":{
                    label: "WMS检验记录单号",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "iqcRecordStatus":{
                    label: "检验结果状态",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "iqcType":{
                    label: "质检方式",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "materialName":{
                    label: "物料名称",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "materialCode":{
                    label: "物料编码",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "materialSpec":{
                    label: "规格",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "orderNo":{
                    label: "关联单号",
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
                "proprietorCode":{
                    label: "proprietorCode",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "receiptNo":{
                    label: "WMS收货单号",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "receiptQty":{
                    label: "收货完成数量",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "regionNo":{
                    label: "库区编码",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "supplierName":{
                    label: "供应商名称",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "supplierCode":{
                    label: "供应商编码",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "urgentFlag":{
                    label: "急料标记",
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

            }
        };
    }

     mounted() {

    }
}
</script>
