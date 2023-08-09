import { store } from "stores";
import { failed, jobApplicationsSuccess, requested } from "stores/reducers/jobApplication.reducer";
import API from "./jobApplication.api";

export const getJobApplicationsRequest = async (params: any) => {
  const { dispatch } = store;
  try {
    dispatch(requested());
    const response = await API.getJobApplications(params);
    if (response.data) {
      dispatch(jobApplicationsSuccess(response.data));
    }
  } catch (error) {
    dispatch(failed(error.response?.data));
  }
};

export const deleteJobApplicationsRequest = async (id: string) => {
  const { dispatch } = store;
  try {
    dispatch(requested());
    const response = await API.deleteJobApplications(id);
    if (response.data) {
      dispatch(jobApplicationsSuccess(response.data));
    }
  } catch (error) {
    dispatch(failed(error.response?.data));
  }
};
