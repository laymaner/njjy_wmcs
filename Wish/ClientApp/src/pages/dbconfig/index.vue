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
    name: "dbconfig",
    mixins: [searchMixin(TABLE_HEADER), actionMixin(ASSEMBLIES)],
    store,
    components: {
        DialogForm
    }
})
export default class Index extends Vue {
    isActive: boolean = false;
    @Action
    getPlcConfig;
    @State
    getPlcConfigData;

    get SEARCH_DATA() {
        return {
            formProps: {
                "label-width": "75px",
                inline: true
            },
            formItem: {
                "Block_Code":{
                    label: this.$t("dbconfig.Block_Code"),
                    rules: [],
                    type: "input"
              },
                "Block_Name":{
                    label: this.$t("dbconfig.Block_Name"),
                    rules: [],
                    type: "input"
              },
                "Block_Offset":{
                    label: this.$t("dbconfig.Block_Offset"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "Block_Length":{
                    label: this.$t("dbconfig.Block_Length"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "PlcConfigId":{
                    label: this.$t("dbconfig.PlcConfigId"),
                    rules: [],
                    type: "select",
                    children: this.getPlcConfigData,
                    props: {
                        clearable: true,
                        placeholder: '全部'
                    }
                    ,isHidden: !this.isActive
              },

            }
        };
    }

     mounted() {
        this.getPlcConfig();

    }
}
</script>
