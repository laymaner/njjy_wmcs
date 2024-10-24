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
    name: "srmcmd",
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

    @Action
    getDicByType;
    // @State
    getDicByTypeData : any=[];

    get SEARCH_DATA() {
        return {
            formProps: {
                "label-width": "75px",
                inline: true
            },
            formItem: {
                "SubTask_No":{
                    label: this.$t("srmcmd.SubTask_No"),
                    rules: [],
                    type: "input"//,
                    //style: 'width: 200px;' // 设置宽度为 200 像素
              },
                "Task_No":{
                    label: this.$t("srmcmd.Task_No"),
                    rules: [],
                    type: "input"
              },
              "Pallet_Barcode":{
                    label: this.$t("srmcmd.Pallet_Barcode"),
                    rules: [],
                    type: "input"
                    // ,isHidden: !this.isActive
              },
              "WaferID":{
                    label: this.$t("srmcmd.WaferID"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "Device_No":{
                    label: this.$t("srmcmd.Device_No"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "Check_Point":{
                    label: this.$t("srmcmd.Check_Point"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "Task_Type":{
                    label: this.$t("srmcmd.Task_Type"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
              "Task_Cmd":{
                    label: this.$t("srmcmd.Task_Cmd"),
                    rules: [],
                    type: "select",
                    children: this.getDicByTypeData,
                    props: {
                        clearable: true,
                        placeholder: this.$t("form.all")
                    }
                    ,isHidden: !this.isActive
              },
                "Exec_Status":{
                    label: this.$t("srmcmd.Exec_Status"),
                    rules: [],
                    type: "select",
                    children: this.getDicByStatusData,
                    props: {
                        clearable: true,
                        placeholder: this.$t("form.all")
                    }
                    ,isHidden: !this.isActive
              },
                "From_Station":{
                    label: this.$t("srmcmd.From_Station"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "From_ForkDirection":{
                    label: this.$t("srmcmd.From_ForkDirection"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "From_Column":{
                    label: this.$t("srmcmd.From_Column"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "From_Layer":{
                    label: this.$t("srmcmd.From_Layer"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
            //     "From_Deep":{
            //         label: this.$t("srmcmd.From_Deep"),
            //         rules: [],
            //         type: "input"
            //         ,isHidden: !this.isActive
            //   },
                "To_Station":{
                    label: this.$t("srmcmd.To_Station"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "To_ForkDirection":{
                    label: this.$t("srmcmd.To_ForkDirection"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "To_Column":{
                    label: this.$t("srmcmd.To_Column"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "To_Layer":{
                    label: this.$t("srmcmd.To_Layer"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
            //     "To_Deep":{
            //         label: this.$t("srmcmd.To_Deep"),
            //         rules: [],
            //         type: "input"
            //         ,isHidden: !this.isActive
            //   },
                "Recive_Date":{
                    label: this.$t("srmcmd.Recive_Date"),
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
                "Begin_Date":{
                    label: this.$t("srmcmd.Begin_Date"),
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
                "Pick_Date":{
                    label: this.$t("srmcmd.Pick_Date"),
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
                "Put_Date":{
                    label: this.$t("srmcmd.Put_Date"),
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
                "Finish_Date":{
                    label: this.$t("srmcmd.Finish_Date"),
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
                    // ,// 添加 添加 picker-options 来设置日期选择器宽度
                    // pickerOptions: {
                    //     style: { width: '300px' }
                    // }
                    ,isHidden: !this.isActive
              },

            }
        };
    }

     mounted() {
        this.getDicByStatus().then((data) => {
            if(data.status==="0"){
                this.getDicByStatusData = data.data.items; // 将获取的数据存储到数组中
            }else{
                this.getDicByStatusData =[];
            }
        });
        this.getDicByType().then((data) => {
            if(data.status==="0"){
                this.getDicByTypeData = data.data.items; // 将获取的数据存储到数组中
            }else{
                this.getDicByTypeData =[];
            }
        });
    }
}
</script>
