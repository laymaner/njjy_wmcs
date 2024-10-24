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
                 label: this.$t("wmsoutinvoicerecordhis.allocatResult"),
                 rules: [],
                    type: "input"
            },
             "Entity.allotQty":{
                 label: this.$t("wmsoutinvoicerecordhis.allotQty"),
                 rules: [{ required: true, message: this.$t("wmsoutinvoicerecordhis.allotQty")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.allotType":{
                 label: this.$t("wmsoutinvoicerecordhis.allotType"),
                 rules: [],
                    type: "input"
            },
             "Entity.docTypeCode":{
                 label: this.$t("wmsoutinvoicerecordhis.docTypeCode"),
                 rules: [],
                    type: "input"
            },
             "Entity.areaNo":{
                 label: this.$t("wmsoutinvoicerecordhis.areaNo"),
                 rules: [{ required: true, message: this.$t("wmsoutinvoicerecordhis.areaNo")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.assemblyIdx":{
                 label: this.$t("wmsoutinvoicerecordhis.assemblyIdx"),
                 rules: [],
                    type: "input"
            },
             "Entity.batchNo":{
                 label: this.$t("wmsoutinvoicerecordhis.batchNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.belongDepartment":{
                 label: this.$t("wmsoutinvoicerecordhis.belongDepartment"),
                 rules: [],
                    type: "input"
            },
             "Entity.binNo":{
                 label: this.$t("wmsoutinvoicerecordhis.binNo"),
                 rules: [{ required: true, message: this.$t("wmsoutinvoicerecordhis.binNo")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.deliveryLocNo":{
                 label: this.$t("wmsoutinvoicerecordhis.deliveryLocNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.erpWhouseNo":{
                 label: this.$t("wmsoutinvoicerecordhis.erpWhouseNo"),
                 rules: [{ required: true, message: this.$t("wmsoutinvoicerecordhis.erpWhouseNo")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.externalOutDtlId":{
                 label: this.$t("wmsoutinvoicerecordhis.externalOutDtlId"),
                 rules: [],
                    type: "input"
            },
             "Entity.externalOutNo":{
                 label: this.$t("wmsoutinvoicerecordhis.externalOutNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.fpName":{
                 label: this.$t("wmsoutinvoicerecordhis.fpName"),
                 rules: [],
                    type: "input"
            },
             "Entity.fpNo":{
                 label: this.$t("wmsoutinvoicerecordhis.fpNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.fpQty":{
                 label: this.$t("wmsoutinvoicerecordhis.fpQty"),
                 rules: [],
                    type: "input"
            },
             "Entity.inOutName":{
                 label: this.$t("wmsoutinvoicerecordhis.inOutName"),
                 rules: [],
                    type: "input"
            },
             "Entity.inOutTypeNo":{
                 label: this.$t("wmsoutinvoicerecordhis.inOutTypeNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.inspectionResult":{
                 label: this.$t("wmsoutinvoicerecordhis.inspectionResult"),
                 rules: [],
                    type: "input"
            },
             "Entity.invoiceDtlId":{
                 label: this.$t("wmsoutinvoicerecordhis.invoiceDtlId"),
                 rules: [{ required: true, message: this.$t("wmsoutinvoicerecordhis.invoiceDtlId")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.invoiceNo":{
                 label: this.$t("wmsoutinvoicerecordhis.invoiceNo"),
                 rules: [{ required: true, message: this.$t("wmsoutinvoicerecordhis.invoiceNo")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.isBatch":{
                 label: this.$t("wmsoutinvoicerecordhis.isBatch"),
                 rules: [],
                    type: "input"
            },
             "Entity.issuedResult":{
                 label: this.$t("wmsoutinvoicerecordhis.issuedResult"),
                 rules: [],
                    type: "input"
            },
             "Entity.materialName":{
                 label: this.$t("wmsoutinvoicerecordhis.materialName"),
                 rules: [],
                    type: "input"
            },
             "Entity.materialCode":{
                 label: this.$t("wmsoutinvoicerecordhis.materialCode"),
                 rules: [{ required: true, message: this.$t("wmsoutinvoicerecordhis.materialCode")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.materialSpec":{
                 label: this.$t("wmsoutinvoicerecordhis.materialSpec"),
                 rules: [],
                    type: "input"
            },
             "Entity.orderDtlId":{
                 label: this.$t("wmsoutinvoicerecordhis.orderDtlId"),
                 rules: [],
                    type: "input"
            },
             "Entity.orderNo":{
                 label: this.$t("wmsoutinvoicerecordhis.orderNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.outRecordStatus":{
                 label: this.$t("wmsoutinvoicerecordhis.outRecordStatus"),
                 rules: [{ required: true, message: this.$t("wmsoutinvoicerecordhis.outRecordStatus")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.palletBarcode":{
                 label: this.$t("wmsoutinvoicerecordhis.palletBarcode"),
                 rules: [{ required: true, message: this.$t("wmsoutinvoicerecordhis.palletBarcode")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.palletPickType":{
                 label: this.$t("wmsoutinvoicerecordhis.palletPickType"),
                 rules: [{ required: true, message: this.$t("wmsoutinvoicerecordhis.palletPickType")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.pickLocNo":{
                 label: this.$t("wmsoutinvoicerecordhis.pickLocNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.pickQty":{
                 label: this.$t("wmsoutinvoicerecordhis.pickQty"),
                 rules: [{ required: true, message: this.$t("wmsoutinvoicerecordhis.pickQty")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.pickTaskNo":{
                 label: this.$t("wmsoutinvoicerecordhis.pickTaskNo"),
                 rules: [{ required: true, message: this.$t("wmsoutinvoicerecordhis.pickTaskNo")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.pickType":{
                 label: this.$t("wmsoutinvoicerecordhis.pickType"),
                 rules: [{ required: true, message: this.$t("wmsoutinvoicerecordhis.pickType")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.preStockDtlId":{
                 label: this.$t("wmsoutinvoicerecordhis.preStockDtlId"),
                 rules: [{ required: true, message: this.$t("wmsoutinvoicerecordhis.preStockDtlId")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.productDeptCode":{
                 label: this.$t("wmsoutinvoicerecordhis.productDeptCode"),
                 rules: [],
                    type: "input"
            },
             "Entity.productDeptName":{
                 label: this.$t("wmsoutinvoicerecordhis.productDeptName"),
                 rules: [],
                    type: "input"
            },
             "Entity.productLocation":{
                 label: this.$t("wmsoutinvoicerecordhis.productLocation"),
                 rules: [],
                    type: "input"
            },
             "Entity.projectNo":{
                 label: this.$t("wmsoutinvoicerecordhis.projectNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.proprietorCode":{
                 label: this.$t("wmsoutinvoicerecordhis.proprietorCode"),
                 rules: [],
                    type: "input"
            },
             "Entity.regionNo":{
                 label: this.$t("wmsoutinvoicerecordhis.regionNo"),
                 rules: [{ required: true, message: this.$t("wmsoutinvoicerecordhis.regionNo")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.reversePickFlag":{
                 label: this.$t("wmsoutinvoicerecordhis.reversePickFlag"),
                 rules: [],
                    type: "input"
            },
             "Entity.skuCode":{
                 label: this.$t("wmsoutinvoicerecordhis.skuCode"),
                 rules: [],
                    type: "input"
            },
             "Entity.sourceBy":{
                 label: this.$t("wmsoutinvoicerecordhis.sourceBy"),
                 rules: [],
                    type: "input"
            },
             "Entity.stockCode":{
                 label: this.$t("wmsoutinvoicerecordhis.stockCode"),
                 rules: [{ required: true, message: this.$t("wmsoutinvoicerecordhis.stockCode")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.stockDtlId":{
                 label: this.$t("wmsoutinvoicerecordhis.stockDtlId"),
                 rules: [],
                    type: "input"
            },
             "Entity.supplierCode":{
                 label: this.$t("wmsoutinvoicerecordhis.supplierCode"),
                 rules: [],
                    type: "input"
            },
             "Entity.supplierName":{
                 label: this.$t("wmsoutinvoicerecordhis.supplierName"),
                 rules: [],
                    type: "input"
            },
            //  "Entity.supplierNameAlias":{
            //      label: this.$t("wmsoutinvoicerecordhis.supplierNameAlias"),
            //      rules: [],
            //         type: "input"
            // },
            //  "Entity.supplierNameEn":{
            //      label: this.$t("wmsoutinvoicerecordhis.supplierNameEn"),
            //      rules: [],
            //         type: "input"
            // },
             "Entity.supplyType":{
                 label: this.$t("wmsoutinvoicerecordhis.supplyType"),
                 rules: [],
                    type: "input"
            },
             "Entity.ticketNo":{
                 label: this.$t("wmsoutinvoicerecordhis.ticketNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.ticketPlanBeginTime":{
                 label: this.$t("wmsoutinvoicerecordhis.ticketPlanBeginTime"),
                 rules: [],
                    type: "datePicker"
            },
             "Entity.ticketType":{
                 label: this.$t("wmsoutinvoicerecordhis.ticketType"),
                 rules: [],
                    type: "input"
            },
             "Entity.unitCode":{
                 label: this.$t("wmsoutinvoicerecordhis.unitCode"),
                 rules: [],
                    type: "input"
            },
             "Entity.waveNo":{
                 label: this.$t("wmsoutinvoicerecordhis.waveNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.whouseNo":{
                 label: this.$t("wmsoutinvoicerecordhis.whouseNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.operationMode":{
                 label: this.$t("wmsoutinvoicerecordhis.operationMode"),
                 rules: [],
                    type: "input"
            },
             "Entity.outBarCode":{
                 label: this.$t("wmsoutinvoicerecordhis.outBarCode"),
                 rules: [],
                    type: "input"
            },
             "Entity.urgentFlag":{
                 label: this.$t("wmsoutinvoicerecordhis.urgentFlag"),
                 rules: [],
                    type: "input"
            },
             "Entity.loadedTtype":{
                 label: this.$t("wmsoutinvoicerecordhis.loadedTtype"),
                 rules: [],
                    type: "input"
            },
             "Entity.productSn":{
                 label: this.$t("wmsoutinvoicerecordhis.productSn"),
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
