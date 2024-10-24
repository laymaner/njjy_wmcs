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
    name: "wmsitninventoryrecordhis",
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
                "blindFlag":{
                    label: this.$t("wmsitninventoryrecordhis.blindFlag"),
                    rules: [],
                    type: "input"
              },
                "differenceFlag":{
                    label: this.$t("wmsitninventoryrecordhis.differenceFlag"),
                    rules: [],
                    type: "input"
              },
                "docTypeCode":{
                    label: this.$t("wmsitninventoryrecordhis.docTypeCode"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "erpWhouseNo":{
                    label: this.$t("wmsitninventoryrecordhis.erpWhouseNo"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "inspectionResult":{
                    label: this.$t("wmsitninventoryrecordhis.inspectionResult"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "inventoryBy":{
                    label: this.$t("wmsitninventoryrecordhis.inventoryBy"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "inventoryNo":{
                    label: this.$t("wmsitninventoryrecordhis.inventoryNo"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "inventoryReason":{
                    label: this.$t("wmsitninventoryrecordhis.inventoryReason"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "inventoryRecordStatus":{
                    label: this.$t("wmsitninventoryrecordhis.inventoryRecordStatus"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "materialName":{
                    label: this.$t("wmsitninventoryrecordhis.materialName"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "materialCode":{
                    label: this.$t("wmsitninventoryrecordhis.materialCode"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "palletBarcode":{
                    label: this.$t("wmsitninventoryrecordhis.palletBarcode"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "projectNo":{
                    label: this.$t("wmsitninventoryrecordhis.projectNo"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "proprietorCode":{
                    label: this.$t("wmsitninventoryrecordhis.proprietorCode"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "putdownLocNo":{
                    label: this.$t("wmsitninventoryrecordhis.putdownLocNo"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "skuCode":{
                    label: this.$t("wmsitninventoryrecordhis.skuCode"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "stockCode":{
                    label: this.$t("wmsitninventoryrecordhis.stockCode"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "supplierCode":{
                    label: this.$t("wmsitninventoryrecordhis.supplierCode"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "whouseNo":{
                    label: this.$t("wmsitninventoryrecordhis.whouseNo"),
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
