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
        <!-- 弹出框 -->
        <dialog-form-create :is-show.sync="dialogcreateIsShow" :status="dialogStatus" @onSearch="onHoldSearch" />
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
import DialogFormCreate from "./views/dialog-form-create.vue";
import store from "./store/index";
// 查询参数, table列 ★★★★★
import { ASSEMBLIES, TABLE_HEADER,  } from "./config";
import i18n from "@/lang";

@Component({
    name: "wmsoutinvoicedtl",
    mixins: [searchMixin(TABLE_HEADER), actionMixin(ASSEMBLIES)],
    store,
    components: {
        DialogForm,
        DialogFormCreate
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
                    label: this.$t("wmsoutinvoicedtl.allocatResult"),
                    rules: [],
                    type: "input"
              },
                "areaNo":{
                    label: this.$t("wmsoutinvoicedtl.areaNo"),
                    rules: [],
                    type: "input"
              },
                "batchNo":{
                    label: this.$t("wmsoutinvoicedtl.batchNo"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "erpWhouseNo":{
                    label: this.$t("wmsoutinvoicedtl.erpWhouseNo"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "externalOutNo":{
                    label: this.$t("wmsoutinvoicedtl.externalOutNo"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "invoiceDtlStatus":{
                    label: this.$t("wmsoutinvoicedtl.invoiceDtlStatus"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "invoiceNo":{
                    label: this.$t("wmsoutinvoicedtl.invoiceNo"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "materialCode":{
                    label: this.$t("wmsoutinvoicedtl.materialCode"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "orderNo":{
                    label: this.$t("wmsoutinvoicedtl.orderNo"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "projectNo":{
                    label: this.$t("wmsoutinvoicedtl.projectNo"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "productSn":{
                    label: this.$t("wmsoutinvoicedtl.productSn"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "waveNo":{
                    label: this.$t("wmsoutinvoicedtl.waveNo"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "whouseNo":{
                    label: this.$t("wmsoutinvoicedtl.whouseNo"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "companyCode":{
                    label: this.$t("wmsoutinvoicedtl.companyCode"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "supplierCode":{
                    label: this.$t("wmsoutinvoicedtl.supplierCode"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "ticketNo":{
                    label: this.$t("wmsoutinvoicedtl.ticketNo"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "unitCode":{
                    label: this.$t("wmsoutinvoicedtl.unitCode"),
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
