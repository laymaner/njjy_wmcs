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
             "Entity.areaNo":{
                 label: "areaNo",
                 rules: [{ required: true, message: "areaNo"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.batchNo":{
                 label: "batchNo",
                 rules: [],
                    type: "input"
            },
             "Entity.confirmQty":{
                 label: "confirmQty",
                 rules: [{ required: true, message: "confirmQty"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.erpWhouseNo":{
                 label: "erpWhouseNo",
                 rules: [{ required: true, message: "erpWhouseNo"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.inspectionResult":{
                 label: "inspectionResult",
                 rules: [{ required: true, message: "inspectionResult"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.itnQcDtlStatus":{
                 label: "itnQcDtlStatus",
                 rules: [{ required: true, message: "itnQcDtlStatus"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.itnQcNo":{
                 label: "itnQcNo",
                 rules: [{ required: true, message: "itnQcNo"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.itnQcQty":{
                 label: "itnQcQty",
                 rules: [{ required: true, message: "itnQcQty"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.materialName":{
                 label: "materialName",
                 rules: [],
                    type: "input"
            },
             "Entity.materialCode":{
                 label: "materialCode",
                 rules: [{ required: true, message: "materialCode"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.materialSpec":{
                 label: "materialSpec",
                 rules: [],
                    type: "input"
            },
             "Entity.proprietorCode":{
                 label: "proprietorCode",
                 rules: [],
                    type: "input"
            },
             "Entity.unitCode":{
                 label: "unitCode",
                 rules: [],
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
