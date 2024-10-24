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
import { ConnTypeTypes } from "../config";

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
             "Entity.Plc_Code":{
                 label: this.$t("plcconfig.Plc_Code"),
                 rules: [{ required: true, message: this.$t("plcconfig.Plc_Code")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.Plc_Name":{
                 label: this.$t("plcconfig.Plc_Name"),
                 rules: [{ required: true, message: this.$t("plcconfig.Plc_Name")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.IP_Address":{
                 label: this.$t("plcconfig.IP_Address"),
                 rules: [{ required: true, message: this.$t("plcconfig.IP_Address")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.IP_Port":{
                 label: this.$t("plcconfig.IP_Port"),
                 rules: [{ required: true, message: this.$t("plcconfig.IP_Port")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.ConnType":{
                 label: this.$t("plcconfig.ConnType"),
                 rules: [{ required: true, message: this.$t("plcconfig.ConnType")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "select",
                    children: ConnTypeTypes,
                    props: {
                        clearable: true
                    }
            },
             "Entity.IsEnabled":{
                 label: this.$t("plcconfig.IsEnabled"),
                 rules: [{ required: true, message: this.$t("plcconfig.IsEnabled")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "switch"
            },
             "Entity.Scan_Cycle":{
                 label: this.$t("plcconfig.Scan_Cycle"),
                 rules: [{ required: true, message: this.$t("plcconfig.Scan_Cycle")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.Heartbeat_DB":{
                 label: this.$t("plcconfig.Heartbeat_DB"),
                 rules: [{ required: true, message: this.$t("plcconfig.Heartbeat_DB")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.Heartbeat_Address":{
                 label: this.$t("plcconfig.Heartbeat_Address"),
                 rules: [{ required: true, message: this.$t("plcconfig.Heartbeat_Address")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.Heartbeat_Enabled":{
                 label: this.$t("plcconfig.Heartbeat_Enabled"),
                 rules: [{ required: true, message: this.$t("plcconfig.Heartbeat_Enabled")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "switch"
            },
             "Entity.Heartbeat_WriteInterval":{
                 label: this.$t("plcconfig.Heartbeat_WriteInterval"),
                 rules: [{ required: true, message: this.$t("plcconfig.Heartbeat_WriteInterval")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.Describe":{
                 label: this.$t("plcconfig.Describe"),
                 rules: [],
                    type: "input"
            }
                
            }
        };
    }
    beforeOpen() {

    }
}
</script>
