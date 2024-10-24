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
             "Entity.allotFlag":{
                 label: "allotFlag",
                 rules: [],
                    type: "input"
            },
             "Entity.allotOperator":{
                 label: "allotOperator",
                 rules: [],
                    type: "input"
            },
             "Entity.allotTime":{
                 label: "allotTime",
                 rules: [],
                    type: "datePicker"
            },
             "Entity.deliveryLocNo":{
                 label: "deliveryLocNo",
                 rules: [],
                    type: "input"
            },
             "Entity.docTypeCode":{
                 label: "docTypeCode",
                 rules: [{ required: true, message: "docTypeCode"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.issuedFlag":{
                 label: "issuedFlag",
                 rules: [],
                    type: "input"
            },
             "Entity.issuedOperator":{
                 label: "issuedOperator",
                 rules: [],
                    type: "input"
            },
             "Entity.issuedResult":{
                 label: "issuedResult",
                 rules: [],
                    type: "input"
            },
             "Entity.issuedTime":{
                 label: "issuedTime",
                 rules: [],
                    type: "datePicker"
            },
             "Entity.operationReason":{
                 label: "operationReason",
                 rules: [],
                    type: "input"
            },
             "Entity.proprietorCode":{
                 label: "proprietorCode",
                 rules: [],
                    type: "input"
            },
             "Entity.waveNo":{
                 label: "waveNo",
                 rules: [{ required: true, message: "waveNo"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.waveStatus":{
                 label: "waveStatus",
                 rules: [{ required: true, message: "waveStatus"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.waveType":{
                 label: "waveType",
                 rules: [{ required: true, message: "waveType"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.whouseNo":{
                 label: "whouseNo",
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
