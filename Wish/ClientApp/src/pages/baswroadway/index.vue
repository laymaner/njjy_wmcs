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
    name: "baswroadway",
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
                    label: this.$t("baswroadway.areaNo"),
                    rules: [],
                    type: "input"
              },
                "errFlag":{
                    label: this.$t("baswroadway.errFlag"),
                    rules: [],
                    type: "input"
              },
                "regionNo":{
                    label: this.$t("baswroadway.regionNo"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "roadwayName":{
                    label: this.$t("baswroadway.roadwayName"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "roadwayNo":{
                    label: this.$t("baswroadway.roadwayNo"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "usedFlag":{
                    label: this.$t("baswroadway.usedFlag"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "virtualFlag":{
                    label: this.$t("baswroadway.virtualFlag"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "whouseNo":{
                    label: this.$t("baswroadway.whouseNo"),
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
