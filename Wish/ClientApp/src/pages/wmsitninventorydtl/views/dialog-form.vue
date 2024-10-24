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
                 label: "批次号",
                 rules: [],
                    type: "input"
            },
             "Entity.confirmQty":{
                 label: "确认数量",
                 rules: [{ required: true, message: "确认数量"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.difFlag":{
                 label: "是否差异",
                 rules: [{ required: true, message: "是否差异"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.inspectionResult":{
                 label: "inspectionResult",
                 rules: [],
                    type: "input"
            },
             "Entity.inventoryDtlStatus":{
                 label: "盘点明细状态",
                 rules: [{ required: true, message: "盘点明细状态"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.inventoryNo":{
                 label: "盘点单号",
                 rules: [{ required: true, message: "盘点单号"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.inventoryQty":{
                 label: "盘点数量",
                 rules: [{ required: true, message: "盘点数量"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.materialCode":{
                 label: "物料编码",
                 rules: [{ required: true, message: "物料编码"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.materialName":{
                 label: "物料名称",
                 rules: [],
                    type: "input"
            },
             "Entity.materialSpec":{
                 label: "物料规格",
                 rules: [],
                    type: "input"
            },
             "Entity.proprietorCode":{
                 label: "proprietorCode",
                 rules: [],
                    type: "input"
            },
             "Entity.qty":{
                 label: "计划数量",
                 rules: [{ required: true, message: "计划数量"+this.$t("form.notnull"),trigger: "blur" }],
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
