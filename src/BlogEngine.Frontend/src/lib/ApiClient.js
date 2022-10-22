import axios from "axios";

const apiBase = "http://localhost:5001/api/";

export class Article {
  static async post(item) {
    const config = {
      headers: { Authorization: `Bearer ${localStorage.getItem("jwt")}` },
    };
    var url = `${apiBase}${"Article"}`;
    return await axios.post(url, item, config).then((res) => {
      console.log(res);
      return res;
    });
  }

  static async put(item) {
    const config = {
      headers: { Authorization: `Bearer ${localStorage.getItem("jwt")}` },
    };
    var url = `${apiBase}${"Article"}`;
    return await axios.put(url, item, config).then((res) => {
      console.log(res);
      return res;
    });
  }

  static async get() {
    var url = `${apiBase}${"Article"}`;
    return await axios.get(url).then((res) => {
      console.log(res);
      return res;
    });
  }

  static async getById(id) {
    var url = `${apiBase}${"Article"}/${id}`;
    return await axios.get(url).then((res) => {
      console.log(res);
      return res;
    });
  }

  static async getPaged(page, capacity) {
    var url = `${apiBase}${"Article"}/pages?Page=${page}&Capacity=${capacity}`;
    return await axios.get(url).then((res) => {
      console.log(res);
      return res;
    });
  }

  static async delete(id) {
    const config = {
      headers: { Authorization: `Bearer ${localStorage.getItem("jwt")}` },
    };
    var url = `${apiBase}${"Article"}/${id}`;
    return await axios.delete(url, config).then((res) => {
      console.log(res);
      return res;
    });
  }
}

export class Issue {
  static async post(item) {
    const config = {
      headers: { Authorization: `Bearer ${localStorage.getItem("jwt")}` },
    };
    var url = `${apiBase}${"Issue"}`;
    return await axios.post(url, item, config).then((res) => {
      console.log(res);
      return res;
    });
  }

  static async put(item) {
    const config = {
      headers: { Authorization: `Bearer ${localStorage.getItem("jwt")}` },
    };
    var url = `${apiBase}${"Issue"}`;
    return await axios.put(url, item, config).then((res) => {
      console.log(res);
      return res;
    });
  }

  static async get() {
    var url = `${apiBase}${"Issue"}`;
    return await axios.get(url).then((res) => {
      console.log(res);
      return res;
    });
  }

  static async getById(id) {
    var url = `${apiBase}${"Issue"}/${id}`;
    return await axios.get(url).then((res) => {
      console.log(res);
      return res;
    });
  }

  static async getPaged(page, capacity) {
    var url = `${apiBase}${"Issue"}/pages?Page=${page}&Capacity=${capacity}`;
    return await axios.get(url).then((res) => {
      console.log(res);
      return res;
    });
  }

  static async delete(id) {
    const config = {
      headers: { Authorization: `Bearer ${localStorage.getItem("jwt")}` },
    };
    var url = `${apiBase}${"Issue"}/${id}`;
    return await axios.delete(url, config).then((res) => {
      console.log(res);
      return res;
    });
  }
}

export class Comment {
  static async post(item) {
    const config = {
      headers: { Authorization: `Bearer ${localStorage.getItem("jwt")}` },
    };
    var url = `${apiBase}${"Comment"}`;
    return await axios.post(url, item, config).then((res) => {
      console.log(res);
      return res;
    });
  }

  static async put(item) {
    const config = {
      headers: { Authorization: `Bearer ${localStorage.getItem("jwt")}` },
    };
    var url = `${apiBase}${"Comment"}`;
    return await axios.put(url, item, config).then((res) => {
      console.log(res);
      return res;
    });
  }

  static async get() {
    var url = `${apiBase}${"Comment"}`;
    return await axios.get(url).then((res) => {
      console.log(res);
      return res;
    });
  }

  static async getById(id) {
    var url = `${apiBase}${"Comment"}/${id}`;
    return await axios.get(url).then((res) => {
      console.log(res);
      return res;
    });
  }

  static async getPaged(page, capacity) {
    var url = `${apiBase}${"Comment"}/pages?Page=${page}&Capacity=${capacity}`;
    return await axios.get(url).then((res) => {
      console.log(res);
      return res;
    });
  }

  static async delete(id) {
    const config = {
      headers: { Authorization: `Bearer ${localStorage.getItem("jwt")}` },
    };
    var url = `${apiBase}${"Comment"}/${id}`;
    return await axios.delete(url, config).then((res) => {
      console.log(res);
      return res;
    });
  }
}

export class User {
  static async post(item) {
    const config = {
      headers: { Authorization: `Bearer ${localStorage.getItem("jwt")}` },
    };
    var url = `${apiBase}${"User"}`;
    return await axios.post(url, item, config).then((res) => {
      console.log(res);
      return res;
    });
  }

  static async put(item) {
    const config = {
      headers: { Authorization: `Bearer ${localStorage.getItem("jwt")}` },
    };
    var url = `${apiBase}${"User"}`;
    return await axios.put(url, item, config).then((res) => {
      console.log(res);
      return res;
    });
  }

  static async get() {
    const config = {
      headers: { Authorization: `Bearer ${localStorage.getItem("jwt")}` },
    };
    var url = `${apiBase}${"User"}`;
    return await axios.get(url, config).then((res) => {
      console.log(res);
      return res;
    });
  }

  static async getById(id) {
    const config = {
      headers: { Authorization: `Bearer ${localStorage.getItem("jwt")}` },
    };
    var url = `${apiBase}${"User"}/${id}`;
    return await axios.get(url, config).then((res) => {
      console.log(res);
      return res;
    });
  }

  static async getPaged(page, capacity) {
    const config = {
      headers: { Authorization: `Bearer ${localStorage.getItem("jwt")}` },
    };
    var url = `${apiBase}${"User"}/pages?Page=${page}&Capacity=${capacity}`;
    return await axios.get(url, config).then((res) => {
      console.log(res);
      return res;
    });
  }

  static async delete(id) {
    const config = {
      headers: { Authorization: `Bearer ${localStorage.getItem("jwt")}` },
    };
    var url = `${apiBase}${"User"}/${id}`;
    return await axios.delete(url, config).then((res) => {
      console.log(res);
      return res;
    });
  }
}

export class Auth {
  static async register(item) {
    var url = `${apiBase}register`;
    return await axios.post(url, item).then((res) => {
      console.log(res);
      return res;
    });
  }

  static async login(item) {
    var url = `${apiBase}login`;
    return await axios.post(url, item).then((res) => {
      console.log(res.data);
      localStorage.setItem("jwt", res.data);
    });
  }

  static async exit() {
    var url = `${apiBase}exit`;
    return await axios.post(url).then((res) => {
      console.log(res);
      localStorage.setItem("jwt", null);
      return res;
    });
  }

  static async me() {
    const config = {
      headers: { Authorization: `Bearer ${localStorage.getItem("jwt")}` },
    };
    var url = `${apiBase}me`;
    return await axios.get(url, config).then((res) => {
      console.log(res);
      return res;
    });
  }
}
