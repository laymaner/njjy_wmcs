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
             "Entity.Device_Code":{
                 label: this.$t("devicetasklog.Device_Code"),
                 rules: [{ required: true, message: this.$t("devicetasklog.Device_Code")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.Direct":{
                 label: this.$t("devicetasklog.Direct"),
                 rules: [{ required: true, message: this.$t("devicetasklog.Direct")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.Task_No":{
                 label: this.$t("devicetasklog.Task_No"),
                 rules: [],
                    type: "input"
            },
             "Entity.Message":{
                 label: this.$t("devicetasklog.Message"),
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
