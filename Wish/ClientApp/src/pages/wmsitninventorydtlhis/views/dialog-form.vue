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
             "Entity.difFlag":{
                 label: "difFlag",
                 rules: [{ required: true, message: "difFlag"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.inspectionResult":{
                 label: "inspectionResult",
                 rules: [],
                    type: "input"
            },
             "Entity.inventoryDtlStatus":{
                 label: "inventoryDtlStatus",
                 rules: [{ required: true, message: "inventoryDtlStatus"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.inventoryNo":{
                 label: "inventoryNo",
                 rules: [{ required: true, message: "inventoryNo"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.inventoryQty":{
                 label: "inventoryQty",
                 rules: [{ required: true, message: "inventoryQty"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.materialCode":{
                 label: "materialCode",
                 rules: [{ required: true, message: "materialCode"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.materialName":{
                 label: "materialName",
                 rules: [],
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
             "Entity.qty":{
                 label: "qty",
                 rules: [{ required: true, message: "qty"+this.$t("form.notnull"),trigger: "blur" }],
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
