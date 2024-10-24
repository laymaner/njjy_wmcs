<template>
  <wtm-dialog-box :is-show.sync="isShow" :status="status" :events="formEvent">
    <wtm-create-form :ref="refName" :status="status" :options="formOptions" />
    <div class="table-container">
      <el-table :data="tableData" border style="width: 100%">
        <el-table-column v-for="column in columns" :key="column.prop" :label="column.label" :prop="column.prop">
          <template slot-scope="scope">
            <span>{{scope.row[column.prop]}}</span>
          </template>
        </el-table-column>
        <el-table-column width="80" v-sticky-column>
          <template slot-scope="scope">
            <el-button @click="deleteRow(scope.$index)">{{ $t("buttom.delete") }}</el-button>
          </template>
        </el-table-column>
      </el-table>
    </div>
    <el-button type="primary" style="background-color: #f8f9fa; color: black;" @click="openNewDialog" >{{ $t("buttom.add") }}</el-button>
    <!-- 添加新的弹窗 -->
    <el-dialog :title="$t('dialogTitle.addData')" :visible.sync="dialogVisible" :modal="false">
      <!-- 使用 el-form 直接包裹输入框 -->
      <el-form :inline="true">
        <el-form-item :label="$t('dialogTitle.materialCode')">
          <el-input v-model="searchFormData.materialCode" :placeholder="$t('dialogTitle.materialCode')"></el-input>
        </el-form-item>
        <el-form-item :label="this.$t('dialogTitle.materialName')">
          <el-input v-model="searchFormData.materialName" :placeholder="$t('dialogTitle.materialName')"></el-input>
        </el-form-item>
      </el-form>
      <el-button @click="searchData">{{ $t("buttom.search") }}</el-button>
      <el-button @click="resetSearch">{{ $t("buttom.reset") }}</el-button>
      <el-table :data="dialogTableData" border style="width: 100%">
        <el-table-column type="selection"></el-table-column> <!-- 复选框列 -->
        <el-table-column v-for="column in dialogColumns" :key="column.prop" :label="column.label" :prop="column.prop">
          <template slot-scope="scope">
            <el-input v-model="scope.row[column.prop]"></el-input>
          </template>
        </el-table-column>
        <!-- 分页组件 -->
        <el-pagination
          background
          layout="prev, pager, next"
          :total="totalItems"
          v-model:current-page="currentPage"
          :page-size="pageSize"
          @current-change="handlePageChange"
        />
      </el-table>
      <template #footer>
        <el-button @click="dialogVisible = false">{{ $t("buttom.cancel") }}</el-button>
        <el-button type="primary" @click="addDataFromDialog">{{ $t("buttom.determine") }}</el-button>
      </template>
    </el-dialog>
  </wtm-dialog-box>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { Action, State } from "vuex-class";
