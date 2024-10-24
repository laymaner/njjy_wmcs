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
                 label: this.$t("wmsinreceiptrecordhis.areaNo"),
                 rules: [{ required: true, message: this.$t("wmsinreceiptrecordhis.areaNo")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.batchNo":{
                 label: this.$t("wmsinreceiptrecordhis.batchNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.binNo":{
                 label: this.$t("wmsinreceiptrecordhis.binNo"),
                 rules: [{ required: true, message: this.$t("wmsinreceiptrecordhis.binNo")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.departmentName":{
                 label: this.$t("wmsinreceiptrecordhis.departmentName"),
                 rules: [],
                    type: "input"
            },
             "Entity.docTypeCode":{
                 label: this.$t("wmsinreceiptrecordhis.docTypeCode"),
                 rules: [{ required: true, message: this.$t("wmsinreceiptrecordhis.docTypeCode")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.erpWhouseNo":{
                 label: this.$t("wmsinreceiptrecordhis.erpWhouseNo"),
                 rules: [{ required: true, message: this.$t("wmsinreceiptrecordhis.erpWhouseNo")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.externalInDtlId":{
                 label: this.$t("wmsinreceiptrecordhis.externalInDtlId"),
                 rules: [],
                    type: "input"
            },
             "Entity.externalInNo":{
                 label: this.$t("wmsinreceiptrecordhis.externalInNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.inDtlId":{
                 label: this.$t("wmsinreceiptrecordhis.inDtlId"),
                 rules: [{ required: true, message: this.$t("wmsinreceiptrecordhis.inDtlId")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.inNo":{
                 label: this.$t("wmsinreceiptrecordhis.inNo"),
                 rules: [{ required: true, message: this.$t("wmsinreceiptrecordhis.inNo")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.inOutName":{
                 label: this.$t("wmsinreceiptrecordhis.inOutName"),
                 rules: [],
                    type: "input"
            },
             "Entity.inOutTypeNo":{
                 label: this.$t("wmsinreceiptrecordhis.inOutTypeNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.inspectionResult":{
                 label: this.$t("wmsinreceiptrecordhis.inspectionResult"),
                 rules: [{ required: true, message: this.$t("wmsinreceiptrecordhis.inspectionResult")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.inspector":{
                 label: this.$t("wmsinreceiptrecordhis.inspector"),
                 rules: [],
                    type: "input"
            },
             "Entity.iqcResultNo":{
                 label: this.$t("wmsinreceiptrecordhis.iqcResultNo"),
                 rules: [{ required: true, message: this.$t("wmsinreceiptrecordhis.iqcResultNo")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.inRecordStatus":{
                 label: this.$t("wmsinreceiptrecordhis.inRecordStatus"),
                 rules: [{ required: true, message: this.$t("wmsinreceiptrecordhis.inRecordStatus")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.loadedType":{
                 label: this.$t("wmsinreceiptrecordhis.loadedType"),
                 rules: [],
                    type: "input"
            },
             "Entity.materialName":{
                 label: this.$t("wmsinreceiptrecordhis.materialName"),
                 rules: [],
                    type: "input"
            },
             "Entity.materialCode":{
                 label: this.$t("wmsinreceiptrecordhis.materialCode"),
                 rules: [{ required: true, message: this.$t("wmsinreceiptrecordhis.materialCode")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.materialSpec":{
                 label: this.$t("wmsinreceiptrecordhis.materialSpec"),
                 rules: [],
                    type: "input"
            },
             "Entity.orderDtlId":{
                 label: this.$t("wmsinreceiptrecordhis.orderDtlId"),
                 rules: [],
                    type: "input"
            },
             "Entity.orderNo":{
                 label: this.$t("wmsinreceiptrecordhis.orderNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.palletBarcode":{
                 label: this.$t("wmsinreceiptrecordhis.palletBarcode"),
                 rules: [],
                    type: "input"
            },
             "Entity.ptaRegionNo":{
                 label: this.$t("wmsinreceiptrecordhis.ptaRegionNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.ptaStockCode":{
                 label: this.$t("wmsinreceiptrecordhis.ptaStockCode"),
                 rules: [],
                    type: "input"
            },
             "Entity.ptaStockDtlId":{
                 label: this.$t("wmsinreceiptrecordhis.ptaStockDtlId"),
                 rules: [],
                    type: "input"
            },
             "Entity.minPkgQty":{
                 label: this.$t("wmsinreceiptrecordhis.minPkgQty"),
                 rules: [],
                    type: "input"
            },
             "Entity.projectNo":{
                 label: this.$t("wmsinreceiptrecordhis.projectNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.proprietorCode":{
                 label: this.$t("wmsinreceiptrecordhis.proprietorCode"),
                 rules: [],
                    type: "input"
            },
             "Entity.ptaBinNo":{
                 label: this.$t("wmsinreceiptrecordhis.ptaBinNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.ptaPalletBarcode":{
                 label: this.$t("wmsinreceiptrecordhis.ptaPalletBarcode"),
                 rules: [],
                    type: "input"
            },
             "Entity.receiptDtlId":{
                 label: this.$t("wmsinreceiptrecordhis.receiptDtlId"),
                 rules: [{ required: true, message: this.$t("wmsinreceiptrecordhis.receiptDtlId")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.receiptNo":{
                 label: this.$t("wmsinreceiptrecordhis.receiptNo"),
                 rules: [{ required: true, message: this.$t("wmsinreceiptrecordhis.receiptNo")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.recordQty":{
                 label: this.$t("wmsinreceiptrecordhis.recordQty"),
                 rules: [],
                    type: "input"
            },
             "Entity.regionNo":{
                 label: this.$t("wmsinreceiptrecordhis.regionNo"),
                 rules: [{ required: true, message: this.$t("wmsinreceiptrecordhis.regionNo")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.replenishFlag":{
                 label: this.$t("wmsinreceiptrecordhis.replenishFlag"),
                 rules: [],
                    type: "input"
            },
             "Entity.returnFlag":{
                 label: this.$t("wmsinreceiptrecordhis.returnFlag"),
                 rules: [{ required: true, message: this.$t("wmsinreceiptrecordhis.returnFlag")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.returnResult":{
                 label: this.$t("wmsinreceiptrecordhis.returnResult"),
                 rules: [],
                    type: "input"
            },
             "Entity.returnTime":{
                 label: this.$t("wmsinreceiptrecordhis.returnTime"),
                 rules: [],
                    type: "datePicker"
            },
             "Entity.skuCode":{
                 label: this.$t("wmsinreceiptrecordhis.skuCode"),
                 rules: [],
                    type: "input"
            },
             "Entity.sourceBy":{
                 label: this.$t("wmsinreceiptrecordhis.sourceBy"),
                 rules: [{ required: true, message: this.$t("wmsinreceiptrecordhis.sourceBy")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.stockCode":{
                 label: this.$t("wmsinreceiptrecordhis.stockCode"),
                 rules: [{ required: true, message: this.$t("wmsinreceiptrecordhis.stockCode")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.stockDtlId":{
                 label: this.$t("wmsinreceiptrecordhis.stockDtlId"),
                 rules: [],
                    type: "input"
            },
             "Entity.supplierCode":{
                 label: this.$t("wmsinreceiptrecordhis.supplierCode"),
                 rules: [],
                    type: "input"
            },
             "Entity.supplierName":{
                 label: this.$t("wmsinreceiptrecordhis.supplierName"),
                 rules: [],
                    type: "input"
            },
            //  "Entity.supplierNameAlias":{
            //      label: this.$t("wmsinreceiptrecordhis.supplierNameAlias"),
            //      rules: [],
            //         type: "input"
            // },
            //  "Entity.supplierNameEn":{
            //      label: this.$t("wmsinreceiptrecordhis.supplierNameEn"),
            //      rules: [],
            //         type: "input"
            // },
             "Entity.ticketNo":{
                 label: this.$t("wmsinreceiptrecordhis.ticketNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.urgentFlag":{
                 label: this.$t("wmsinreceiptrecordhis.urgentFlag"),
                 rules: [{ required: true, message: this.$t("wmsinreceiptrecordhis.urgentFlag")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.unitCode":{
                 label: this.$t("wmsinreceiptrecordhis.unitCode"),
                 rules: [],
                    type: "input"
            },
             "Entity.whouseNo":{
                 label: this.$t("wmsinreceiptrecordhis.whouseNo"),
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
