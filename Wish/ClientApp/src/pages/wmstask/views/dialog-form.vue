<template>
    <wtm-dialog-box :is-show.sync="isShow" :status="status" :events="formEvent">
        <wtm-create-form :ref="refName" :status="status" :options="formOptions" ></wtm-create-form>
        <el-card>   </el-card>
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
                "label-width": this.$t("wmstask.LabelWidthForm")
            },
            formItem: {
                "Entity.ID": {
                    isHidden: true
                },
                "Entity.orderNo":{
                 label: this.$t("wmstask.orderNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.palletBarcode":{
                 label: this.$t("wmstask.palletBarcode"),
                 rules: [{ required: true, message: this.$t("wmstask.palletBarcode")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
            // "Entity.wmsTaskNo":{
            //      label: this.$t("wmstask.wmsTaskNo"),
            //      rules: [{ required: true, message: "wmsTaskNo"+this.$t("form.notnull"),trigger: "blur" }],
            //         type: "input"
            // },
            "Entity.stockCode":{
                 label: this.$t("wmstask.stockCode"),
                 rules: [],
                    type: "input"
            },
             "Entity.feedbackDesc":{
                 label: this.$t("wmstask.feedbackDesc"),
                 rules: [],
                    type: "input"
            },
             "Entity.feedbackStatus":{
                 label: this.$t("wmstask.feedbackStatus"),
                 rules: [{ required: true, message: this.$t("wmstask.feedbackStatus")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.frLocationNo":{
                 label: this.$t("wmstask.frLocationNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.frLocationType":{
                 label: this.$t("wmstask.frLocationType"),
                 rules: [],
                    type: "input"
            },
             "Entity.loadedType":{
                 label: this.$t("wmstask.loadedType"),
                 rules: [],
                    type: "input"
            },
             "Entity.matHeight":{
                 label: this.$t("wmstask.matHeight"),
                 rules: [],
                    type: "input"
            },
             "Entity.matLength":{
                 label: this.$t("wmstask.matLength"),
                 rules: [],
                    type: "input"
            },
             "Entity.matQty":{
                 label: this.$t("wmstask.matQty"),
                 rules: [],
                    type: "input"
            },
             "Entity.matWeight":{
                 label: this.$t("wmstask.matWeight"),
                 rules: [],
                    type: "input"
            },
             "Entity.matWidth":{
                 label: this.$t("wmstask.matWidth"),
                 rules: [],
                    type: "input"
            },
             
             "Entity.proprietorCode":{
                 label: this.$t("wmstask.proprietorCode"),
                 rules: [],
                    type: "input"
            },
             "Entity.regionNo":{
                 label: this.$t("wmstask.regionNo"),
                 rules: [{ required: true, message: this.$t("wmstask.regionNo")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.roadwayNo":{
                 label: this.$t("wmstask.roadwayNo"),
                 rules: [{ required: true, message: this.$t("wmstask.roadwayNo")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             
             "Entity.taskDesc":{
                 label: this.$t("wmstask.taskDesc"),
                 rules: [],
                    type: "input"
            },
             "Entity.taskPriority":{
                 label: this.$t("wmstask.taskPriority"),
                 rules: [{ required: true, message: this.$t("wmstask.taskPriority")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.taskStatus":{
                 label: this.$t("wmstask.taskStatus"),
                 rules: [{ required: true, message: this.$t("wmstask.taskStatus")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.taskTypeNo":{
                 label: this.$t("wmstask.taskTypeNo"),
                 rules: [{ required: true, message: this.$t("wmstask.taskTypeNo")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.toLocationNo":{
                 label: this.$t("wmstask.toLocationNo"),
                 rules: [{ required: true, message: this.$t("wmstask.toLocationNo")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.toLocationType":{
                 label: this.$t("wmstask.toLocationType"),
                 rules: [],
                    type: "input"
            },
             "Entity.whouseNo":{
                 label: this.$t("wmstask.whouseNo"),
                 rules: [],
                    type: "input"
            },
             
             "Entity.wmsTaskType":{
                 label: this.$t("wmstask.wmsTaskType"),
                 rules: [{ required: true, message: this.$t("wmstask.wmsTaskType")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            }
                
            }
        };
    }
    beforeOpen() {

    }
}
</script>
