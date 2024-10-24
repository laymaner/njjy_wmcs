<template>
    <wtm-dialog-box :is-show.sync="isShow" :status="status" :events="formEvent">
        <wtm-create-form :ref="refName" :status="status" :options="formOptions" ></wtm-create-form>
    </wtm-dialog-box>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { Action, State } from "vuex-class";
import formMixin from "@/vue-custom/mixin/form-mixin";
import UploadImg from "@/components/page/UploadImg.vue";


@Component({
    mixins: [formMixin()]
})
export default class Index extends Vue {

    // 表单结构
    get formOptions() {
        const filterMethod = (query, item) => {
            return item.label.indexOf(query) > -1;
        };
        return {
            formProps: {
                "label-width": "100px"
            },
            formItem: {
                "Entity.ID": {
                    isHidden: true
                },
             "Entity.SubTask_No":{
                 label: this.$t("srmcmd.SubTask_No"),
                 rules: [{ required: true, message: this.$t("srmcmd.SubTask_No")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.Task_No":{
                 label: this.$t("srmcmd.Task_No"),
                 rules: [{ required: true, message: this.$t("srmcmd.Task_No")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.Serial_No":{
                 label: this.$t("srmcmd.Serial_No"),
                 rules: [{ required: true, message: this.$t("srmcmd.Serial_No")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.Device_No":{
                 label: this.$t("srmcmd.Device_No"),
                 rules: [],
                    type: "input"
            },
             "Entity.Fork_No":{
                 label: this.$t("srmcmd.Fork_No"),
                 rules: [],
                    type: "input"
            },
             "Entity.Station_Type":{
                 label: this.$t("srmcmd.Station_Type"),
                 rules: [],
                    type: "input"
            },
             "Entity.Check_Point":{
                 label: this.$t("srmcmd.Check_Point"),
                 rules: [{ required: true, message: this.$t("srmcmd.Check_Point")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.Task_Cmd":{
                 label: this.$t("srmcmd.Task_Cmd"),
                 rules: [{ required: true, message: this.$t("srmcmd.Task_Cmd")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.Task_Type":{
                 label: this.$t("srmcmd.Task_Type"),
                 rules: [{ required: true, message: this.$t("srmcmd.Task_Type")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.Pallet_Barcode":{
                 label: this.$t("srmcmd.Pallet_Barcode"),
                 rules: [{ required: true, message: this.$t("srmcmd.Pallet_Barcode")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.Exec_Status":{
                 label: this.$t("srmcmd.Exec_Status"),
                 rules: [{ required: true, message: this.$t("srmcmd.Exec_Status")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.From_Station":{
                 label: this.$t("srmcmd.From_Station"),
                 rules: [{ required: true, message: this.$t("srmcmd.From_Station")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.From_ForkDirection":{
                 label: this.$t("srmcmd.From_ForkDirection"),
                 rules: [{ required: true, message: this.$t("srmcmd.From_ForkDirection")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.From_Column":{
                 label: this.$t("srmcmd.From_Column"),
                 rules: [{ required: true, message: this.$t("srmcmd.From_Column")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.From_Layer":{
                 label: this.$t("srmcmd.From_Layer"),
                 rules: [{ required: true, message: this.$t("srmcmd.From_Layer")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.From_Deep":{
                 label: this.$t("srmcmd.From_Deep"),
                 rules: [{ required: true, message: this.$t("srmcmd.From_Deep")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.To_Station":{
                 label: this.$t("srmcmd.To_Station"),
                 rules: [{ required: true, message: this.$t("srmcmd.To_Station")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.To_ForkDirection":{
                 label: this.$t("srmcmd.To_ForkDirection"),
                 rules: [{ required: true, message: this.$t("srmcmd.To_ForkDirection")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.To_Column":{
                 label: this.$t("srmcmd.To_Column"),
                 rules: [{ required: true, message: this.$t("srmcmd.To_Column")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.To_Layer":{
                 label: this.$t("srmcmd.To_Layer"),
                 rules: [{ required: true, message: this.$t("srmcmd.To_Layer")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.To_Deep":{
                 label: this.$t("srmcmd.To_Deep"),
                 rules: [{ required: true, message: this.$t("srmcmd.To_Deep")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.Recive_Date":{
                 label: this.$t("srmcmd.Recive_Date"),
                 rules: [],
                    type: "datePicker"
            },
             "Entity.Begin_Date":{
                 label: this.$t("srmcmd.Begin_Date"),
                 rules: [],
                    type: "datePicker"
            },
             "Entity.Pick_Date":{
                 label: this.$t("srmcmd.Pick_Date"),
                 rules: [],
                    type: "datePicker"
            },
             "Entity.Put_Date":{
                 label: this.$t("srmcmd.Put_Date"),
                 rules: [],
                    type: "datePicker"
            },
             "Entity.Finish_Date":{
                 label: this.$t("srmcmd.Finish_Date"),
                 rules: [],
                    type: "datePicker"
            }
                
            }
        };
    }
    beforeOpen() {

    }
}
</script>
