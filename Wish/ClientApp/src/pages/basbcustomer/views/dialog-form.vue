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
             "Entity.Address":{
                 label: "Address",
                 rules: [],
                    type: "input"
            },
             "Entity.Contacts":{
                 label: "Contacts",
                 rules: [],
                    type: "input"
            },
             "Entity.CustomerFullname":{
                 label: "CustomerFullname",
                 rules: [],
                    type: "input"
            },
             "Entity.CustomerFullnameAlias":{
                 label: "CustomerFullnameAlias",
                 rules: [],
                    type: "input"
            },
             "Entity.CustomerFullnameEn":{
                 label: "CustomerFullnameEn",
                 rules: [],
                    type: "input"
            },
             "Entity.CustomerName":{
                 label: "CustomerName",
                 rules: [{ required: true, message: "CustomerName"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.CustomerNameAlias":{
                 label: "CustomerNameAlias",
                 rules: [],
                    type: "input"
            },
             "Entity.CustomerNameEn":{
                 label: "CustomerNameEn",
                 rules: [],
                    type: "input"
            },
             "Entity.CustomerCode":{
                 label: "CustomerCode",
                 rules: [{ required: true, message: "CustomerCode"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.Description":{
                 label: "Description",
                 rules: [],
                    type: "input"
            },
             "Entity.Fax":{
                 label: "Fax",
                 rules: [],
                    type: "input"
            },
             "Entity.Mail":{
                 label: "Mail",
                 rules: [],
                    type: "input"
            },
             "Entity.Mobile":{
                 label: "Mobile",
                 rules: [],
                    type: "input"
            },
             "Entity.Phone":{
                 label: "Phone",
                 rules: [],
                    type: "input"
            },
             "Entity.ProprietorCode":{
                 label: "ProprietorCode",
                 rules: [],
                    type: "input"
            },
             "Entity.UsedFlag":{
                 label: "UsedFlag",
                 rules: [{ required: true, message: "UsedFlag"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.WhouseNo":{
                 label: "WhouseNo",
                 rules: [],
                    type: "input"
            },
             "Entity.Zip":{
                 label: "Zip",
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
