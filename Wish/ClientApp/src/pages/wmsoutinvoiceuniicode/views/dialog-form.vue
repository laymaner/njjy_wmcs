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
                 label: "楼号",
                 rules: [{ required: true, message: "楼号"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.batchNo":{
                 label: "批次",
                 rules: [],
                    type: "input"
            },
             "Entity.dataCode":{
                 label: "DC",
                 rules: [],
                    type: "input"
            },
             "Entity.delayReason":{
                 label: "延期原因",
                 rules: [],
                    type: "input"
            },
             "Entity.delayTimes":{
                 label: "延期次数",
                 rules: [],
                    type: "input"
            },
             "Entity.delayToEndDate":{
                 label: "延长有效期至日期",
                 rules: [],
                    type: "datePicker"
            },
             "Entity.driedScrapFlag":{
                 label: "是否烘干报废",
                 rules: [],
                    type: "input"
            },
             "Entity.driedTimes":{
                 label: "已烘干次数",
                 rules: [],
                    type: "input"
            },
             "Entity.erpBinNo":{
                 label: "erpBinNo",
                 rules: [],
                    type: "input"
            },
             "Entity.erpWhouseNo":{
                 label: "ERP仓库",
                 rules: [{ required: true, message: "ERP仓库"+this.$t("form.notnull"),trigger: "blur" }],
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
             "Entity.inspectionResult":{
                 label: "质检结果",
                 rules: [],
                    type: "input"
            },
             "Entity.invoiceDtlId":{
                 label: "invoiceDtlId",
                 rules: [],
                    type: "input"
            },
             "Entity.invoiceNo":{
                 label: "WMS发货单号",
                 rules: [{ required: true, message: "WMS发货单号"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.invoiceRecordId":{
                 label: "invoiceRecordId",
                 rules: [],
                    type: "input"
            },
             "Entity.leftMslTimes":{
                 label: "剩余湿敏时长",
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
             "Entity.packageTime":{
                 label: "封包时间",
                 rules: [],
                    type: "datePicker"
            },
             "Entity.palletBarcode":{
                 label: "载体条码",
                 rules: [{ required: true, message: "载体条码"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.allotQty":{
                 label: "分配数量",
                 rules: [{ required: true, message: "分配数量"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.pickQty":{
                 label: "拣选数量",
                 rules: [{ required: true, message: "拣选数量"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.pickTaskNo":{
                 label: "拣选任务号",
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
             "Entity.realExposeTimes":{
                 label: "实际暴露时长",
                 rules: [],
                    type: "input"
            },
             "Entity.skuCode":{
                 label: "skuCode",
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
             "Entity.supplierCode":{
                 label: "供应商编码",
                 rules: [],
                    type: "input"
            },
             "Entity.supplierExposeTimes":{
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
             "Entity.uniicode":{
                 label: "包装条码/SN码",
                 rules: [{ required: true, message: "包装条码/SN码"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.unpackTime":{
                 label: "开封时间",
                 rules: [],
                    type: "datePicker"
            },
             "Entity.waveNo":{
                 label: "WMS波次号",
                 rules: [],
                    type: "input"
            },
             "Entity.whouseNo":{
                 label: "whouseNo",
                 rules: [],
                    type: "input"
            },
             "Entity.outBarcode":{
                 label: "出库条码",
                 rules: [],
                    type: "input"
            },
             "Entity.ouniiStatus":{
                 label: "包装条码状态",
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
