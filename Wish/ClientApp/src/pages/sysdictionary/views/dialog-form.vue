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
             "Entity.dictionaryCode":{
                 label: "dictionaryCode",
                 rules: [{ required: true, message: "dictionaryCode"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.dictionaryItemCode":{
                 label: "dictionaryItemCode",
                 rules: [{ required: true, message: "dictionaryItemCode"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.dictionaryItemName":{
                 label: "dictionaryItemName",
                 rules: [{ required: true, message: "dictionaryItemName"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.dictionaryItemNameAlias":{
                 label: "dictionaryItemNameAlias",
                 rules: [],
                    type: "input"
            },
             "Entity.dictionaryItemNameEn":{
                 label: "dictionaryItemNameEn",
                 rules: [],
                    type: "input"
            },
             "Entity.dictionaryName":{
                 label: "dictionaryName",
                 rules: [{ required: true, message: "dictionaryName"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.dictionaryNameAlias":{
                 label: "dictionaryNameAlias",
                 rules: [],
                    type: "input"
            },
             "Entity.dictionaryNameEn":{
                 label: "dictionaryNameEn",
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
