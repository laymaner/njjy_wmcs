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
             "Entity.barcodeFlag":{
                 label: "码级管理",
                 rules: [{ required: true, message: "码级管理"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.batchNo":{
                 label: "批次",
                 rules: [],
                    type: "input"
            },
             "Entity.confirmQty":{
                 label: "CONFIRM_QTY",
                 rules: [],
                    type: "input"
            },
             "Entity.curLocationNo":{
                 label: "curLocationNo",
                 rules: [],
                    type: "input"
            },
             "Entity.curLocationType":{
                 label: "curLocationType",
                 rules: [],
                    type: "input"
            },
             "Entity.curPalletbarCode":{
                 label: "curPalletbarCode",
                 rules: [],
                    type: "input"
            },
             "Entity.curStockCode":{
                 label: "当前库存编码",
                 rules: [],
                    type: "input"
            },
             "Entity.curStockDtlId":{
                 label: "当前库存明细ID",
                 rules: [],
                    type: "input"
            },
             "Entity.docTypeCode":{
                 label: "docTypeCode",
                 rules: [],
                    type: "input"
            },
             "Entity.frLocationNo":{
                 label: "来源站台/库位号",
                 rules: [],
                    type: "input"
            },
             "Entity.frLocationType":{
                 label: "来源位置类型",
                 rules: [],
                    type: "input"
            },
             "Entity.frPalletBarcode":{
                 label: "来源托盘条码",
                 rules: [],
                    type: "input"
            },
             "Entity.frRegionNo":{
                 label: "frRegionNo",
                 rules: [],
                    type: "input"
            },
             "Entity.frStockCode":{
                 label: "frStockCode",
                 rules: [],
                    type: "input"
            },
             "Entity.frStockDtlId":{
                 label: "frStockDtlId",
                 rules: [],
                    type: "input"
            },
             "Entity.inspectionResult":{
                 label: "inspectionResult",
                 rules: [{ required: true, message: "inspectionResult"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.materialCode":{
                 label: "materialCode",
                 rules: [],
                    type: "input"
            },
             "Entity.materialSpec":{
                 label: "materialSpec",
                 rules: [],
                    type: "input"
            },
             "Entity.moveNo":{
                 label: "moveNo",
                 rules: [],
                    type: "input"
            },
             "Entity.moveDtlId":{
                 label: "moveDtlId",
                 rules: [],
                    type: "input"
            },
             "Entity.moveQty":{
                 label: "moveQty",
                 rules: [],
                    type: "input"
            },
             "Entity.moveRecordStatus":{
                 label: "moveRecordStatus",
                 rules: [],
                    type: "input"
            },
             "Entity.pickMethod":{
                 label: "pickMethod",
                 rules: [],
                    type: "input"
            },
             "Entity.pickType":{
                 label: "pickType",
                 rules: [],
                    type: "input"
            },
             "Entity.productDate":{
                 label: "productDate",
                 rules: [],
                    type: "datePicker"
            },
             "Entity.expDate":{
                 label: "expDate",
                 rules: [],
                    type: "datePicker"
            },
             "Entity.putDownLocNo":{
                 label: "putDownLocNo",
                 rules: [],
                    type: "input"
            },
             "Entity.skuCode":{
                 label: "skuCode",
                 rules: [],
                    type: "input"
            },
             "Entity.stockQty":{
                 label: "stockQty",
                 rules: [],
                    type: "input"
            },
             "Entity.supplierBatchNo":{
                 label: "supplierBatchNo",
                 rules: [],
                    type: "input"
            },
             "Entity.supplierCode":{
                 label: "supplierCode",
                 rules: [],
                    type: "input"
            },
             "Entity.supplierName":{
                 label: "供应商名称",
                 rules: [],
                    type: "input"
            },
             "Entity.supplierNameEn":{
                 label: "供应商名称-EN",
                 rules: [],
                    type: "input"
            },
             "Entity.supplierNameAlias":{
                 label: "supplierNameAlias",
                 rules: [],
                    type: "input"
            },
             "Entity.supplierType":{
                 label: "供货方类型：供应商、产线",
                 rules: [],
                    type: "input"
            },
             "Entity.toLocationNo":{
                 label: "目标库位/站台号",
                 rules: [],
                    type: "input"
            },
             "Entity.toLocationType":{
                 label: "toLocationType",
                 rules: [],
                    type: "input"
            },
             "Entity.toPalletBarcode":{
                 label: "toPalletBarcode",
                 rules: [],
                    type: "input"
            },
             "Entity.toRegionNo":{
                 label: "目标库区",
                 rules: [],
                    type: "input"
            },
             "Entity.toStockCode":{
                 label: "目标库存编码",
                 rules: [{ required: true, message: "目标库存编码"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.toStockDtlId":{
                 label: "目标库存明细ID",
                 rules: [],
                    type: "input"
            },
             "Entity.unitCode":{
                 label: "unitCode",
                 rules: [],
                    type: "input"
            },
             "Entity.proprietorCode":{
                 label: "货主",
                 rules: [],
                    type: "input"
            },
             "Entity.whouseNo":{
                 label: "仓库号",
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
