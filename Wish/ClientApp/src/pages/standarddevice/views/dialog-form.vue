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
import { DeviceTypeTypes } from "../config";

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
             "Entity.Device_Code":{
                 label: "DeviceInfo.DeviceNo",
                 rules: [{ required: true, message: "DeviceInfo.DeviceNo"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.Device_Name":{
                 label: "DeviceInfo.DeviceName",
                 rules: [{ required: true, message: "DeviceInfo.DeviceName"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.Device_Class":{
                 label: "实现类名",
                 rules: [{ required: true, message: "实现类名"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.DeviceType":{
                 label: "VersionInfo.Type",
                 rules: [{ required: true, message: "VersionInfo.Type"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "select",
                    children: DeviceTypeTypes,
                    props: {
                        clearable: true
                    }
            },
             "Entity.Company":{
                 label: "VersionInfo.Company",
                 rules: [{ required: true, message: "VersionInfo.Company"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.Config":{
                 label: "DeviceInfo.Config",
                 rules: [{ required: true, message: "DeviceInfo.Config"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.Describe":{
                 label: "DeviceInfo.Remark",
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
