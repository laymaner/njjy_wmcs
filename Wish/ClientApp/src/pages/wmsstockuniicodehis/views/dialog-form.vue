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
                 label: this.$t("wmsstockuniicodehis.areaNo"),
                 rules: [{ required: true, message: this.$t("wmsstockuniicodehis.areaNo")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.batchNo":{
                 label: this.$t("wmsstockuniicodehis.batchNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.dataCode":{
                 label: this.$t("wmsstockuniicodehis.dataCode"),
                 rules: [],
                    type: "input"
            },
             "Entity.delayFrozenFlag":{
                 label: this.$t("wmsstockuniicodehis.delayFrozenFlag"),
                 rules: [],
                    type: "input"
            },
             "Entity.delayFrozenReason":{
                 label: this.$t("wmsstockuniicodehis.delayFrozenReason"),
                 rules: [],
                    type: "input"
            },
             "Entity.delayReason":{
                 label: this.$t("wmsstockuniicodehis.delayReason"),
                 rules: [],
                    type: "input"
            },
             "Entity.delayTimes":{
                 label: this.$t("wmsstockuniicodehis.delayTimes"),
                 rules: [],
                    type: "input"
            },
             "Entity.delayToEndDate":{
                 label: this.$t("wmsstockuniicodehis.delayToEndDate"),
                 rules: [],
                    type: "datePicker"
            },
             "Entity.driedScrapFlag":{
                 label: this.$t("wmsstockuniicodehis.driedScrapFlag"),
                 rules: [],
                    type: "input"
            },
             "Entity.driedTimes":{
                 label: this.$t("wmsstockuniicodehis.driedTimes"),
                 rules: [],
                    type: "input"
            },
             "Entity.erpWhouseNo":{
                 label: this.$t("wmsstockuniicodehis.erpWhouseNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.expDate":{
                 label: this.$t("wmsstockuniicodehis.expDate"),
                 rules: [],
                    type: "datePicker"
            },
             "Entity.exposeFrozenFlag":{
                 label: this.$t("wmsstockuniicodehis.exposeFrozenFlag"),
                 rules: [],
                    type: "input"
            },
             "Entity.exposeFrozenReason":{
                 label: this.$t("wmsstockuniicodehis.exposeFrozenReason"),
                 rules: [],
                    type: "input"
            },
             "Entity.inspectionResult":{
                 label: this.$t("wmsstockuniicodehis.inspectionResult"),
                 rules: [],
                    type: "input"
            },
             "Entity.inwhTime":{
                 label: this.$t("wmsstockuniicodehis.inwhTime"),
                 rules: [],
                    type: "datePicker"
            },
             "Entity.leftMslTimes":{
                 label: this.$t("wmsstockuniicodehis.leftMslTimes"),
                 rules: [],
                    type: "input"
            },
             "Entity.materialName":{
                 label: this.$t("wmsstockuniicodehis.materialName"),
                 rules: [],
                    type: "input"
            },
             "Entity.materialCode":{
                 label: this.$t("wmsstockuniicodehis.materialCode"),
                 rules: [{ required: true, message: this.$t("wmsstockuniicodehis.materialCode")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.materialSpec":{
                 label: this.$t("wmsstockuniicodehis.materialSpec"),
                 rules: [],
                    type: "input"
            },
             "Entity.mslGradeCode":{
                 label: this.$t("wmsstockuniicodehis.mslGradeCode"),
                 rules: [],
                    type: "input"
            },
             "Entity.occupyQty":{
                 label: this.$t("wmsstockuniicodehis.occupyQty"),
                 rules: [{ required: true, message: this.$t("wmsstockuniicodehis.occupyQty")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.packageTime":{
                 label: this.$t("wmsstockuniicodehis.packageTime"),
                 rules: [],
                    type: "datePicker"
            },
             "Entity.palletBarcode":{
                 label: this.$t("wmsstockuniicodehis.palletBarcode"),
                 rules: [{ required: true, message: this.$t("wmsstockuniicodehis.palletBarcode")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.positionNo":{
                 label: this.$t("wmsstockuniicodehis.positionNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.productDate":{
                 label: this.$t("wmsstockuniicodehis.productDate"),
                 rules: [],
                    type: "datePicker"
            },
             "Entity.projectNo":{
                 label: this.$t("wmsstockuniicodehis.projectNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.proprietorCode":{
                 label: this.$t("wmsstockuniicodehis.proprietorCode"),
                 rules: [],
                    type: "input"
            },
             "Entity.realExposeTimes":{
                 label: this.$t("wmsstockuniicodehis.realExposeTimes"),
                 rules: [],
                    type: "input"
            },
             "Entity.skuCode":{
                 label: this.$t("wmsstockuniicodehis.skuCode"),
                 rules: [],
                    type: "input"
            },
             "Entity.stockCode":{
                 label: this.$t("wmsstockuniicodehis.stockCode"),
                 rules: [{ required: true, message: this.$t("wmsstockuniicodehis.stockCode")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.stockDtlId":{
                 label: this.$t("wmsstockuniicodehis.stockDtlId"),
                 rules: [{ required: true, message: this.$t("wmsstockuniicodehis.stockDtlId")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.qty":{
                 label: this.$t("wmsstockuniicodehis.qty"),
                 rules: [{ required: true, message: this.$t("wmsstockuniicodehis.qty")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.supplierCode":{
                 label: this.$t("wmsstockuniicodehis.supplierCode"),
                 rules: [],
                    type: "input"
            },
             "Entity.supplierExposeTimes":{
                 label: this.$t("wmsstockuniicodehis.supplierExposeTimes"),
                 rules: [],
                    type: "input"
            },
             "Entity.supplierName":{
                 label: this.$t("wmsstockuniicodehis.supplierName"),
                 rules: [],
                    type: "input"
            },
            //  "Entity.supplierNameAlias":{
            //      label: this.$t("wmsstockuniicodehis.supplierNameAlias"),
            //      rules: [],
            //         type: "input"
            // },
            //  "Entity.supplierNameEn":{
            //      label: this.$t("wmsstockuniicodehis.supplierNameEn"),
            //      rules: [],
            //         type: "input"
            // },
             "Entity.uniicode":{
                 label: this.$t("wmsstockuniicodehis.uniicode"),
                 rules: [{ required: true, message: this.$t("wmsstockuniicodehis.uniicode")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.unpackStatus":{
                 label: this.$t("wmsstockuniicodehis.unpackStatus"),
                 rules: [],
                    type: "input"
            },
             "Entity.unpackTime":{
                 label: this.$t("wmsstockuniicodehis.unpackTime"),
                 rules: [],
                    type: "datePicker"
            },
             "Entity.unitCode":{
                 label: this.$t("wmsstockuniicodehis.unitCode"),
                 rules: [],
                    type: "input"
            },
             "Entity.whouseNo":{
                 label: this.$t("wmsstockuniicodehis.whouseNo"),
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
