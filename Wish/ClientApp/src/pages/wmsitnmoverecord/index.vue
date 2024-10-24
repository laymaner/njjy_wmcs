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
    name: "wmsitnmoverecord",
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
                "barcodeFlag":{
                    label: "码级管理",
                    rules: [],
                    type: "input"
              },
                "batchNo":{
                    label: "批次",
                    rules: [],
                    type: "input"
              },
                "curLocationNo":{
                    label: "curLocationNo",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "curLocationType":{
                    label: "curLocationType",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "curPalletbarCode":{
                    label: "curPalletbarCode",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "curStockCode":{
                    label: "当前库存编码",
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
                "frLocationNo":{
                    label: "来源站台/库位号",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "frLocationType":{
                    label: "来源位置类型",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "frPalletBarcode":{
                    label: "来源托盘条码",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "frRegionNo":{
                    label: "frRegionNo",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "frStockCode":{
                    label: "frStockCode",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "inspectionResult":{
                    label: "inspectionResult",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "materialCode":{
                    label: "materialCode",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "moveNo":{
                    label: "moveNo",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "moveRecordStatus":{
                    label: "moveRecordStatus",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "pickMethod":{
                    label: "pickMethod",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "pickType":{
                    label: "pickType",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "putDownLocNo":{
                    label: "putDownLocNo",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "skuCode":{
                    label: "skuCode",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "supplierBatchNo":{
                    label: "supplierBatchNo",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "supplierCode":{
                    label: "supplierCode",
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
                "supplierType":{
                    label: "供货方类型：供应商、产线",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "toRegionNo":{
                    label: "目标库区",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "toStockCode":{
                    label: "目标库存编码",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "unitCode":{
                    label: "unitCode",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "proprietorCode":{
                    label: "货主",
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
