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
    name: "basbmaterial",
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
                "BarcodeFlag":{
                    label: this.$t("basbmaterial.BarcodeFlag"),
                    rules: [],
                    type: "input"
              },
                "Brand":{
                    label: this.$t("basbmaterial.Brand"),
                    rules: [],
                    type: "input"
              },
                "BuyerCode":{
                    label: this.$t("basbmaterial.BuyerCode"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "BuyerName":{
                    label: this.$t("basbmaterial.BuyerName"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "ErpBinNo":{
                    label: this.$t("basbmaterial.ErpBinNo"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "MaterialCategoryCode":{
                    label: this.$t("basbmaterial.MaterialCategoryCode"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "MaterialCode":{
                    label: this.$t("basbmaterial.MaterialCode"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "MaterialTypeCode":{
                    label: this.$t("basbmaterial.MaterialTypeCode"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "UnitCode":{
                    label: this.$t("basbmaterial.UnitCode"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "WhouseNo":{
                    label: this.$t("basbmaterial.WhouseNo"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "CompanyCode":{
                    label: this.$t("basbmaterial.CompanyCode"),
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
