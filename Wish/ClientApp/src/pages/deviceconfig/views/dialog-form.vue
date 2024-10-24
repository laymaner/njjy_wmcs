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
    @Action
    getStandardDevice;
    @State
    getStandardDeviceData;
    @Action
    getPlcConfig;
    @State
    getPlcConfigData;

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
             "Entity.Device_Code":{
                 label: this.$t("deviceconfig.Device_Code"),
                 rules: [{ required: true, message: this.$t("deviceconfig.Device_Code")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.Device_Name":{
                 label: this.$t("deviceconfig.Device_Name"),
                 rules: [{ required: true, message: this.$t("deviceconfig.Device_Name")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.WarehouseId":{
                 label: this.$t("deviceconfig.WarehouseId"),
                 rules: [{ required: true, message: this.$t("deviceconfig.WarehouseId")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.StandardDeviceId":{
                 label: this.$t("deviceconfig.StandardDeviceId"),
                 rules: [{ required: true, message: this.$t("deviceconfig.StandardDeviceId")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "select",
                    children: this.getStandardDeviceData,
                    props: {
                        clearable: true
                    }
            },
             "Entity.IsEnabled":{
                 label: this.$t("deviceconfig.IsEnabled"),
                 rules: [{ required: true, message: this.$t("deviceconfig.IsEnabled")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "switch"
            },
             "Entity.Exec_Flag":{
                 label: this.$t("deviceconfig.Exec_Flag"),
                 rules: [{ required: true, message: this.$t("deviceconfig.Exec_Flag")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "switch"
            },
             "Entity.Device_Group":{
                 label: this.$t("deviceconfig.Device_Group"),
                 rules: [],
                    type: "input"
            },
             "Entity.PlcConfigId":{
                 label: this.$t("deviceconfig.PlcConfigId"),
                 rules: [{ required: true, message: this.$t("deviceconfig.PlcConfigId")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "select",
                    children: this.getPlcConfigData,
                    props: {
                        clearable: true
                    }
            },
             "Entity.Plc2WcsStep":{
                 label: this.$t("deviceconfig.Plc2WcsStep"),
                 rules: [{ required: true, message: this.$t("deviceconfig.Plc2WcsStep")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.Wcs2PlcStep":{
                 label: this.$t("deviceconfig.Wcs2PlcStep"),
                 rules: [{ required: true, message: this.$t("deviceconfig.Wcs2PlcStep")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.Mode":{
                 label: this.$t("deviceconfig.Mode"),
                 rules: [{ required: true, message: this.$t("deviceconfig.Mode")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.IsOnline":{
                 label: this.$t("deviceconfig.IsOnline"),
                 rules: [{ required: true, message: this.$t("deviceconfig.IsOnline")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "switch"
            },
             "Entity.Config":{
                 label: this.$t("deviceconfig.Config"),
                 rules: [{ required: true, message: this.$t("deviceconfig.Config")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.Describe":{
                 label: this.$t("deviceconfig.Describe"),
                 rules: [],
                    type: "input"
            }
                
            }
        };
    }
    beforeOpen() {
        this.getStandardDevice();
        this.getPlcConfig();

    }
}
</script>
