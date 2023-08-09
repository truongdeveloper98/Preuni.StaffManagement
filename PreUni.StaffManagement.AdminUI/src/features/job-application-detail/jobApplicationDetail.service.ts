import {
  failed,
  jobApplicationsSuccess,
  requested,
  succeed,
} from "stores/reducers/jobApplication.reducer";
import API from "./jobApplicationDetail.api";
import { store } from "stores";

export const getJobApplicationRequest = async (id: string, callback: any) => {
  const { dispatch } = store;

  try {
    dispatch(requested());
    const response = await API.getJobApplicationById(id);
    if (response.data) {
      if (callback) callback(response.data);
    }
  } catch (error) {
    dispatch(failed(error.response?.data));
  }
};

export const createJobApplicationRequest = async (data: any, callback: any) => {
  const { dispatch } = store;

  try {
    dispatch(requested());
    await API.createJobApplication(data);
    dispatch(succeed("Job application created successfully"));
    if (callback) callback();
  } catch (error) {
    dispatch(failed(error.response?.data));
  }
};

export const updateJobApplicationRequest = async (id: string, body: any, callback: any) => {
  const { dispatch } = store;

  try {
    dispatch(requested());
    const response = await API.updateJobApplication(body, id);
    if (response.data) {
      dispatch(jobApplicationsSuccess(response.data));
      if (callback) callback();
    }
  } catch (error) {
    dispatch(failed(error.response?.data));
  }
};
