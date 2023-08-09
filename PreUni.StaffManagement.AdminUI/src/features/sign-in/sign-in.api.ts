import api from "services/api";

const apis = {
  login: "Authentications/Login",
};

const API = {
  login: (email, password) => api.post(apis.login, { email, password }),
};

export default API;
