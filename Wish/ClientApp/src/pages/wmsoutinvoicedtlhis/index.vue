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
    name: "wmsoutinvoicedtlhis",
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
                "allocatResult":{
                    label: "allocatResult",
                    rules: [],
                    type: "input"
              },
                "allotQty":{
                    label: "allotQty",
                    rules: [],
                    type: "input"
              },
                "areaNo":{
                    label: "areaNo",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "batchNo":{
                    label: "batchNo",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "erpWhouseNo":{
                    label: "erpWhouseNo",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "externalOutNo":{
                    label: "externalOutNo",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "invoiceDtlStatus":{
                    label: "invoiceDtlStatus",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "invoiceNo":{
                    label: "invoiceNo",
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
                "orderNo":{
                    label: "orderNo",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "productDeptCode":{
                    label: "productDeptCode",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "productDeptName":{
                    label: "productDeptName",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "projectNo":{
                    label: "projectNo",
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
                "productSn":{
                    label: "productSn",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "supplyType":{
                    label: "supplyType",
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
                "whouseNo":{
                    label: "whouseNo",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "companyCode":{
                    label: "companyCode",
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
                "ticketNo":{
                    label: "ticketNo",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "unitCode":{
                    label: "单位编码",
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
