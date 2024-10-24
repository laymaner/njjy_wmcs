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
             "Entity.strategyItemIdx":{
                 label: "strategyItemIdx",
                 rules: [],
                    type: "input"
            },
             "Entity.strategyItemNo":{
                 label: "strategyItemNo",
                 rules: [],
                    type: "input"
            },
             "Entity.strategyItemValue1":{
                 label: "strategyItemValue1",
                 rules: [],
                    type: "input"
            },
             "Entity.strategyItemValue2":{
                 label: "strategyItemValue2",
                 rules: [],
                    type: "input"
            },
             "Entity.strategyNo":{
                 label: "strategyNo",
                 rules: [],
                    type: "input"
            },
             "Entity.strategyTypeCode":{
                 label: "strategyTypeCode",
                 rules: [],
                    type: "input"
            },
             "Entity.usedFlag":{
                 label: "usedFlag",
                 rules: [{ required: true, message: "usedFlag"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            }
                
            }
        };
    }
    beforeOpen() {

    }
}
</script>
