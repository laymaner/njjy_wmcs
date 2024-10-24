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
    name: "wmsitninventoryrecord",
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
                "blindFlag":{
                    label: this.$t("wmsitninventoryrecord.blindFlag"),
                    rules: [],
                    type: "input"
              },
                "confirmBy":{
                    label: this.$t("wmsitninventoryrecord.confirmBy"),
                    rules: [],
                    type: "input"
              },
                "confirmQty":{
                    label: this.$t("wmsitninventoryrecord.confirmQty"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "confirmReason":{
                    label: this.$t("wmsitninventoryrecord.confirmReason"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "differenceFlag":{
                    label: this.$t("wmsitninventoryrecord.differenceFlag"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "docTypeCode":{
                    label: this.$t("wmsitninventoryrecord.docTypeCode"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "erpWhouseNo":{
                    label: this.$t("wmsitninventoryrecord.erpWhouseNo"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "inspectionResult":{
                    label: this.$t("wmsitninventoryrecord.inspectionResult"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "inventoryBy":{
                    label: this.$t("wmsitninventoryrecord.inventoryBy"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "inventoryNo":{
                    label: this.$t("wmsitninventoryrecord.inventoryNo"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "inventoryReason":{
                    label: this.$t("wmsitninventoryrecord.inventoryReason"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "inventoryRecordStatus":{
                    label: this.$t("wmsitninventoryrecord.inventoryRecordStatus"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "materialName":{
                    label: this.$t("wmsitninventoryrecord.materialName"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "materialCode":{
                    label: this.$t("wmsitninventoryrecord.materialCode"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "palletBarcode":{
                    label: this.$t("wmsitninventoryrecord.palletBarcode"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "projectNo":{
                    label: this.$t("wmsitninventoryrecord.projectNo"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "proprietorCode":{
                    label: this.$t("wmsitninventoryrecord.proprietorCode"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "putdownLocNo":{
                    label: this.$t("wmsitninventoryrecord.putdownLocNo"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "skuCode":{
                    label: this.$t("wmsitninventoryrecord.skuCode"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "stockCode":{
                    label: this.$t("wmsitninventoryrecord.stockCode"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "supplierCode":{
                    label: this.$t("wmsitninventoryrecord.supplierCode"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "whouseNo":{
                    label: this.$t("wmsitninventoryrecord.whouseNo"),
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
