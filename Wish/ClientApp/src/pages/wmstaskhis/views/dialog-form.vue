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
             "Entity.feedbackDesc":{
                 label: this.$t("wmstaskhis.feedbackDesc"),
                 rules: [],
                    type: "input"
            },
             "Entity.feedbackStatus":{
                 label: this.$t("wmstaskhis.feedbackStatus"),
                 rules: [{ required: true, message: this.$t("wmstaskhis.feedbackStatus")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.frLocationNo":{
                 label: this.$t("wmstaskhis.frLocationNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.frLocationType":{
                 label: this.$t("wmstaskhis.frLocationType"),
                 rules: [],
                    type: "input"
            },
             "Entity.loadedType":{
                 label: this.$t("wmstaskhis.loadedType"),
                 rules: [],
                    type: "input"
            },
             "Entity.matHeight":{
                 label: this.$t("wmstaskhis.matHeight"),
                 rules: [],
                    type: "input"
            },
             "Entity.matLength":{
                 label: this.$t("wmstaskhis.matLength"),
                 rules: [],
                    type: "input"
            },
             "Entity.matQty":{
                 label: this.$t("wmstaskhis.matQty"),
                 rules: [],
                    type: "input"
            },
             "Entity.matWeight":{
                 label: this.$t("wmstaskhis.matWeight"),
                 rules: [],
                    type: "input"
            },
             "Entity.matWidth":{
                 label: this.$t("wmstaskhis.matWidth"),
                 rules: [],
                    type: "input"
            },
             "Entity.orderNo":{
                 label: this.$t("wmstaskhis.orderNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.palletBarcode":{
                 label: this.$t("wmstaskhis.palletBarcode"),
                 rules: [{ required: true, message: this.$t("wmstaskhis.palletBarcode")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.proprietorCode":{
                 label: this.$t("wmstaskhis.proprietorCode"),
                 rules: [],
                    type: "input"
            },
             "Entity.regionNo":{
                 label: this.$t("wmstaskhis.regionNo"),
                 rules: [{ required: true, message: this.$t("wmstaskhis.regionNo")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.roadwayNo":{
                 label: this.$t("wmstaskhis.roadwayNo"),
                 rules: [{ required: true, message: this.$t("wmstaskhis.roadwayNo")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.stockCode":{
                 label: this.$t("wmstaskhis.stockCode"),
                 rules: [],
                    type: "input"
            },
             "Entity.taskDesc":{
                 label: this.$t("wmstaskhis.taskDesc"),
                 rules: [],
                    type: "input"
            },
             "Entity.taskPriority":{
                 label: this.$t("wmstaskhis.taskPriority"),
                 rules: [{ required: true, message: this.$t("wmstaskhis.taskPriority")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.taskStatus":{
                 label: this.$t("wmstaskhis.taskStatus"),
                 rules: [{ required: true, message: this.$t("wmstaskhis.taskStatus")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.taskTypeNo":{
                 label: this.$t("wmstaskhis.taskTypeNo"),
                 rules: [{ required: true, message: this.$t("wmstaskhis.taskTypeNo")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.toLocationNo":{
                 label: this.$t("wmstaskhis.toLocationNo"),
                 rules: [{ required: true, message: this.$t("wmstaskhis.toLocationNo")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.toLocationType":{
                 label: this.$t("wmstaskhis.toLocationType"),
                 rules: [],
                    type: "input"
            },
             "Entity.whouseNo":{
                 label: this.$t("wmstaskhis.whouseNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.wmsTaskNo":{
                 label: this.$t("wmstaskhis.wmsTaskNo"),
                 rules: [{ required: true, message: this.$t("wmstaskhis.wmsTaskNo")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.wmsTaskType":{
                 label: this.$t("wmstaskhis.wmsTaskType"),
                 rules: [{ required: true, message: this.$t("wmstaskhis.wmsTaskType")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            }
                
            }
        };
    }
    beforeOpen() {

    }
}
</script>
