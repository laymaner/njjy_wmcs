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
                 label: this.$t("wmsoutinvoicedtl.allocatResult"),
                 rules: [],
                    type: "input"
            },
             "Entity.allotQty":{
                 label: this.$t("wmsoutinvoicedtl.allotQty"),
                 rules: [{ required: true, message: this.$t("wmsoutinvoicedtl.allotQty")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.areaNo":{
                 label: this.$t("wmsoutinvoicedtl.areaNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.assemblyIdx":{
                 label: this.$t("wmsoutinvoicedtl.assemblyIdx"),
                 rules: [],
                    type: "input"
            },
             "Entity.batchNo":{
                 label: this.$t("wmsoutinvoicedtl.batchNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.belongDepartment":{
                 label: this.$t("wmsoutinvoicedtl.belongDepartment"),
                 rules: [],
                    type: "input"
            },
             "Entity.completeQty":{
                 label: this.$t("wmsoutinvoicedtl.completeQty"),
                 rules: [{ required: true, message: this.$t("wmsoutinvoicedtl.completeQty")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.erpUndeliverQty":{
                 label: this.$t("wmsoutinvoicedtl.erpUndeliverQty"),
                 rules: [{ required: true, message: this.$t("wmsoutinvoicedtl.erpUndeliverQty")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.erpWhouseNo":{
                 label: this.$t("wmsoutinvoicedtl.erpWhouseNo"),
                 rules: [{ required: true, message: this.$t("wmsoutinvoicedtl.erpWhouseNo")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
            //  "Entity.extend1":{
            //      label: "extend1",
            //      rules: [],
            //         type: "input"
            // },
            //  "Entity.extend10":{
            //      label: "extend10",
            //      rules: [],
            //         type: "input"
            // },
            //  "Entity.extend11":{
            //      label: "extend11",
            //      rules: [],
            //         type: "input"
            // },
            //  "Entity.extend12":{
            //      label: "extend12",
            //      rules: [],
            //         type: "input"
            // },
            //  "Entity.extend13":{
            //      label: "extend13",
            //      rules: [],
            //         type: "input"
            // },
            //  "Entity.extend14":{
            //      label: "extend14",
            //      rules: [],
            //         type: "input"
            // },
            //  "Entity.extend15":{
            //      label: "extend15",
            //      rules: [],
            //         type: "input"
            // },
            //  "Entity.extend2":{
            //      label: "extend2",
            //      rules: [],
            //         type: "input"
            // },
            //  "Entity.extend3":{
            //      label: "extend3",
            //      rules: [],
            //         type: "input"
            // },
            //  "Entity.extend4":{
            //      label: "extend4",
            //      rules: [],
            //         type: "input"
            // },
            //  "Entity.extend5":{
            //      label: "extend5",
            //      rules: [],
            //         type: "input"
            // },
            //  "Entity.extend6":{
            //      label: "extend6",
            //      rules: [],
            //         type: "input"
            // },
            //  "Entity.extend7":{
            //      label: "extend7",
            //      rules: [],
            //         type: "input"
            // },
            //  "Entity.extend8":{
            //      label: "extend8",
            //      rules: [],
            //         type: "input"
            // },
            //  "Entity.extend9":{
            //      label: "extend9",
            //      rules: [],
            //         type: "input"
            // },
             "Entity.externalOutDtlId":{
                 label: this.$t("wmsoutinvoicedtl.externalOutDtlId"),
                 rules: [],
                    type: "input"
            },
             "Entity.externalOutNo":{
                 label: this.$t("wmsoutinvoicedtl.externalOutNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.invoiceDtlStatus":{
                 label: this.$t("wmsoutinvoicedtl.invoiceDtlStatus"),
                 rules: [{ required: true, message: this.$t("wmsoutinvoicedtl.invoiceDtlStatus")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.invoiceNo":{
                 label: this.$t("wmsoutinvoicedtl.invoiceNo"),
                 rules: [{ required: true, message: this.$t("wmsoutinvoicedtl.invoiceNo")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.materialName":{
                 label: this.$t("wmsoutinvoicedtl.materialName"),
                 rules: [],
                    type: "input"
            },
             "Entity.materialCode":{
                 label: this.$t("wmsoutinvoicedtl.materialCode"),
                 rules: [{ required: true, message: this.$t("wmsoutinvoicedtl.materialCode")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.materialSpec":{
                 label: this.$t("wmsoutinvoicedtl.materialSpec"),
                 rules: [],
                    type: "input"
            },
             "Entity.orderDtlId":{
                 label: this.$t("wmsoutinvoicedtl.orderDtlId"),
                 rules: [],
                    type: "input"
            },
             "Entity.orderNo":{
                 label: this.$t("wmsoutinvoicedtl.orderNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.productLocation":{
                 label: this.$t("wmsoutinvoicedtl.productLocation"),
                 rules: [],
                    type: "input"
            },
             "Entity.productDeptCode":{
                 label: this.$t("wmsoutinvoicedtl.productDeptCode"),
                 rules: [],
                    type: "input"
            },
             "Entity.productDeptName":{
                 label: this.$t("wmsoutinvoicedtl.productDeptName"),
                 rules: [],
                    type: "input"
            },
             "Entity.projectNo":{
                 label: this.$t("wmsoutinvoicedtl.projectNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.proprietorCode":{
                 label: this.$t("wmsoutinvoicedtl.proprietorCode"),
                 rules: [],
                    type: "input"
            },
             "Entity.invoiceQty":{
                 label: this.$t("wmsoutinvoicedtl.invoiceQty"),
                 rules: [{ required: true, message: this.$t("wmsoutinvoicedtl.invoiceQty")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.putdownQty":{
                 label: this.$t("wmsoutinvoicedtl.putdownQty"),
                 rules: [{ required: true, message: this.$t("wmsoutinvoicedtl.putdownQty")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.productSn":{
                 label: this.$t("wmsoutinvoicedtl.productSn"),
                 rules: [],
                    type: "input"
            },
             "Entity.originalSn":{
                 label: this.$t("wmsoutinvoicedtl.originalSn"),
                 rules: [],
                    type: "input"
            },
             "Entity.supplyType":{
                 label: this.$t("wmsoutinvoicedtl.supplyType"),
                 rules: [],
                    type: "input"
            },
             "Entity.ticketPlanBeginTime":{
                 label: this.$t("wmsoutinvoicedtl.ticketPlanBeginTime"),
                 rules: [],
                    type: "datePicker"
            },
             "Entity.waveNo":{
                 label: this.$t("wmsoutinvoicedtl.waveNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.whouseNo":{
                 label: this.$t("wmsoutinvoicedtl.whouseNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.companyCode":{
                 label: this.$t("wmsoutinvoicedtl.companyCode"),
                 rules: [],
                    type: "input"
            },
            //  "Entity.intfId":{
            //      label: this.$t("wmsoutinvoicedtl.intfId"),
            //      rules: [],
            //         type: "input"
            // },
            //  "Entity.intfBatchId":{
            //      label: this.$t("wmsoutinvoicedtl.intfBatchId"),
            //      rules: [],
            //         type: "input"
            // },
             "Entity.supplierCode":{
                 label: this.$t("wmsoutinvoicedtl.supplierCode"),
                 rules: [],
                    type: "input"
            },
             "Entity.supplierName":{
                 label: this.$t("wmsoutinvoicedtl.supplierName"),
                 rules: [],
                    type: "input"
            },
            //  "Entity.supplierNameEn":{
            //      label: this.$t("wmsoutinvoicedtl.supplierNameEn"),
            //      rules: [],
            //         type: "input"
            // },
            //  "Entity.supplierNameAlias":{
            //      label: this.$t("wmsoutinvoicedtl.supplierNameAlias"),
            //      rules: [],
            //         type: "input"
            // },
             "Entity.ticketNo":{
                 label: this.$t("wmsoutinvoicedtl.ticketNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.unitCode":{
                 label: this.$t("wmsoutinvoicedtl.unitCode"),
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
