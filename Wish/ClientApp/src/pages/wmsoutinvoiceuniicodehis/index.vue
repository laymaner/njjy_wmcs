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
    name: "wmsoutinvoiceuniicodehis",
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
                    label: "批次号",
                    rules: [],
                    type: "input"
              },
                "dataCode":{
                    label: "DC",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "erpBinNo":{
                    label: "erpBinNo",
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
                "expDate":{
                    label: "失效日期",
                    rules: [],
                    type: "datePicker",
                    span: 12,
                    props: {
                            type: "datetimerange",
                        "value-format": "yyyy-MM-dd HH:mm:ss",
                        "range-separator": "-",
                        "start-placeholder": this.$t("table.startdate"),
                        "end-placeholder": this.$t("table.enddate")
                    }
                    ,isHidden: !this.isActive
              },
                "inspectionResult":{
                    label: "质检结果",
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
                "palletBarcode":{
                    label: "载体条码",
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
                "skuCode":{
                    label: "skuCode",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "stockCode":{
                    label: "stockCode",
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
                "supplierName":{
                    label: "供应商名称",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "uniicode":{
                    label: "包装条码/SN码",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "waveNo":{
                    label: "波次号",
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
                "outBarcode":{
                    label: "出库条码",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "ouniiStatus":{
                    label: "ouniiStatus",
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
