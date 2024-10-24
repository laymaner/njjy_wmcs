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
             "Entity.differQty":{
                 label: "differQty",
                 rules: [{ required: true, message: "differQty"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.erpStockNo":{
                 label: "erpStockNo",
                 rules: [],
                    type: "input"
            },
             "Entity.erpStockQty":{
                 label: "erpStockQty",
                 rules: [{ required: true, message: "erpStockQty"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.erpWhouseNo":{
                 label: "erpWhouseNo",
                 rules: [{ required: true, message: "erpWhouseNo"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.materialCategoryCode":{
                 label: "materialCategoryCode",
                 rules: [],
                    type: "input"
            },
             "Entity.materialCode":{
                 label: "materialCode",
                 rules: [{ required: true, message: "materialCode"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.materialTypeCode":{
                 label: "materialTypeCode",
                 rules: [],
                    type: "input"
            },
             "Entity.proprietorCode":{
                 label: "proprietorCode",
                 rules: [],
                    type: "input"
            },
             "Entity.reconcileNo":{
                 label: "reconcileNo",
                 rules: [{ required: true, message: "reconcileNo"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.reconcileOperator":{
                 label: "reconcileOperator",
                 rules: [],
                    type: "input"
            },
             "Entity.reconcileTime":{
                 label: "reconcileTime",
                 rules: [],
                    type: "datePicker"
            },
             "Entity.whouseNo":{
                 label: "whouseNo",
                 rules: [],
                    type: "input"
            },
             "Entity.wmsStockQty":{
                 label: "wmsStockQty",
                 rules: [{ required: true, message: "wmsStockQty"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            }
                
            }
        };
    }
    beforeOpen() {

    }
}
</script>
