import { failed, requested, succeed, usersSuccess } from "stores/reducers/user.reducer";
import API from "./userDetail.api";
import { store } from "stores";

export const getUserRequest = async (id: string, callback: any) => {
  const { dispatch } = store;

  try {
    dispatch(requested());
    const response = await API.getUserById(id);
    if (response.data) {
      if (callback) callback(response.data);
    }
  } catch (error) {
    dispatch(failed(error.response?.data));
  }
};

export const createUserRequest = async (data: any, callback: any) => {
  const { dispatch } = store;

  try {
    dispatch(requested());
    await API.createUser(data);
    dispatch(succeed("User created successfully"));
    if (callback) callback();
  } catch (error) {
    dispatch(failed(error.response?.data));
  }
};

export const updateUserRequest = async (id: string, body: any, callback: any) => {
  const { dispatch } = store;

  try {
    dispatch(requested());
    const response = await API.updateUser(body, id);
    if (response.data) {
      dispatch(usersSuccess(response.data));
      if (callback) callback();
    }
  } catch (error) {
    dispatch(failed(error.response?.data));
  }
};