import formMixin from "@/vue-custom/mixin/form-mixin";
import UploadImg from "@/components/page/UploadImg.vue";
import axios from 'axios';
// 创建自定义指令
Vue.directive('sticky-column', {
  inserted: function (el, binding, vnode) {
    const tableContainer = el.closest('.table-container');
    tableContainer.addEventListener('scroll', function () {
      el.style.top = tableContainer.scrollTop + 'px';
    });
  }
});

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
        "Entity.supplierCode": {
          label: this.$t("wmsoutinvoicedtl.supplierCode"),
          rules: [],
          type: "input"
        },
        "Entity.docTypeCode": {
          label: this.$t("wmsoutinvoicedtl.docTypeCode"),
          rules: [],
          type: "input"
        }
      }
    };
  }

  columns: { label: string; prop: string }[] = [
    { label: this.$t("wmsoutinvoicedtl.materialCode"), prop: 'materialCode' },
    { label: this.$t("wmsoutinvoicedtl.materialName"), prop: 'materialName' },
    { label: this.$t("wmsoutinvoicedtl.materialSpec"), prop: 'materialSpec' },
    { label: this.$t("wmsoutinvoicedtl.invoiceQty"), prop: 'invoiceQty' },
    { label: this.$t("wmsoutinvoicedtl.erpWhouseNo"), prop: 'erpWhouseNo' }
  ];

  beforeOpen() {}

  // 初始化表格数据，这里使用对象数组来表示多行数据，每一行是一个包含字段值的对象
  data() {
    return {
      tableData: [],
      dialogVisible: false,
      dialogTableData: [],
      dialogColumns: [
        { label: this.$t("wmsoutinvoicedtl.materialCode"), prop: 'materialCode' },
        { label: this.$t("wmsoutinvoicedtl.materialName"), prop: 'materialName' },
        { label: this.$t("wmsoutinvoicedtl.materialSpec"), prop: 'materialSpec' },
        { label: this.$t("wmsoutinvoicedtl.qty"), prop: 'qty' },
        { label: this.$t("wmsoutinvoicedtl.occupyQty"), prop: 'occupyQty' },
        { label: this.$t("wmsoutinvoicedtl.unitName"), prop: 'unitName' },
        { label: this.$t("wmsoutinvoicedtl.erpWhouseNo"), prop: 'erpWhouseNo' }
        // 添加其他列定义
      ],
      totalItems: 0, // 总数据条数
      currentPage: 1, // 当前页码
      pageSize: 10,
      searchFormRef: '',
      searchFormData: {
        materialCode: '',
        materialName: ''
      },
      // 初始化 searchFormOptions
      searchFormOptions: {
        formProps: {
          "label-width": "100px"
        },
        formItem: {
          materialCode: {
            label: this.$t("wmsoutinvoicedtl.materialCode"),
            rules: [],
            type: "input"
          },
          materialName: {
            label: this.$t("wmsoutinvoicedtl.materialName"),
            rules: [],
            type: "input"
          }
        }
      }
    };
  }

  // 删除行的方法
  deleteRow(index) {
    this.tableData.splice(index, 1);
  }

  // 打开新弹窗的方法
  openNewDialog() {
    this.dialogVisible = true;
    this.searchData();
  }

  // 分页切换时的方法
  handlePageChange(page) {
    this.currentPage = page;
    this.searchData();
  }

  // 查询数据方法
  searchData() {
    let url = "/api/wmsoutinvoice/getstockdtlbymaterial";
    console.log("调用接口地址："+url);
    console.log("调用接口参数："+this.searchFormData.materialCode+":"+this.searchFormData.materialName);
    axios.post(url, {
      page: this.currentPage,
      pageSize: this.pageSize,
      materialCode: this.searchFormData.materialCode || '',
      materialName: this.searchFormData.materialName || ''
      // 添加其他搜索条件参数
    }, {
      headers: {
        'Content-Type': 'application/json'
      }
    })
 .then(response => {
      if (response.status === 200) {
        this.dialogTableData = response.data.item || [];
        this.totalItems = response.data.total;
      } else {
        console.error('获取数据错误：1', response.msg);
      }
    })
 .catch(error => {
      console.error('获取数据错误：2', error);
    });
  }

  // 重置搜索条件方法
  resetSearch() {
    this.searchFormData = {
      materialCode: '',
      materialName: ''
    };
    this.currentPage = 1;
    this.searchData();
  }

  // 确定按钮点击时将弹窗表格数据添加到外部表格数据中
  addDataFromDialog() {
    // 获取选中的行数据
    const selectedRows = this.dialogTableData.filter(row => row.selected);
    const newData = selectedRows.map(selectedRow => {
      // 根据字段映射关系进行赋值
      return {
        materialCode: selectedRow.materialCode,
        materialName: selectedRow.materialName,
        materialSpec: selectedRow.materialSpec,
        invoiceQty: selectedRow.qty - selectedRow.occupyQty,
        erpWhouseNo: selectedRow.erpWhouseNo
        // 继续进行其他字段的赋值
      };
    });
    this.tableData.push(...newData);
    this.dialogVisible = false;
    // 可以根据需要重置弹窗表格数据
    this.dialogTableData = [];
  }
}
</script>

<style>
/* 可根据实际情况调整样式 */
.table-container {
  position: relative;
}
</style>