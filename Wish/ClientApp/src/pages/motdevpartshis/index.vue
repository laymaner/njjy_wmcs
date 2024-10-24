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
    name: "motdevpartshis",
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
                "AreaNo":{
                    label: "AreaNo",
                    rules: [],
                    type: "input"
              },
                "DevNo":{
                    label: "DevNo",
                    rules: [],
                    type: "input"
              },
                "PartNo":{
                    label: "PartNo",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "PartLocNo":{
                    label: "PartLocNo",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "DevRunMode":{
                    label: "DevRunMode",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "SrmRoadway":{
                    label: "SrmRoadway",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "SrmForkType":{
                    label: "SrmForkType",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "SrmExecStep":{
                    label: "SrmExecStep",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "IsAlarming":{
                    label: "IsAlarming",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "AlarmCode":{
                    label: "AlarmCode",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "IsFree":{
                    label: "IsFree",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "IsHasGoods":{
                    label: "IsHasGoods",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "CmdNo":{
                    label: "CmdNo",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "PalletNo":{
                    label: "PalletNo",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "OldPalletNo":{
                    label: "OldPalletNo",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "ReadPalletNo":{
                    label: "ReadPalletNo",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "StationNo":{
                    label: "StationNo",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "CurrentX":{
                    label: "CurrentX",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "CurrentY":{
                    label: "CurrentY",
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "CurrentZ":{
                    label: "CurrentZ",
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
