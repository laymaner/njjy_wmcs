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
             "Entity.businessDesc":{
                 label: "businessDesc",
                 rules: [],
                    type: "input"
            },
             "Entity.businessName":{
                 label: "businessName",
                 rules: [{ required: true, message: "businessName"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.businessNameAlias":{
                 label: "businessNameAlias",
                 rules: [],
                    type: "input"
            },
             "Entity.businessNameEn":{
                 label: "businessNameEn",
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
