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
    name: "devicealarmlog",
    mixins: [searchMixin(TABLE_HEADER), actionMixin(ASSEMBLIES)],
    store,
    components: {
        DialogForm
    }
})
export default class Index extends Vue {
    isActive: boolean = false;
    @Action
    getDicByStatus;
    // @State
    getDicByStatusData : any=[];
    get SEARCH_DATA() {
        return {
            formProps: {
                "label-width": "75px",
                inline: true
            },
            formItem: {
                "Device_Code":{
                    label: this.$t("devicealarmlog.Device_Code"),
                    rules: [],
                    type: "input"
              },
                "Message":{
                    label: this.$t("devicealarmlog.Message"),
                    rules: [],
                    type: "input"
              },
              "HandleFlag":{
                    label: this.$t("devicealarmlog.HandleFlag"),
                    rules: [],
                    type: "select",
                    children: this.getDicByStatusData,
                    props: {
                        clearable: true,
                        placeholder: this.$t("form.all")
                    }
              },
                "OriginTime":{
                    label: this.$t("devicealarmlog.OriginTime"),
                    rules: [],
                    type: "datePicker",
                    span: 12,
                    props: {
                            type: "datetimerange",
                        "value-format": "yyyy-MM-dd HH:mm:ss",
                        "range-separator": "-",
                        "start-placeholder": this.$t("table.startdate"),
                        "end-placeholder": this.$t("table.enddate")
                    }
                    ,isHidden: !this.isActive
              },
                "EndTime":{
                    label: this.$t("devicealarmlog.EndTime"),
                    rules: [],
                    type: "datePicker",
                    span: 12,
                    props: {
                            type: "datetimerange",
                        "value-format": "yyyy-MM-dd HH:mm:ss",
                        "range-separator": "-",
                        "start-placeholder": this.$t("table.startdate"),
                        "end-placeholder": this.$t("table.enddate")
                    }
                    ,isHidden: !this.isActive
              },
            }
        };
    }

     mounted() {

    }
}
</script>
