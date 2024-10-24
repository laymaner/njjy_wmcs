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
             "Entity.erpWhouseNo":{
                 label: "erpWhouseNo",
                 rules: [{ required: true, message: "erpWhouseNo"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.priority":{
                 label: "priority",
                 rules: [{ required: true, message: "priority"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.erpWhouseName":{
                 label: "erpWhouseName",
                 rules: [],
                    type: "input"
            },
             "Entity.erpWhouseNameEn":{
                 label: "erpWhouseNameEn",
                 rules: [],
                    type: "input"
            },
             "Entity.erpWhouseNameAlias":{
                 label: "erpWhouseNameAlias",
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
