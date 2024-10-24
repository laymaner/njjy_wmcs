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
             "Entity.Device_Code":{
                 label: this.$t("devicestatuslog.Device_Code"),
                 rules: [{ required: true, message: this.$t("devicestatuslog.Device_Code")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.Attribute":{
                 label: this.$t("devicestatuslog.Attribute"),
                 rules: [],
                    type: "input"
            },
             "Entity.Content":{
                 label: this.$t("devicestatuslog.Content"),
                 rules: [],
                    type: "input"
            },
             "Entity.Message":{
                 label: this.$t("devicestatuslog.Message"),
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
