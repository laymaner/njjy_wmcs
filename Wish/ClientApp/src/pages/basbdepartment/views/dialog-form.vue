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
             "Entity.DepartmentCode":{
                 label: "DepartmentCode",
                 rules: [{ required: true, message: "DepartmentCode"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.DepartmentName":{
                 label: "DepartmentName",
                 rules: [{ required: true, message: "DepartmentName"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.DepartmentNameAlias":{
                 label: "DepartmentNameAlias",
                 rules: [],
                    type: "input"
            },
             "Entity.DepartmentNameEn":{
                 label: "DepartmentNameEn",
                 rules: [],
                    type: "input"
            },
             "Entity.UsedFlag":{
                 label: "UsedFlag",
                 rules: [{ required: true, message: "UsedFlag"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            }
                
            }
        };
    }
    beforeOpen() {

    }
}
</script>
