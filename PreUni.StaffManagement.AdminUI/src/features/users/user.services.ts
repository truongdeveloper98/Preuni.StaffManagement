import { failed, requested, usersSuccess } from "stores/reducers/user.reducer";
import { store } from "stores";
import API from "./users.api";

export const getUsersRequest = async (params: any) => {
  const { dispatch } = store;
  try {
    dispatch(requested());
    const response = await API.getUsers(params);
    if (response.data) {
      dispatch(usersSuccess(response.data));
    }
  } catch (error) {
    dispatch(failed(error.response?.data));
  }
};

export const deleteUserRequest = async (id: string, callback: any) => {
  const { dispatch } = store;

  try {
    dispatch(requested());
    const response = await API.deleteUser(id);
    if (response.data) {
      dispatch(usersSuccess(response.data));
      if (callback) callback();
    }
  } catch (error) {
    dispatch(failed(error.response?.data));
  }
};
