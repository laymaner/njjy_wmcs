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
                 label: this.$t("wmsstockdtl.areaNo"),
                 rules: [{ required: true, message: this.$t("wmsstockdtl.areaNo")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.erpWhouseNo":{
                 label: this.$t("wmsstockdtl.erpWhouseNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.inspectionResult":{
                 label: this.$t("wmsstockdtl.inspectionResult"),
                 rules: [],
                    type: "input"
            },
             "Entity.lockFlag":{
                 label: this.$t("wmsstockdtl.lockFlag"),
                 rules: [],
                    type: "input"
            },
             "Entity.lockReason":{
                 label: this.$t("wmsstockdtl.lockReason"),
                 rules: [],
                    type: "input"
            },
             "Entity.materialName":{
                 label: this.$t("wmsstockdtl.materialName"),
                 rules: [],
                    type: "input"
            },
             "Entity.materialCode":{
                 label: this.$t("wmsstockdtl.materialCode"),
                 rules: [{ required: true, message: this.$t("wmsstockdtl.materialCode")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.materialSpec":{
                 label: this.$t("wmsstockdtl.materialSpec"),
                 rules: [],
                    type: "input"
            },
             "Entity.occupyQty":{
                 label: this.$t("wmsstockdtl.occupyQty"),
                 rules: [{ required: true, message: this.$t("wmsstockdtl.occupyQty")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.palletBarcode":{
                 label: this.$t("wmsstockdtl.palletBarcode"),
                 rules: [{ required: true, message: this.$t("wmsstockdtl.palletBarcode")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.projectNo":{
                 label: this.$t("wmsstockdtl.projectNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.projectNoBak":{
                 label: this.$t("wmsstockdtl.projectNoBak"),
                 rules: [],
                    type: "input"
            },
             "Entity.proprietorCode":{
                 label: this.$t("wmsstockdtl.proprietorCode"),
                 rules: [],
                    type: "input"
            },
             "Entity.qty":{
                 label: this.$t("wmsstockdtl.qty"),
                 rules: [{ required: true, message: this.$t("wmsstockdtl.qty")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.skuCode":{
                 label: this.$t("wmsstockdtl.skuCode"),
                 rules: [],
                    type: "input"
            },
             "Entity.stockCode":{
                 label: this.$t("wmsstockdtl.stockCode"),
                 rules: [{ required: true, message: this.$t("wmsstockdtl.stockCode")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.stockDtlStatus":{
                 label: this.$t("wmsstockdtl.stockDtlStatus"),
                 rules: [{ required: true, message: this.$t("wmsstockdtl.stockDtlStatus")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.supplierCode":{
                 label: this.$t("wmsstockdtl.supplierCode"),
                 rules: [],
                    type: "input"
            },
             "Entity.supplierName":{
                 label: this.$t("wmsstockdtl.supplierName"),
                 rules: [],
                    type: "input"
            },
            //  "Entity.supplierNameAlias":{
            //      label: this.$t("wmsstockdtl.supplierNameAlias"),
            //      rules: [],
            //         type: "input"
            // },
            //  "Entity.supplierNameEn":{
            //      label: this.$t("wmsstockdtl.supplierNameEn"),
            //      rules: [],
            //         type: "input"
            // },
             "Entity.whouseNo":{
                 label: this.$t("wmsstockdtl.whouseNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.unitCode":{
                 label: this.$t("wmsstockdtl.unitCode"),
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
