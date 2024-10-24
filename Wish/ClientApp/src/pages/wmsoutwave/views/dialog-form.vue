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
                 label: "分配操作人",
                 rules: [],
                    type: "input"
            },
             "Entity.allotTime":{
                 label: "分配开始时间",
                 rules: [],
                    type: "datePicker"
            },
             "Entity.deliveryLocNo":{
                 label: "deliveryLocNo",
                 rules: [],
                    type: "input"
            },
             "Entity.docTypeCode":{
                 label: "单据类型",
                 rules: [{ required: true, message: "单据类型"+this.$t("form.notnull"),trigger: "blur" }],
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
                 label: "波次号",
                 rules: [{ required: true, message: "波次号"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.waveStatus":{
                 label: "波次单状态",
                 rules: [{ required: true, message: "波次单状态"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.waveType":{
                 label: "波次类型",
                 rules: [{ required: true, message: "波次类型"+this.$t("form.notnull"),trigger: "blur" }],
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
