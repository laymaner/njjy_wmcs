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
             "Entity.blindFlag":{
                 label: this.$t("wmsitninventoryrecordhis.blindFlag"),
                 rules: [{ required: true, message: "blindFlag"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.confirmBy":{
                 label: this.$t("wmsitninventoryrecordhis.confirmBy"),
                 rules: [],
                    type: "input"
            },
             "Entity.confirmQty":{
                 label: this.$t("wmsitninventoryrecordhis.confirmQty"),
                 rules: [],
                    type: "input"
            },
             "Entity.confirmReason":{
                 label: this.$t("wmsitninventoryrecordhis.confirmReason"),
                 rules: [],
                    type: "input"
            },
             "Entity.differenceFlag":{
                 label: this.$t("wmsitninventoryrecordhis.differenceFlag"),
                 rules: [{ required: true, message: "differenceFlag"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.docTypeCode":{
                 label: this.$t("wmsitninventoryrecordhis.docTypeCode"),
                 rules: [{ required: true, message: "docTypeCode"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.erpWhouseNo":{
                 label: this.$t("wmsitninventoryrecordhis.erpWhouseNo"),
                 rules: [{ required: true, message: "erpWhouseNo"+this.$t("form.notnull"),trigger: "blur" }],
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
                 label: this.$t("wmsitninventoryrecordhis.inspectionResult"),
                 rules: [],
                    type: "input"
            },
             "Entity.inventoryBy":{
                 label: this.$t("wmsitninventoryrecordhis.inventoryBy"),
                 rules: [],
                    type: "input"
            },
             "Entity.inventoryDtlId":{
                 label: this.$t("wmsitninventoryrecordhis.inventoryDtlId"),
                 rules: [{ required: true, message: "inventoryDtlId"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.inventoryNo":{
                 label: this.$t("wmsitninventoryrecordhis.inventoryNo"),
                 rules: [{ required: true, message: "inventoryNo"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.inventoryQty":{
                 label: this.$t("wmsitninventoryrecordhis.inventoryQty"),
                 rules: [{ required: true, message: "inventoryQty"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.inventoryReason":{
                 label: this.$t("wmsitninventoryrecordhis.inventoryReason"),
                 rules: [],
                    type: "input"
            },
             "Entity.inventoryRecordStatus":{
                 label: this.$t("wmsitninventoryrecordhis.inventoryRecordStatus"),
                 rules: [{ required: true, message: "inventoryRecordStatus"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.materialName":{
                 label: this.$t("wmsitninventoryrecordhis.materialName"),
                 rules: [],
                    type: "input"
            },
             "Entity.materialCode":{
                 label: this.$t("wmsitninventoryrecordhis.materialCode"),
                 rules: [{ required: true, message: "materialCode"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.materialSpec":{
                 label: this.$t("wmsitninventoryrecordhis.materialSpec"),
                 rules: [],
                    type: "input"
            },
             "Entity.occupyQty":{
                 label: this.$t("wmsitninventoryrecordhis.occupyQty"),
                 rules: [{ required: true, message: "occupyQty"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.palletBarcode":{
                 label: this.$t("wmsitninventoryrecordhis.palletBarcode"),
                 rules: [{ required: true, message: "palletBarcode"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.projectNo":{
                 label: this.$t("wmsitninventoryrecordhis.projectNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.proprietorCode":{
                 label: this.$t("wmsitninventoryrecordhis.proprietorCode"),
                 rules: [],
                    type: "input"
            },
             "Entity.putdownLocNo":{
                 label: this.$t("wmsitninventoryrecordhis.putdownLocNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.skuCode":{
                 label: this.$t("wmsitninventoryrecordhis.skuCode"),
                 rules: [],
                    type: "input"
            },
             "Entity.stockCode":{
                 label: this.$t("wmsitninventoryrecordhis.stockCode"),
                 rules: [{ required: true, message: "stockCode"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.stockDtlId":{
                 label: this.$t("wmsitninventoryrecordhis.stockDtlId"),
                 rules: [{ required: true, message: "stockDtlId"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.stockQty":{
                 label: this.$t("wmsitninventoryrecordhis.stockQty"),
                 rules: [{ required: true, message: "stockQty"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.supplierCode":{
                 label: this.$t("wmsitninventoryrecordhis.supplierCode"),
                 rules: [],
                    type: "input"
            },
             "Entity.supplierName":{
                 label: this.$t("wmsitninventoryrecordhis.supplierName"),
                 rules: [],
                    type: "input"
            },
             "Entity.supplierNameAlias":{
                 label: this.$t("wmsitninventoryrecordhis.supplierNameAlias"),
                 rules: [],
                    type: "input"
            },
             "Entity.supplierNameEn":{
                 label: this.$t("wmsitninventoryrecordhis.supplierNameEn"),
                 rules: [],
                    type: "input"
            },
             "Entity.whouseNo":{
                 label: this.$t("wmsitninventoryrecordhis.whouseNo"),
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
