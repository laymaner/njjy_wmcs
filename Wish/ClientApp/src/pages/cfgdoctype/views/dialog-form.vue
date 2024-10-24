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
             "Entity.businessCode":{
                 label: "businessCode",
                 rules: [{ required: true, message: "businessCode"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.cvType":{
                 label: "cvType",
                 rules: [],
                    type: "input"
            },
             "Entity.developFlag":{
                 label: "developFlag",
                 rules: [{ required: true, message: "developFlag"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.docPriority":{
                 label: "docPriority",
                 rules: [{ required: true, message: "docPriority"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.docTypeCode":{
                 label: "docTypeCode",
                 rules: [{ required: true, message: "docTypeCode"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.docTypeDesc":{
                 label: "docTypeDesc",
                 rules: [],
                    type: "input"
            },
             "Entity.docTypeName":{
                 label: "docTypeName",
                 rules: [{ required: true, message: "docTypeName"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.docTypeNameAlias":{
                 label: "docTypeNameAlias",
                 rules: [],
                    type: "input"
            },
             "Entity.docTypeNameEn":{
                 label: "docTypeNameEn",
                 rules: [],
                    type: "input"
            },
             "Entity.externalDocTypeCode":{
                 label: "externalDocTypeCode",
                 rules: [],
                    type: "input"
            },
             "Entity.taskMaxQty":{
                 label: "taskMaxQty",
                 rules: [],
                    type: "input"
            },
             "Entity.usedFlag":{
                 label: "usedFlag",
                 rules: [{ required: true, message: "usedFlag"+this.$t("form.notnull"),trigger: "blur" }],
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
