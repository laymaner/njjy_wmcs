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
             "Entity.allocatResult":{
                 label: this.$t("wmsoutinvoicerecord.allocatResult"),
                 rules: [],
                    type: "input"
            },
             "Entity.allotQty":{
                 label: this.$t("wmsoutinvoicerecord.allotQty"),
                 rules: [{ required: true, message: this.$t("wmsoutinvoicerecord.allotQty")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.allotType":{
                 label: this.$t("wmsoutinvoicerecord.allotType"),
                 rules: [],
                    type: "input"
            },
             "Entity.docTypeCode":{
                 label: this.$t("wmsoutinvoicerecord.docTypeCode"),
                 rules: [],
                    type: "input"
            },
             "Entity.areaNo":{
                 label: this.$t("wmsoutinvoicerecord.areaNo"),
                 rules: [{ required: true, message: this.$t("wmsoutinvoicerecord.areaNo")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.assemblyIdx":{
                 label: this.$t("wmsoutinvoicerecord.assemblyIdx"),
                 rules: [],
                    type: "input"
            },
             "Entity.batchNo":{
                 label: this.$t("wmsoutinvoicerecord.batchNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.belongDepartment":{
                 label: this.$t("wmsoutinvoicerecord.belongDepartment"),
                 rules: [],
                    type: "input"
            },
             "Entity.binNo":{
                 label: this.$t("wmsoutinvoicerecord.binNo"),
                 rules: [{ required: true, message: this.$t("wmsoutinvoicerecord.binNo")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.deliveryLocNo":{
                 label: this.$t("wmsoutinvoicerecord.deliveryLocNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.erpWhouseNo":{
                 label: this.$t("wmsoutinvoicerecord.erpWhouseNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.externalOutDtlId":{
                 label: this.$t("wmsoutinvoicerecord.externalOutDtlId"),
                 rules: [],
                    type: "input"
            },
             "Entity.externalOutNo":{
                 label: this.$t("wmsoutinvoicerecord.externalOutNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.fpName":{
                 label: this.$t("wmsoutinvoicerecord.fpName"),
                 rules: [],
                    type: "input"
            },
             "Entity.fpNo":{
                 label: this.$t("wmsoutinvoicerecord.fpNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.fpQty":{
                 label: this.$t("wmsoutinvoicerecord.fpQty"),
                 rules: [],
                    type: "input"
            },
             "Entity.inOutName":{
                 label: this.$t("wmsoutinvoicerecord.inOutName"),
                 rules: [],
                    type: "input"
            },
             "Entity.inOutTypeNo":{
                 label: this.$t("wmsoutinvoicerecord.inOutTypeNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.inspectionResult":{
                 label: this.$t("wmsoutinvoicerecord.inspectionResult"),
                 rules: [],
                    type: "input"
            },
             "Entity.invoiceDtlId":{
                 label: this.$t("wmsoutinvoicerecord.invoiceDtlId"),
                 rules: [],
                    type: "input"
            },
             "Entity.invoiceNo":{
                 label: this.$t("wmsoutinvoicerecord.invoiceNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.isBatch":{
                 label: this.$t("wmsoutinvoicerecord.isBatch"),
                 rules: [],
                    type: "input"
            },
             "Entity.issuedResult":{
                 label: this.$t("wmsoutinvoicerecord.issuedResult"),
                 rules: [],
                    type: "input"
            },
             "Entity.materialName":{
                 label: this.$t("wmsoutinvoicerecord.materialName"),
                 rules: [],
                    type: "input"
            },
             "Entity.materialCode":{
                 label: this.$t("wmsoutinvoicerecord.materialCode"),
                 rules: [{ required: true, message: this.$t("wmsoutinvoicerecord.materialCode")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.materialSpec":{
                 label: this.$t("wmsoutinvoicerecord.materialSpec"),
                 rules: [],
                    type: "input"
            },
             "Entity.orderDtlId":{
                 label: this.$t("wmsoutinvoicerecord.orderDtlId"),
                 rules: [],
                    type: "input"
            },
             "Entity.orderNo":{
                 label: this.$t("wmsoutinvoicerecord.orderNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.outRecordStatus":{
                 label: this.$t("wmsoutinvoicerecord.outRecordStatus"),
                 rules: [{ required: true, message: this.$t("wmsoutinvoicerecord.outRecordStatus")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.palletBarcode":{
                 label: this.$t("wmsoutinvoicerecord.palletBarcode"),
                 rules: [{ required: true, message: this.$t("wmsoutinvoicerecord.palletBarcode")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.palletPickType":{
                 label: this.$t("wmsoutinvoicerecord.palletPickType"),
                 rules: [{ required: true, message: this.$t("wmsoutinvoicerecord.palletPickType")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.pickLocNo":{
                 label: this.$t("wmsoutinvoicerecord.pickLocNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.pickQty":{
                 label: this.$t("wmsoutinvoicerecord.pickQty"),
                 rules: [{ required: true, message: this.$t("wmsoutinvoicerecord.pickQty")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.pickTaskNo":{
                 label: this.$t("wmsoutinvoicerecord.pickTaskNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.pickType":{
                 label: this.$t("wmsoutinvoicerecord.pickType"),
                 rules: [{ required: true, message: this.$t("wmsoutinvoicerecord.pickType")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.preStockDtlId":{
                 label: this.$t("wmsoutinvoicerecord.preStockDtlId"),
                 rules: [{ required: true, message: this.$t("wmsoutinvoicerecord.preStockDtlId")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.productDeptCode":{
                 label: this.$t("wmsoutinvoicerecord.productDeptCode"),
                 rules: [],
                    type: "input"
            },
             "Entity.productDeptName":{
                 label: this.$t("wmsoutinvoicerecord.productDeptName"),
                 rules: [],
                    type: "input"
            },
             "Entity.productLocation":{
                 label: this.$t("wmsoutinvoicerecord.productLocation"),
                 rules: [],
                    type: "input"
            },
             "Entity.projectNo":{
                 label: this.$t("wmsoutinvoicerecord.projectNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.proprietorCode":{
                 label: this.$t("wmsoutinvoicerecord.proprietorCode"),
                 rules: [],
                    type: "input"
            },
             "Entity.regionNo":{
                 label: this.$t("wmsoutinvoicerecord.regionNo"),
                 rules: [{ required: true, message: this.$t("wmsoutinvoicerecord.reversePickFlag")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.reversePickFlag":{
                 label: this.$t("wmsoutinvoicerecord.reversePickFlag"),
                 rules: [],
                    type: "input"
            },
             "Entity.skuCode":{
                 label: this.$t("wmsoutinvoicerecord.skuCode"),
                 rules: [],
                    type: "input"
            },
             "Entity.sourceBy":{
                 label: this.$t("wmsoutinvoicerecord.sourceBy"),
                 rules: [],
                    type: "input"
            },
             "Entity.stockCode":{
                 label: this.$t("wmsoutinvoicerecord.stockCode"),
                 rules: [{ required: true, message: this.$t("wmsoutinvoicerecord.stockCode")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.stockDtlId":{
                 label: this.$t("wmsoutinvoicerecord.stockDtlId"),
                 rules: [],
                    type: "input"
            },
             "Entity.supplierCode":{
                 label: this.$t("wmsoutinvoicerecord.supplierCode"),
                 rules: [],
                    type: "input"
            },
             "Entity.supplierName":{
                 label: this.$t("wmsoutinvoicerecord.supplierName"),
                 rules: [],
                    type: "input"
            },
            //  "Entity.supplierNameAlias":{
            //      label: this.$t("wmsoutinvoicerecord.supplierNameAlias"),
            //      rules: [],
            //         type: "input"
            // },
            //  "Entity.supplierNameEn":{
            //      label: this.$t("wmsoutinvoicerecord.supplierNameEn"),
            //      rules: [],
            //         type: "input"
            // },
             "Entity.supplyType":{
                 label: this.$t("wmsoutinvoicerecord.supplyType"),
                 rules: [],
                    type: "input"
            },
             "Entity.ticketNo":{
                 label: this.$t("wmsoutinvoicerecord.ticketNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.ticketPlanBeginTime":{
                 label: this.$t("wmsoutinvoicerecord.ticketPlanBeginTime"),
                 rules: [],
                    type: "datePicker"
            },
             "Entity.ticketType":{
                 label: this.$t("wmsoutinvoicerecord.ticketType"),
                 rules: [],
                    type: "input"
            },
             "Entity.unitCode":{
                 label: this.$t("wmsoutinvoicerecord.unitCode"),
                 rules: [],
                    type: "input"
            },
             "Entity.waveNo":{
                 label: this.$t("wmsoutinvoicerecord.waveNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.whouseNo":{
                 label: this.$t("wmsoutinvoicerecord.whouseNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.operationMode":{
                 label: this.$t("wmsoutinvoicerecord.operationMode"),
                 rules: [],
                    type: "input"
            },
             "Entity.outBarCode":{
                 label: this.$t("wmsoutinvoicerecord.outBarCode"),
                 rules: [],
                    type: "input"
            },
             "Entity.urgentFlag":{
                 label: this.$t("wmsoutinvoicerecord.urgentFlag"),
                 rules: [],
                    type: "input"
            },
             "Entity.loadedTtype":{
                 label: this.$t("wmsoutinvoicerecord.loadedTtype"),
                 rules: [],
                    type: "input"
            },
             "Entity.productSn":{
                 label: this.$t("wmsoutinvoicerecord.productSn"),
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
