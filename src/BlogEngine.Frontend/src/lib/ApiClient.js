import axios from "axios";

const apiBase = "http://localhost:5001/api/";

export class Article {
  static async post(item) {
    var url = `${apiBase}${"Article"}`;
    return await axios.post(url, item).then((res) => console.log(res.data));
  }

  static async put(item) {
    var url = `${apiBase}${"Article"}`;
    return await axios.put(url, item).then((res) => console.log(res.data));
  }

  static async get() {
    var url = `${apiBase}${"Article"}`;
    return await axios.get(url).then((res) => console.log(res.data));
  }

  static async getById(id) {
    var url = `${apiBase}${"Article"}/${id}`;
    return await axios.get(url).then((res) => console.log(res.data));
  }

  static async getPaged(page, capacity) {
    var url = `${apiBase}${"Article"}/pages?Page=${page}&Capacity=${capacity}`;
    return await axios.get(url).then((res) => console.log(res.data));
  }

  static async delete(id) {
    var url = `${apiBase}${"Article"}/${id}`;
    return await axios.delete(url).then((res) => console.log(res.data));
  }
}

export class Comment {
  static async post(item) {
    var url = `${apiBase}${"Comment"}`;
    return await axios.post(url, item).then((res) => console.log(res.data));
  }

  static async put(item) {
    var url = `${apiBase}${"Comment"}`;
    return await axios.put(url, item).then((res) => console.log(res.data));
  }

  static async get() {
    var url = `${apiBase}${"Comment"}`;
    return await axios.get(url).then((res) => console.log(res.data));
  }

  static async getById(id) {
    var url = `${apiBase}${"Comment"}/${id}`;
    return await axios.get(url).then((res) => console.log(res.data));
  }

  static async getPaged(page, capacity) {
    var url = `${apiBase}${"Comment"}/pages?Page=${page}&Capacity=${capacity}`;
    return await axios.get(url).then((res) => console.log(res.data));
  }

  static async delete(id) {
    var url = `${apiBase}${"Comment"}/${id}`;
    return await axios.delete(url).then((res) => console.log(res.data));
  }
}

export class User {
  static async post(item) {
    var url = `${apiBase}${"User"}`;
    return await axios.post(url, item).then((res) => console.log(res.data));
  }

  static async put(item) {
    var url = `${apiBase}${"User"}`;
    return await axios.put(url, item).then((res) => console.log(res.data));
  }

  static async get() {
    var url = `${apiBase}${"User"}`;
    return await axios.get(url).then((res) => console.log(res.data));
  }

  static async getById(id) {
    var url = `${apiBase}${"User"}/${id}`;
    return await axios.get(url).then((res) => console.log(res.data));
  }

  static async getPaged(page, capacity) {
    var url = `${apiBase}${"User"}/pages?Page=${page}&Capacity=${capacity}`;
    return await axios.get(url).then((res) => console.log(res.data));
  }

  static async delete(id) {
    var url = `${apiBase}${"User"}/${id}`;
    return await axios.delete(url).then((res) => console.log(res.data));
  }
}

export class Auth {
  static async register(item) {
    var url = `${apiBase}register`;
    return await axios.post(url, item).then((res) => console.log(res));
  }

  static async login(item) {
    var url = `${apiBase}login`;
    return await axios.post(url, item).then((res) => console.log(res));
  }

  static async exit() {
    var url = `${apiBase}exit`;
    return await axios.post(url).then((res) => console.log(res));
  }

  static async me() {
    var url = `${apiBase}me`;
    return await axios.get(url).then((res) => console.log(res));
  }
}
