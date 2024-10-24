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
    name: "baswpallettype",
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
                "barcodeFlag":{
                    label: "barcodeFlag",
                    rules: [],
                    type: "input"
              },
                "developFlag":{
                    label: "developFlag",
                    rules: [],
                    type: "input"
              },
                "palletTypeCode":{
                    label: "palletTypeCode",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "palletTypeFlag":{
                    label: "palletTypeFlag",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "palletTypeName":{
                    label: "palletTypeName",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "palletWeight":{
                    label: "palletWeight",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "palletWidth":{
                    label: "palletWidth",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "positionCol":{
                    label: "positionCol",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "positionDirect":{
                    label: "positionDirect",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "positionFlag":{
                    label: "positionFlag",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "positionRow":{
                    label: "positionRow",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "usedFlag":{
                    label: "usedFlag",
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

            }
        };
    }

     mounted() {

    }
}
</script>
