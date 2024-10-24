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
                 label: "areaNo",
                 rules: [{ required: true, message: "areaNo"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.erpWhouseNo":{
                 label: "erpWhouseNo",
                 rules: [],
                    type: "input"
            },
             "Entity.extend1":{
                 label: "extend1",
                 rules: [],
                    type: "input"
            },
             "Entity.extend10":{
                 label: "extend10",
                 rules: [],
                    type: "input"
            },
             "Entity.extend11":{
                 label: "extend11",
                 rules: [],
                    type: "input"
            },
             "Entity.extend12":{
                 label: "extend12",
                 rules: [],
                    type: "input"
            },
             "Entity.extend13":{
                 label: "extend13",
                 rules: [],
                    type: "input"
            },
             "Entity.extend14":{
                 label: "extend14",
                 rules: [],
                    type: "input"
            },
             "Entity.extend15":{
                 label: "extend15",
                 rules: [],
                    type: "input"
            },
             "Entity.extend2":{
                 label: "extend2",
                 rules: [],
                    type: "input"
            },
             "Entity.extend3":{
                 label: "extend3",
                 rules: [],
                    type: "input"
            },
             "Entity.extend4":{
                 label: "extend4",
                 rules: [],
                    type: "input"
            },
             "Entity.extend5":{
                 label: "extend5",
                 rules: [],
                    type: "input"
            },
             "Entity.extend6":{
                 label: "extend6",
                 rules: [],
                    type: "input"
            },
             "Entity.extend7":{
                 label: "extend7",
                 rules: [],
                    type: "input"
            },
             "Entity.extend8":{
                 label: "extend8",
                 rules: [],
                    type: "input"
            },
             "Entity.extend9":{
                 label: "extend9",
                 rules: [],
                    type: "input"
            },
             "Entity.inspectionResult":{
                 label: "质量标记",
                 rules: [],
                    type: "input"
            },
             "Entity.materialName":{
                 label: "物料名称",
                 rules: [],
                    type: "input"
            },
             "Entity.materialCode":{
                 label: "物料编码",
                 rules: [{ required: true, message: "物料编码"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.materialSpec":{
                 label: "规格型号",
                 rules: [],
                    type: "input"
            },
             "Entity.occupyQty":{
                 label: "占用数量",
                 rules: [],
                    type: "input"
            },
             "Entity.palletBarcode":{
                 label: "载体条码",
                 rules: [{ required: true, message: "载体条码"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.projectNo":{
                 label: "projectNo",
                 rules: [],
                    type: "input"
            },
             "Entity.proprietorCode":{
                 label: "proprietorCode",
                 rules: [],
                    type: "input"
            },
             "Entity.putdownNo":{
                 label: "putdownNo",
                 rules: [{ required: true, message: "putdownNo"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.skuCode":{
                 label: "SKU编码",
                 rules: [],
                    type: "input"
            },
             "Entity.stockCode":{
                 label: "库存编码",
                 rules: [{ required: true, message: "库存编码"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.stockDtlId":{
                 label: "stockDtlId",
                 rules: [{ required: true, message: "stockDtlId"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.stockQty":{
                 label: "库存数量",
                 rules: [{ required: true, message: "库存数量"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.supplierName":{
                 label: "supplierName",
                 rules: [],
                    type: "input"
            },
             "Entity.supplierNameAlias":{
                 label: "supplierNameAlias",
                 rules: [],
                    type: "input"
            },
             "Entity.supplierNameEn":{
                 label: "supplierNameEn",
                 rules: [],
                    type: "input"
            },
             "Entity.supplierCode":{
                 label: "supplierCode",
                 rules: [],
                    type: "input"
            },
             "Entity.whouseNo":{
                 label: "whouseNo",
                 rules: [],
                    type: "input"
            },
             "Entity.putdownDtlStatus":{
                 label: "putdownDtlStatus",
                 rules: [],
                    type: "input"
            },
             "Entity.unitCode":{
                 label: "unitCode",
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
