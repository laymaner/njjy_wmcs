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
    name: "wmsinreceiptuniicodehis",
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
                "batchNo":{
                    label: "批次",
                    rules: [],
                    type: "input"
              },
                "curPalletBarcode":{
                    label: "当前托盘号",
                    rules: [],
                    type: "input"
              },
                "curPositionNo":{
                    label: "当前位置编号",
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
                "curStockDtlId":{
                    label: "当前库存明细ID",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "dataCode":{
                    label: "DC",
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
                "iqcResultNo":{
                    label: "WMS检验单号",
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
                "productDate":{
                    label: "生产日期",
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
                "projectNo":{
                    label: "项目号",
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
                "qty":{
                    label: "数量",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "uniicode":{
                    label: "包装条码/SN号",
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
                    label: "whouseNo",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "runiiStatus":{
                    label: "入库唯一码状态",
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
