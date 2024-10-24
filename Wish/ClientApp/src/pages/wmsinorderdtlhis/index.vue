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
    name: "wmsinorderdtlhis",
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
                "batchNo":{
                    label: "批次",
                    rules: [],
                    type: "input"
              },
                "companyCode":{
                    label: "公司代码",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "departmentName":{
                    label: "部门名称",
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
                "inDtlStatus":{
                    label: "入库单明细状态",
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
                "inspector":{
                    label: "质检员",
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
                "minPkgQty":{
                    label: "包装数量",
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
                "purchaseOrderId":{
                    label: "采购订单行号",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "purchaseOrderMaker":{
                    label: "采购订单制单人",
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
                "unitCode":{
                    label: "单位",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "whouseNo":{
                    label: "仓库号",
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
