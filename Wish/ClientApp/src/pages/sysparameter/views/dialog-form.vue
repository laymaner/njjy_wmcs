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
             "Entity.developFlag":{
                 label: "developFlag",
                 rules: [{ required: true, message: "developFlag"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.parCode":{
                 label: "parCode",
                 rules: [{ required: true, message: "parCode"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.parDesc":{
                 label: "parDesc",
                 rules: [],
                    type: "input"
            },
             "Entity.parDescAlias":{
                 label: "parDescAlias",
                 rules: [],
                    type: "input"
            },
             "Entity.parDescEn":{
                 label: "parDescEn",
                 rules: [],
                    type: "input"
            },
             "Entity.parValue":{
                 label: "parValue",
                 rules: [],
                    type: "input"
            },
             "Entity.parValueType":{
                 label: "parValueType",
                 rules: [{ required: true, message: "parValueType"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            }
                
            }
        };
    }
    beforeOpen() {

    }
}
</script>
