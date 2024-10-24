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
             "Entity.interfaceCode":{
                 label: "interfaceCode",
                 rules: [],
                    type: "input"
            },
             "Entity.interfaceName":{
                 label: "interfaceName",
                 rules: [],
                    type: "input"
            },
             "Entity.interfaceSendInfo":{
                 label: "interfaceSendInfo",
                 rules: [],
                    type: "input"
            },
             "Entity.interfaceResult":{
                 label: "interfaceResult",
                 rules: [],
                    type: "input"
            },
             "Entity.returnFlag":{
                 label: "returnFlag",
                 rules: [{ required: true, message: "returnFlag"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.returnTimes":{
                 label: "returnTimes",
                 rules: [{ required: true, message: "returnTimes"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            }
                
            }
        };
    }
    beforeOpen() {

    }
}
</script>
