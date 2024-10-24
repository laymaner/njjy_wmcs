import Vuex from "vuex";
import api from "./api";
import createStore from "@/store/base/index";
const newStore = createStore(api);
// 可以打印查看store结构
console.log('newStore:', newStore);
console.log('api:',api);
export default new Vuex.Store({
  strict: true,
  getters: {},
  ...newStore
});
