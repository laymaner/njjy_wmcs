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
             "Entity.batchNo":{
                 label: this.$t("wmsitninventoryrecorddif.batchNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.dataCode":{
                 label: this.$t("wmsitninventoryrecorddif.dataCode"),
                 rules: [],
                    type: "input"
            },
             "Entity.delayFrozenFlag":{
                 label: this.$t("wmsitninventoryrecorddif.delayFrozenFlag"),
                 rules: [],
                    type: "input"
            },
             "Entity.delayFrozenReason":{
                 label: this.$t("wmsitninventoryrecorddif.delayFrozenReason"),
                 rules: [],
                    type: "input"
            },
             "Entity.delayReason":{
                 label: this.$t("wmsitninventoryrecorddif.delayReason"),
                 rules: [],
                    type: "input"
            },
             "Entity.delayTimes":{
                 label: this.$t("wmsitninventoryrecorddif.delayTimes"),
                 rules: [],
                    type: "input"
            },
             "Entity.delayToEndDate":{
                 label: this.$t("wmsitninventoryrecorddif.delayToEndDate"),
                 rules: [],
                    type: "datePicker"
            },
             "Entity.difQty":{
                 label: this.$t("wmsitninventoryrecorddif.difQty"),
                 rules: [{ required: true, message: "difQty"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.driedScrapFlag":{
                 label: this.$t("wmsitninventoryrecorddif.driedScrapFlag"),
                 rules: [],
                    type: "input"
            },
             "Entity.driedTimes":{
                 label: this.$t("wmsitninventoryrecorddif.driedTimes"),
                 rules: [],
                    type: "input"
            },
             "Entity.expDate":{
                 label: this.$t("wmsitninventoryrecorddif.expDate"),
                 rules: [],
                    type: "datePicker"
            },
             "Entity.exposeFrozenFlag":{
                 label: this.$t("wmsitninventoryrecorddif.exposeFrozenFlag"),
                 rules: [],
                    type: "input"
            },
             "Entity.exposeFrozenReason":{
                 label: this.$t("wmsitninventoryrecorddif.exposeFrozenReason"),
                 rules: [],
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
             "Entity.inspectionResult":{
                 label: this.$t("wmsitninventoryrecorddif.inspectionResult"),
                 rules: [],
                    type: "input"
            },
             "Entity.inventoryDtlId":{
                 label: this.$t("wmsitninventoryrecorddif.inventoryDtlId"),
                 rules: [{ required: true, message: "inventoryDtlId"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.inventoryNo":{
                 label: this.$t("wmsitninventoryrecorddif.inventoryNo"),
                 rules: [{ required: true, message: "inventoryNo"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.inventoryQty":{
                 label: this.$t("wmsitninventoryrecorddif.inventoryQty"),
                 rules: [{ required: true, message: "inventoryQty"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.inventoryRecordId":{
                 label: this.$t("wmsitninventoryrecorddif.inventoryRecordId"),
                 rules: [{ required: true, message: "inventoryRecordId"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.leftMslTimes":{
                 label: this.$t("wmsitninventoryrecorddif.leftMslTimes"),
                 rules: [],
                    type: "input"
            },
             "Entity.materialName":{
                 label: this.$t("wmsitninventoryrecorddif.materialName"),
                 rules: [],
                    type: "input"
            },
             "Entity.materialCode":{
                 label: this.$t("wmsitninventoryrecorddif.materialCode"),
                 rules: [{ required: true, message: "materialCode"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.materialSpec":{
                 label: this.$t("wmsitninventoryrecorddif.materialSpec"),
                 rules: [],
                    type: "input"
            },
             "Entity.mslGradeCode":{
                 label: this.$t("wmsitninventoryrecorddif.mslGradeCode"),
                 rules: [],
                    type: "input"
            },
             "Entity.packageTime":{
                 label: this.$t("wmsitninventoryrecorddif.packageTime"),
                 rules: [],
                    type: "datePicker"
            },
             "Entity.palletBarcode":{
                 label: this.$t("wmsitninventoryrecorddif.palletBarcode"),
                 rules: [{ required: true, message: "palletBarcode"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.positionNo":{
                 label: this.$t("wmsitninventoryrecorddif.positionNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.productDate":{
                 label: this.$t("wmsitninventoryrecorddif.productDate"),
                 rules: [],
                    type: "datePicker"
            },
             "Entity.projectNo":{
                 label: this.$t("wmsitninventoryrecorddif.projectNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.proprietorCode":{
                 label: this.$t("wmsitninventoryrecorddif.proprietorCode"),
                 rules: [],
                    type: "input"
            },
             "Entity.realExposeTimes":{
                 label: this.$t("wmsitninventoryrecorddif.realExposeTimes"),
                 rules: [],
                    type: "input"
            },
             "Entity.skuCode":{
                 label: this.$t("wmsitninventoryrecorddif.skuCode"),
                 rules: [],
                    type: "input"
            },
             "Entity.stockCode":{
                 label: this.$t("wmsitninventoryrecorddif.stockCode"),
                 rules: [{ required: true, message: "stockCode"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.stockDtlId":{
                 label: this.$t("wmsitninventoryrecorddif.stockDtlId"),
                 rules: [{ required: true, message: "stockDtlId"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.supplierCode":{
                 label: this.$t("wmsitninventoryrecorddif.supplierCode"),
                 rules: [],
                    type: "input"
            },
             "Entity.supplierExposeTimes":{
                 label: this.$t("wmsitninventoryrecorddif.supplierExposeTimes"),
                 rules: [],
                    type: "input"
            },
             "Entity.supplierName":{
                 label: this.$t("wmsitninventoryrecorddif.supplierName"),
                 rules: [],
                    type: "input"
            },
             "Entity.supplierNameAlias":{
                 label: this.$t("wmsitninventoryrecorddif.supplierNameAlias"),
                 rules: [],
                    type: "input"
            },
             "Entity.supplierNameEn":{
                 label: this.$t("wmsitninventoryrecorddif.supplierNameEn"),
                 rules: [],
                    type: "input"
            },
             "Entity.uniicode":{
                 label: this.$t("wmsitninventoryrecorddif.uniicode"),
                 rules: [{ required: true, message: "uniicode"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.unpackTime":{
                 label: this.$t("wmsitninventoryrecorddif.unpackTime"),
                 rules: [],
                    type: "datePicker"
            },
             "Entity.whouseNo":{
                 label: this.$t("wmsitninventoryrecorddif.whouseNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.areaNo":{
                 label: this.$t("wmsitninventoryrecorddif.areaNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.delFlag":{
                 label: this.$t("wmsitninventoryrecorddif.delFlag"),
                 rules: [],
                    type: "input"
            },
             "Entity.erpWhouseNo":{
                 label: this.$t("wmsitninventoryrecorddif.erpWhouseNo"),
                 rules: [{ required: true, message: "erpWhouseNo"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.inwhTime":{
                 label: this.$t("wmsitninventoryrecorddif.inwhTime"),
                 rules: [],
                    type: "datePicker"
            },
             "Entity.occupyQty":{
                 label: this.$t("wmsitninventoryrecorddif.occupyQty"),
                 rules: [],
                    type: "input"
            },
             "Entity.qty":{
                 label: this.$t("wmsitninventoryrecorddif.qty"),
                 rules: [],
                    type: "input"
            },
             "Entity.unitCode":{
                 label: this.$t("wmsitninventoryrecorddif.unitCode"),
                 rules: [],
                    type: "input"
            },
             "Entity.fileedId":{
                 label: this.$t("wmsitninventoryrecorddif.fileedId"),
                 rules: [],
                    type: "input"
            },
             "Entity.fileedName":{
                 label: this.$t("wmsitninventoryrecorddif.fileedName"),
                 rules: [],
                    type: "input"
            },
             "Entity.oldStockDtlId":{
                 label: this.$t("wmsitninventoryrecorddif.oldStockDtlId"),
                 rules: [{ required: true, message: "oldStockDtlId"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.projectNoBak":{
                 label: this.$t("wmsitninventoryrecorddif.projectNoBak"),
                 rules: [],
                    type: "input"
            },
             "Entity.unpackStatus":{
                 label: this.$t("wmsitninventoryrecorddif.unpackStatus"),
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
