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
    name: "wmsstockhis",
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
                    label: this.$t("wmsstockhis.areaNo"),
                    rules: [],
                    type: "input"
              },
                "binNo":{
                    label: this.$t("wmsstockhis.binNo"),
                    rules: [],
                    type: "input"
              },
                "invoiceNo":{
                    label: this.$t("wmsstockhis.invoiceNo"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "locNo":{
                    label: this.$t("wmsstockhis.locNo"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "palletBarcode":{
                    label: this.$t("wmsstockhis.palletBarcode"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "regionNo":{
                    label: this.$t("wmsstockhis.regionNo"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "roadwayNo":{
                    label: this.$t("wmsstockhis.roadwayNo"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "stockCode":{
                    label: this.$t("wmsstockhis.stockCode"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "stockStatus":{
                    label: this.$t("wmsstockhis.stockStatus"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "whouseNo":{
                    label: this.$t("wmsstockhis.whouseNo"),
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
