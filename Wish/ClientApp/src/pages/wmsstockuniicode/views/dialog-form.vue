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
                 label: this.$t("wmsstockuniicode.areaNo"),
                 rules: [{ required: true, message: this.$t("wmsstockuniicode.areaNo")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.batchNo":{
                 label: this.$t("wmsstockuniicode.batchNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.dataCode":{
                 label: this.$t("wmsstockuniicode.dataCode"),
                 rules: [],
                    type: "input"
            },
             "Entity.delayFrozenFlag":{
                 label: this.$t("wmsstockuniicode.delayFrozenFlag"),
                 rules: [],
                    type: "input"
            },
             "Entity.delayFrozenReason":{
                 label: this.$t("wmsstockuniicode.delayFrozenReason"),
                 rules: [],
                    type: "input"
            },
             "Entity.delayReason":{
                 label: this.$t("wmsstockuniicode.delayReason"),
                 rules: [],
                    type: "input"
            },
             "Entity.delayTimes":{
                 label: this.$t("wmsstockuniicode.delayTimes"),
                 rules: [],
                    type: "input"
            },
             "Entity.delayToEndDate":{
                 label: this.$t("wmsstockuniicode.delayToEndDate"),
                 rules: [],
                    type: "datePicker"
            },
             "Entity.driedScrapFlag":{
                 label: this.$t("wmsstockuniicode.driedScrapFlag"),
                 rules: [],
                    type: "input"
            },
             "Entity.driedTimes":{
                 label: this.$t("wmsstockuniicode.driedTimes"),
                 rules: [],
                    type: "input"
            },
             "Entity.erpWhouseNo":{
                 label: this.$t("wmsstockuniicode.erpWhouseNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.expDate":{
                 label: this.$t("wmsstockuniicode.expDate"),
                 rules: [],
                    type: "datePicker"
            },
             "Entity.exposeFrozenFlag":{
                 label: this.$t("wmsstockuniicode.exposeFrozenFlag"),
                 rules: [],
                    type: "input"
            },
             "Entity.exposeFrozenReason":{
                 label: this.$t("wmsstockuniicode.exposeFrozenReason"),
                 rules: [],
                    type: "input"
            },
             "Entity.inspectionResult":{
                 label: this.$t("wmsstockuniicode.inspectionResult"),
                 rules: [],
                    type: "input"
            },
             "Entity.inwhTime":{
                 label: this.$t("wmsstockuniicode.inwhTime"),
                 rules: [],
                    type: "datePicker"
            },
             "Entity.leftMslTimes":{
                 label: this.$t("wmsstockuniicode.leftMslTimes"),
                 rules: [],
                    type: "input"
            },
             "Entity.materialName":{
                 label: this.$t("wmsstockuniicode.materialName"),
                 rules: [],
                    type: "input"
            },
             "Entity.materialCode":{
                 label: this.$t("wmsstockuniicode.materialCode"),
                 rules: [{ required: true, message: this.$t("wmsstockuniicode.materialCode")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.materialSpec":{
                 label: this.$t("wmsstockuniicode.materialSpec"),
                 rules: [],
                    type: "input"
            },
             "Entity.mslGradeCode":{
                 label: this.$t("wmsstockuniicode.mslGradeCode"),
                 rules: [],
                    type: "input"
            },
             "Entity.occupyQty":{
                 label: this.$t("wmsstockuniicode.occupyQty"),
                 rules: [{ required: true, message: this.$t("wmsstockuniicode.occupyQty")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.packageTime":{
                 label: this.$t("wmsstockuniicode.packageTime"),
                 rules: [],
                    type: "datePicker"
            },
             "Entity.palletBarcode":{
                 label: this.$t("wmsstockuniicode.palletBarcode"),
                 rules: [{ required: true, message: this.$t("wmsstockuniicode.palletBarcode")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.positionNo":{
                 label: this.$t("wmsstockuniicode.positionNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.productDate":{
                 label: this.$t("wmsstockuniicode.productDate"),
                 rules: [],
                    type: "datePicker"
            },
             "Entity.projectNo":{
                 label: this.$t("wmsstockuniicode.projectNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.proprietorCode":{
                 label: this.$t("wmsstockuniicode.proprietorCode"),
                 rules: [],
                    type: "input"
            },
             "Entity.realExposeTimes":{
                 label: this.$t("wmsstockuniicode.realExposeTimes"),
                 rules: [],
                    type: "input"
            },
             "Entity.skuCode":{
                 label: this.$t("wmsstockuniicode.skuCode"),
                 rules: [],
                    type: "input"
            },
             "Entity.stockCode":{
                 label: this.$t("wmsstockuniicode.stockCode"),
                 rules: [{ required: true, message: this.$t("wmsstockuniicode.stockCode")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.stockDtlId":{
                 label: this.$t("wmsstockuniicode.stockDtlId"),
                 rules: [{ required: true, message: this.$t("wmsstockuniicode.stockDtlId")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.qty":{
                 label: this.$t("wmsstockuniicode.qty"),
                 rules: [{ required: true, message: this.$t("wmsstockuniicode.qty")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.supplierCode":{
                 label: this.$t("wmsstockuniicode.supplierCode"),
                 rules: [],
                    type: "input"
            },
             "Entity.supplierExposeTimes":{
                 label: this.$t("wmsstockuniicode.supplierExposeTimes"),
                 rules: [],
                    type: "input"
            },
             "Entity.supplierName":{
                 label: this.$t("wmsstockuniicode.supplierName"),
                 rules: [],
                    type: "input"
            },
            //  "Entity.supplierNameAlias":{
            //      label: this.$t("wmsstockuniicode.supplierNameAlias"),
            //      rules: [],
            //         type: "input"
            // },
            //  "Entity.supplierNameEn":{
            //      label: this.$t("wmsstockuniicode.supplierNameEn"),
            //      rules: [],
            //         type: "input"
            // },
             "Entity.uniicode":{
                 label: this.$t("wmsstockuniicode.uniicode"),
                 rules: [{ required: true, message: this.$t("wmsstockuniicode.uniicode")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.unpackStatus":{
                 label: this.$t("wmsstockuniicode.unpackStatus"),
                 rules: [],
                    type: "input"
            },
             "Entity.unpackTime":{
                 label: this.$t("wmsstockuniicode.unpackTime"),
                 rules: [],
                    type: "datePicker"
            },
             "Entity.unitCode":{
                 label: this.$t("wmsstockuniicode.unitCode"),
                 rules: [],
                    type: "input"
            },
             "Entity.whouseNo":{
                 label: this.$t("wmsstockuniicode.whouseNo"),
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
