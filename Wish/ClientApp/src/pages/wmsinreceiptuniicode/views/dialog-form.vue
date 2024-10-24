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
                 label: "批次",
                 rules: [],
                    type: "input"
            },
             "Entity.curPalletBarcode":{
                 label: "当前托盘号",
                 rules: [],
                    type: "input"
            },
             "Entity.curPositionNo":{
                 label: "当前位置编号",
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
             "Entity.dataCode":{
                 label: "DC",
                 rules: [],
                    type: "input"
            },
             "Entity.erpWhouseNo":{
                 label: "ERP仓库",
                 rules: [],
                    type: "input"
            },
             "Entity.expDate":{
                 label: "失效日期",
                 rules: [],
                    type: "datePicker"
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
             "Entity.externalInDtlId":{
                 label: "外部入库单行号",
                 rules: [],
                    type: "input"
            },
             "Entity.externalInNo":{
                 label: "外部入库单号",
                 rules: [],
                    type: "input"
            },
             "Entity.inDtlId":{
                 label: "inDtlId",
                 rules: [],
                    type: "input"
            },
             "Entity.inNo":{
                 label: "WMS入库单号",
                 rules: [],
                    type: "input"
            },
             "Entity.iqcResultNo":{
                 label: "WMS检验单号",
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
                 label: "规格",
                 rules: [],
                    type: "input"
            },
             "Entity.mslGradeCode":{
                 label: "MSL等级",
                 rules: [],
                    type: "input"
            },
             "Entity.productDate":{
                 label: "生产日期",
                 rules: [],
                    type: "datePicker"
            },
             "Entity.projectNo":{
                 label: "项目号",
                 rules: [],
                    type: "input"
            },
             "Entity.proprietorCode":{
                 label: "proprietorCode",
                 rules: [],
                    type: "input"
            },
             "Entity.receiptDtlId":{
                 label: "receiptDtlId",
                 rules: [],
                    type: "input"
            },
             "Entity.receiptNo":{
                 label: "WMS收货单号",
                 rules: [],
                    type: "input"
            },
             "Entity.qty":{
                 label: "数量",
                 rules: [{ required: true, message: "数量"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.recordQty":{
                 label: "实际上架数量",
                 rules: [],
                    type: "input"
            },
             "Entity.receiptRecordId":{
                 label: "receiptRecordId",
                 rules: [],
                    type: "input"
            },
             "Entity.supplierExposeTimeDuration":{
                 label: "供应商暴露时长",
                 rules: [],
                    type: "input"
            },
             "Entity.supplierName":{
                 label: "供应商名称",
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
                 label: "供应商编码",
                 rules: [],
                    type: "input"
            },
             "Entity.skuCode":{
                 label: "SKU",
                 rules: [],
                    type: "input"
            },
             "Entity.uniicode":{
                 label: "包装条码/SN号",
                 rules: [{ required: true, message: "包装条码/SN号"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.unitCode":{
                 label: "单位",
                 rules: [{ required: true, message: "单位"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.whouseNo":{
                 label: "whouseNo",
                 rules: [],
                    type: "input"
            },
             "Entity.runiiStatus":{
                 label: "入库唯一码状态",
                 rules: [{ required: true, message: "入库唯一码状态"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            }
                
            }
        };
    }
    beforeOpen() {

    }
}
</script>
