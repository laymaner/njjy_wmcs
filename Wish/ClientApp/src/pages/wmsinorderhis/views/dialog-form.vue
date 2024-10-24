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
                 label: "区域编码",
                 rules: [{ required: true, message: "区域编码"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.cvCode":{
                 label: "cvCode",
                 rules: [],
                    type: "input"
            },
             "Entity.cvName":{
                 label: "cvName",
                 rules: [],
                    type: "input"
            },
             "Entity.cvNameAlias":{
                 label: "cvNameAlias",
                 rules: [],
                    type: "input"
            },
             "Entity.cvNameEn":{
                 label: "cvNameEn",
                 rules: [],
                    type: "input"
            },
             "Entity.cvType":{
                 label: "客供类型",
                 rules: [],
                    type: "input"
            },
             "Entity.deliverMode":{
                 label: "deliverMode",
                 rules: [],
                    type: "input"
            },
             "Entity.docTypeCode":{
                 label: "单据类型",
                 rules: [{ required: true, message: "单据类型"+this.$t("form.notnull"),trigger: "blur" }],
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
             "Entity.externalInId":{
                 label: "externalInId",
                 rules: [],
                    type: "input"
            },
             "Entity.externalInNo":{
                 label: "外部入库单号",
                 rules: [],
                    type: "input"
            },
             "Entity.inNo":{
                 label: "入库单号",
                 rules: [{ required: true, message: "入库单号"+this.$t("form.notnull"),trigger: "blur" }],
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
             "Entity.inStatus":{
                 label: "入库单状态",
                 rules: [{ required: true, message: "入库单状态"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.operationReason":{
                 label: "操作原因",
                 rules: [],
                    type: "input"
            },
             "Entity.orderDesc":{
                 label: "备注说明",
                 rules: [],
                    type: "input"
            },
             "Entity.planArrivalDate":{
                 label: "planArrivalDate",
                 rules: [],
                    type: "datePicker"
            },
             "Entity.proprietorCode":{
                 label: "proprietorCode",
                 rules: [],
                    type: "input"
            },
             "Entity.sourceBy":{
                 label: "数据来源",
                 rules: [{ required: true, message: "数据来源"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.ticketNo":{
                 label: "ticketNo",
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
