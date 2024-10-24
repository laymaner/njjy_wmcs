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
                 label: this.$t("wmsstockdtlhis.areaNo"),
                 rules: [{ required: true, message: this.$t("wmsstockdtlhis.areaNo")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.erpWhouseNo":{
                 label: this.$t("wmsstockdtlhis.erpWhouseNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.inspectionResult":{
                 label: this.$t("wmsstockdtlhis.inspectionResult"),
                 rules: [],
                    type: "input"
            },
             "Entity.lockFlag":{
                 label: this.$t("wmsstockdtlhis.lockFlag"),
                 rules: [],
                    type: "input"
            },
             "Entity.lockReason":{
                 label: this.$t("wmsstockdtlhis.lockReason"),
                 rules: [],
                    type: "input"
            },
             "Entity.materialName":{
                 label: this.$t("wmsstockdtlhis.materialName"),
                 rules: [],
                    type: "input"
            },
             "Entity.materialCode":{
                 label: this.$t("wmsstockdtlhis.materialCode"),
                 rules: [{ required: true, message: this.$t("wmsstockdtlhis.materialCode")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.materialSpec":{
                 label: this.$t("wmsstockdtlhis.materialSpec"),
                 rules: [],
                    type: "input"
            },
             "Entity.occupyQty":{
                 label: this.$t("wmsstockdtlhis.occupyQty"),
                 rules: [{ required: true, message: this.$t("wmsstockdtlhis.occupyQty")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.palletBarcode":{
                 label: this.$t("wmsstockdtlhis.palletBarcode"),
                 rules: [{ required: true, message: this.$t("wmsstockdtlhis.palletBarcode")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.projectNo":{
                 label: this.$t("wmsstockdtlhis.projectNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.projectNoBak":{
                 label: this.$t("wmsstockdtlhis.projectNoBak"),
                 rules: [],
                    type: "input"
            },
             "Entity.proprietorCode":{
                 label: this.$t("wmsstockdtlhis.proprietorCode"),
                 rules: [],
                    type: "input"
            },
             "Entity.qty":{
                 label: this.$t("wmsstockdtlhis.qty"),
                 rules: [{ required: true, message: this.$t("wmsstockdtlhis.qty")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.skuCode":{
                 label: this.$t("wmsstockdtlhis.skuCode"),
                 rules: [],
                    type: "input"
            },
             "Entity.stockCode":{
                 label: this.$t("wmsstockdtlhis.stockCode"),
                 rules: [{ required: true, message: this.$t("wmsstockdtlhis.stockCode")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.stockDtlStatus":{
                 label: this.$t("wmsstockdtlhis.stockDtlStatus"),
                 rules: [{ required: true, message: this.$t("wmsstockdtlhis.stockDtlStatus")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.supplierCode":{
                 label: this.$t("wmsstockdtlhis.supplierCode"),
                 rules: [],
                    type: "input"
            },
             "Entity.supplierName":{
                 label: this.$t("wmsstockdtlhis.supplierName"),
                 rules: [],
                    type: "input"
            },
            //  "Entity.supplierNameAlias":{
            //      label: this.$t("wmsstockdtlhis.supplierNameAlias"),
            //      rules: [],
            //         type: "input"
            // },
            //  "Entity.supplierNameEn":{
            //      label: this.$t("wmsstockdtlhis.supplierNameEn"),
            //      rules: [],
            //         type: "input"
            // },
             "Entity.whouseNo":{
                 label: this.$t("wmsstockdtlhis.whouseNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.unitCode":{
                 label: this.$t("wmsstockdtlhis.unitCode"),
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
