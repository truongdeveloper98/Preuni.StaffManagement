import { store } from "stores";
import { setAuth, failed } from "stores/reducers/auth.reducer";
import API from "./sign-in.api";

export const loginRequest = async ({ email, password }) => {
  const { dispatch } = store;
  try {
    const request = await API.login(email, password);
    dispatch(setAuth(request.data));
  } catch (error) {
    dispatch(failed(error?.response?.data?.title));
  }
};
