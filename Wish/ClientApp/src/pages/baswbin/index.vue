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
    name: "baswbin",
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
                "binNo":{
                    label: this.$t("baswbin.binNo"),
                    rules: [],
                    type: "input"
              },
                "binCol":{
                    label: this.$t("baswbin.binCol"),
                    rules: [],
                    type: "input"
              },"binRow":{
                    label: this.$t("baswbin.binRow"),
                    rules: [],
                    type: "input"
                    // ,isHidden: !this.isActive
              },
              "binLayer":{
                    label: this.$t("baswbin.binLayer"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "binErrFlag":{
                    label: this.$t("baswbin.binErrFlag"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "binHeight":{
                    label: this.$t("baswbin.binHeight"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                
                "binLength":{
                    label: this.$t("baswbin.binLength"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "binName":{
                    label: this.$t("baswbin.binName"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "areaNo":{
                    label: this.$t("baswbin.areaNo"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                
                "binType":{
                    label: this.$t("baswbin.binType"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "binWidth":{
                    label: this.$t("baswbin.binWidth"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "fireFlag":{
                    label: this.$t("baswbin.fireFlag"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "rackNo":{
                    label: this.$t("baswbin.rackNo"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "regionNo":{
                    label: this.$t("baswbin.regionNo"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "roadwayNo":{
                    label: this.$t("baswbin.roadwayNo"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "usedFlag":{
                    label: this.$t("baswbin.usedFlag"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "virtualFlag":{
                    label: this.$t("baswbin.virtualFlag"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "whouseNo":{
                    label: this.$t("baswbin.whouseNo"),
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
