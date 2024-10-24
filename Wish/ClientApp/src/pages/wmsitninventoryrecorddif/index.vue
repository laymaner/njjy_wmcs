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
    name: "wmsitninventoryrecorddif",
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
                    label: this.$t("wmsitninventoryrecorddif.batchNo"),
                    rules: [],
                    type: "input"
              },
                "dataCode":{
                    label: this.$t("wmsitninventoryrecorddif.dataCode"),
                    rules: [],
                    type: "input"
              },
                "delayFrozenFlag":{
                    label: this.$t("wmsitninventoryrecorddif.delayFrozenFlag"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "delayTimes":{
                    label: this.$t("wmsitninventoryrecorddif.delayTimes"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "difQty":{
                    label: this.$t("wmsitninventoryrecorddif.difQty"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "driedScrapFlag":{
                    label: this.$t("wmsitninventoryrecorddif.driedScrapFlag"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "exposeFrozenFlag":{
                    label: this.$t("wmsitninventoryrecorddif.exposeFrozenFlag"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "exposeFrozenReason":{
                    label: this.$t("wmsitninventoryrecorddif.exposeFrozenReason"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "inspectionResult":{
                    label: this.$t("wmsitninventoryrecorddif.inspectionResult"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "inventoryDtlId":{
                    label: this.$t("wmsitninventoryrecorddif.inventoryDtlId"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "inventoryNo":{
                    label: this.$t("wmsitninventoryrecorddif.inventoryNo"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "inventoryQty":{
                    label: this.$t("wmsitninventoryrecorddif.inventoryQty"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "materialName":{
                    label: this.$t("wmsitninventoryrecorddif.materialName"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "materialCode":{
                    label: this.$t("wmsitninventoryrecorddif.materialCode"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "mslGradeCode":{
                    label: this.$t("wmsitninventoryrecorddif.mslGradeCode"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "palletBarcode":{
                    label: this.$t("wmsitninventoryrecorddif.palletBarcode"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "positionNo":{
                    label: this.$t("wmsitninventoryrecorddif.positionNo"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "projectNo":{
                    label: this.$t("wmsitninventoryrecorddif.projectNo"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "proprietorCode":{
                    label: this.$t("wmsitninventoryrecorddif.proprietorCode"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "skuCode":{
                    label: this.$t("wmsitninventoryrecorddif.skuCode"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "stockCode":{
                    label: this.$t("wmsitninventoryrecorddif.stockCode"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "supplierCode":{
                    label: this.$t("wmsitninventoryrecorddif.supplierCode"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "supplierName":{
                    label: this.$t("wmsitninventoryrecorddif.supplierName"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "uniicode":{
                    label: this.$t("wmsitninventoryrecorddif.uniicode"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "unpackTime":{
                    label: this.$t("wmsitninventoryrecorddif.unpackTime"),
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
                "whouseNo":{
                    label: this.$t("wmsitninventoryrecorddif.whouseNo"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "areaNo":{
                    label: this.$t("wmsitninventoryrecorddif.areaNo"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "delFlag":{
                    label: this.$t("wmsitninventoryrecorddif.delFlag"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "erpWhouseNo":{
                    label: this.$t("wmsitninventoryrecorddif.erpWhouseNo"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "unitCode":{
                    label: this.$t("wmsitninventoryrecorddif.unitCode"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "projectNoBak":{
                    label: this.$t("wmsitninventoryrecorddif.projectNoBak"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "unpackStatus":{
                    label: this.$t("wmsitninventoryrecorddif.unpackStatus"),
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
