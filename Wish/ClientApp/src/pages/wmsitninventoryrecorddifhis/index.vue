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
    name: "wmsitninventoryrecorddifhishis",
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
                    label: this.$t("wmsitninventoryrecorddifhis.batchNo"),
                    rules: [],
                    type: "input"
              },
                "dataCode":{
                    label: this.$t("wmsitninventoryrecorddifhis.dataCode"),
                    rules: [],
                    type: "input"
              },
                "delayFrozenFlag":{
                    label: this.$t("wmsitninventoryrecorddifhis.delayFrozenFlag"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "delayTimes":{
                    label: this.$t("wmsitninventoryrecorddifhis.delayTimes"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "driedScrapFlag":{
                    label: this.$t("wmsitninventoryrecorddifhis.driedScrapFlag"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "exposeFrozenFlag":{
                    label: this.$t("wmsitninventoryrecorddifhis.exposeFrozenFlag"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "inspectionResult":{
                    label: this.$t("wmsitninventoryrecorddifhis.inspectionResult"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "inventoryNo":{
                    label: this.$t("wmsitninventoryrecorddifhis.inventoryNo"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "materialName":{
                    label: this.$t("wmsitninventoryrecorddifhis.materialName"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "materialCode":{
                    label: this.$t("wmsitninventoryrecorddifhis.materialCode"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "mslGradeCode":{
                    label: this.$t("wmsitninventoryrecorddifhis.mslGradeCode"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "palletBarcode":{
                    label: this.$t("wmsitninventoryrecorddifhis.palletBarcode"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "positionNo":{
                    label: this.$t("wmsitninventoryrecorddifhis.positionNo"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "projectNo":{
                    label: this.$t("wmsitninventoryrecorddifhis.projectNo"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "proprietorCode":{
                    label: this.$t("wmsitninventoryrecorddifhis.proprietorCode"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "skuCode":{
                    label: this.$t("wmsitninventoryrecorddifhis.skuCode"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "stockCode":{
                    label: this.$t("wmsitninventoryrecorddifhis.stockCode"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "supplierCode":{
                    label: this.$t("wmsitninventoryrecorddifhis.supplierCode"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "uniicode":{
                    label: this.$t("wmsitninventoryrecorddifhis.uniicode"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "whouseNo":{
                    label: this.$t("wmsitninventoryrecorddifhis.whouseNo"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "areaNo":{
                    label: this.$t("wmsitninventoryrecorddifhis.areaNo"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "delFlag":{
                    label: this.$t("wmsitninventoryrecorddifhis.delFlag"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "erpWhouseNo":{
                    label: this.$t("wmsitninventoryrecorddifhis.erpWhouseNo"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "unitCode":{
                    label: this.$t("wmsitninventoryrecorddifhis.unitCode"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "projectNoBak":{
                    label: this.$t("wmsitninventoryrecorddifhis.projectNoBak"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "unpackStatus":{
                    label: this.$t("wmsitninventoryrecorddifhis.unpackStatus"),
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
