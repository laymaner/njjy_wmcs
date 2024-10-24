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
             "Entity.badDescription":{
                 label: "不良说明",
                 rules: [],
                    type: "input"
            },
             "Entity.badOptions":{
                 label: "不良选项",
                 rules: [],
                    type: "input"
            },
             "Entity.badSolveType":{
                 label: "不良处理方式",
                 rules: [],
                    type: "input"
            },
             "Entity.batchNo":{
                 label: "批次",
                 rules: [],
                    type: "input"
            },
             "Entity.binNo":{
                 label: "库位编码",
                 rules: [{ required: true, message: "库位编码"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.docTypeCode":{
                 label: "docTypeCode",
                 rules: [{ required: true, message: "docTypeCode"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.erpWhouseNo":{
                 label: "ERP仓库",
                 rules: [{ required: true, message: "ERP仓库"+this.$t("form.notnull"),trigger: "blur" }],
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
                 rules: [{ required: true, message: "inDtlId"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.inNo":{
                 label: "WMS入库单号",
                 rules: [{ required: true, message: "WMS入库单号"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.inOutName":{
                 label: "inOutName",
                 rules: [],
                    type: "input"
            },
             "Entity.inOutTypeNo":{
                 label: "inOutTypeNo",
                 rules: [],
                    type: "input"
            },
             "Entity.inspector":{
                 label: "质检员",
                 rules: [],
                    type: "input"
            },
             "Entity.iqcRecordNo":{
                 label: "WMS检验记录单号",
                 rules: [{ required: true, message: "WMS检验记录单号"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.iqcRecordStatus":{
                 label: "检验结果状态",
                 rules: [{ required: true, message: "检验结果状态"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.iqcType":{
                 label: "质检方式",
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
             "Entity.minPkgQty":{
                 label: "包装数量",
                 rules: [],
                    type: "input"
            },
             "Entity.orderDtlId":{
                 label: "orderDtlId",
                 rules: [],
                    type: "input"
            },
             "Entity.orderNo":{
                 label: "关联单号",
                 rules: [],
                    type: "input"
            },
             "Entity.orderDesc":{
                 label: "备注说明",
                 rules: [],
                    type: "input"
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
             "Entity.qualifiedQty":{
                 label: "检验合格数量",
                 rules: [],
                    type: "input"
            },
             "Entity.erpQualifiedSpecialQty":{
                 label: "ERP特采数量",
                 rules: [],
                    type: "input"
            },
             "Entity.receiptDtlId":{
                 label: "receiptDtlId",
                 rules: [{ required: true, message: "receiptDtlId"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.receiptNo":{
                 label: "WMS收货单号",
                 rules: [{ required: true, message: "WMS收货单号"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.receiptQty":{
                 label: "收货完成数量",
                 rules: [{ required: true, message: "收货完成数量"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.regionNo":{
                 label: "库区编码",
                 rules: [{ required: true, message: "库区编码"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.sourceBy":{
                 label: "sourceBy",
                 rules: [{ required: true, message: "sourceBy"+this.$t("form.notnull"),trigger: "blur" }],
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
             "Entity.wmsUnqualifiedQty":{
                 label: "检验不合格数量",
                 rules: [],
                    type: "input"
            },
             "Entity.erpUnqualifiedQty":{
                 label: "ERP不合格数量",
                 rules: [],
                    type: "input"
            },
             "Entity.urgentFlag":{
                 label: "急料标记",
                 rules: [],
                    type: "input"
            },
             "Entity.unitCode":{
                 label: "单位",
                 rules: [],
                    type: "input"
            },
             "Entity.whouseNo":{
                 label: "whouseNo",
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
