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
                 label: this.$t("wmsinreceiptrecord.areaNo"),
                 rules: [{ required: true, message: this.$t("wmsinreceiptrecord.areaNo")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.batchNo":{
                 label: this.$t("wmsinreceiptrecord.batchNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.binNo":{
                 label: this.$t("wmsinreceiptrecord.binNo"),
                 rules: [{ required: true, message: this.$t("wmsinreceiptrecord.binNo")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.departmentName":{
                 label: this.$t("wmsinreceiptrecord.departmentName"),
                 rules: [],
                    type: "input"
            },
             "Entity.docTypeCode":{
                 label: this.$t("wmsinreceiptrecord.docTypeCode"),
                 rules: [{ required: true, message: this.$t("wmsinreceiptrecord.docTypeCode")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.erpWhouseNo":{
                 label: this.$t("wmsinreceiptrecord.erpWhouseNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.externalInDtlId":{
                 label: this.$t("wmsinreceiptrecord.externalInDtlId"),
                 rules: [],
                    type: "input"
            },
             "Entity.externalInNo":{
                 label: this.$t("wmsinreceiptrecord.externalInNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.inDtlId":{
                 label: this.$t("wmsinreceiptrecord.inDtlId"),
                 rules: [],
                    type: "input"
            },
             "Entity.inNo":{
                 label: this.$t("wmsinreceiptrecord.inNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.inOutName":{
                 label: this.$t("wmsinreceiptrecord.inOutName"),
                 rules: [],
                    type: "input"
            },
             "Entity.inOutTypeNo":{
                 label: this.$t("wmsinreceiptrecord.inOutTypeNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.inspectionResult":{
                 label: this.$t("wmsinreceiptrecord.inspectionResult"),
                 rules: [{ required: true, message: this.$t("wmsinreceiptrecord.inspectionResult")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.inspector":{
                 label: this.$t("wmsinreceiptrecord.inspector"),
                 rules: [],
                    type: "input"
            },
             "Entity.iqcResultNo":{
                 label: this.$t("wmsinreceiptrecord.iqcResultNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.inRecordStatus":{
                 label: this.$t("wmsinreceiptrecord.inRecordStatus"),
                 rules: [{ required: true, message: this.$t("wmsinreceiptrecord.inRecordStatus")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.loadedType":{
                 label: this.$t("wmsinreceiptrecord.loadedType"),
                 rules: [],
                    type: "input"
            },
             "Entity.materialName":{
                 label: this.$t("wmsinreceiptrecord.materialName"),
                 rules: [],
                    type: "input"
            },
             "Entity.materialCode":{
                 label: this.$t("wmsinreceiptrecord.materialCode"),
                 rules: [{ required: true, message: this.$t("wmsinreceiptrecord.materialCode")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.materialSpec":{
                 label: this.$t("wmsinreceiptrecord.materialSpec"),
                 rules: [],
                    type: "input"
            },
             "Entity.orderDtlId":{
                 label: this.$t("wmsinreceiptrecord.orderDtlId"),
                 rules: [],
                    type: "input"
            },
             "Entity.orderNo":{
                 label: this.$t("wmsinreceiptrecord.orderNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.palletBarcode":{
                 label: this.$t("wmsinreceiptrecord.palletBarcode"),
                 rules: [],
                    type: "input"
            },
             "Entity.ptaRegionNo":{
                 label: this.$t("wmsinreceiptrecord.ptaRegionNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.ptaStockCode":{
                 label: this.$t("wmsinreceiptrecord.ptaStockCode"),
                 rules: [],
                    type: "input"
            },
             "Entity.ptaStockDtlId":{
                 label: this.$t("wmsinreceiptrecord.ptaStockDtlId"),
                 rules: [],
                    type: "input"
            },
             "Entity.minPkgQty":{
                 label: this.$t("wmsinreceiptrecord.minPkgQty"),
                 rules: [],
                    type: "input"
            },
             "Entity.projectNo":{
                 label: this.$t("wmsinreceiptrecord.projectNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.proprietorCode":{
                 label: this.$t("wmsinreceiptrecord.proprietorCode"),
                 rules: [],
                    type: "input"
            },
             "Entity.ptaBinNo":{
                 label: this.$t("wmsinreceiptrecord.ptaBinNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.ptaPalletBarcode":{
                 label: this.$t("wmsinreceiptrecord.ptaPalletBarcode"),
                 rules: [],
                    type: "input"
            },
             "Entity.receiptDtlId":{
                 label: this.$t("wmsinreceiptrecord.receiptDtlId"),
                 rules: [],
                    type: "input"
            },
             "Entity.receiptNo":{
                 label: this.$t("wmsinreceiptrecord.receiptNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.recordQty":{
                 label: this.$t("wmsinreceiptrecord.recordQty"),
                 rules: [],
                    type: "input"
            },
             "Entity.regionNo":{
                 label: this.$t("wmsinreceiptrecord.regionNo"),
                 rules: [{ required: true, message: "库区编号"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.replenishFlag":{
                 label: this.$t("wmsinreceiptrecord.replenishFlag"),
                 rules: [],
                    type: "input"
            },
             "Entity.returnFlag":{
                 label: this.$t("wmsinreceiptrecord.returnFlag"),
                 rules: [{ required: true, message: this.$t("wmsinreceiptrecord.returnFlag")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.returnResult":{
                 label: this.$t("wmsinreceiptrecord.returnResult"),
                 rules: [],
                    type: "input"
            },
             "Entity.returnTime":{
                 label: this.$t("wmsinreceiptrecord.returnTime"),
                 rules: [],
                    type: "datePicker"
            },
             "Entity.skuCode":{
                 label: this.$t("wmsinreceiptrecord.skuCode"),
                 rules: [],
                    type: "input"
            },
             "Entity.sourceBy":{
                 label: this.$t("wmsinreceiptrecord.sourceBy"),
                 rules: [{ required: true, message: this.$t("wmsinreceiptrecord.sourceBy")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.stockCode":{
                 label: this.$t("wmsinreceiptrecord.stockCode"),
                 rules: [{ required: true, message: this.$t("wmsinreceiptrecord.stockCode")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.stockDtlId":{
                 label: this.$t("wmsinreceiptrecord.stockDtlId"),
                 rules: [],
                    type: "input"
            },
             "Entity.supplierCode":{
                 label: this.$t("wmsinreceiptrecord.supplierCode"),
                 rules: [],
                    type: "input"
            },
             "Entity.supplierName":{
                 label: this.$t("wmsinreceiptrecord.supplierName"),
                 rules: [],
                    type: "input"
            },
            //  "Entity.supplierNameAlias":{
            //      label: this.$t("wmsinreceiptrecord.supplierNameAlias"),
            //      rules: [],
            //         type: "input"
            // },
            //  "Entity.supplierNameEn":{
            //      label: this.$t("wmsinreceiptrecord.supplierNameEn"),
            //      rules: [],
            //         type: "input"
            // },
             "Entity.ticketNo":{
                 label: this.$t("wmsinreceiptrecord.ticketNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.urgentFlag":{
                 label: this.$t("wmsinreceiptrecord.urgentFlag"),
                 rules: [],
                    type: "input"
            },
             "Entity.unitCode":{
                 label: this.$t("wmsinreceiptrecord.unitCode"),
                 rules: [],
                    type: "input"
            },
             "Entity.whouseNo":{
                 label: this.$t("wmsinreceiptrecord.whouseNo"),
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
