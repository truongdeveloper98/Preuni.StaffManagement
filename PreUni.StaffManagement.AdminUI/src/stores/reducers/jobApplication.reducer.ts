import { createSlice } from "@reduxjs/toolkit";

export interface JobApplicationState {
  jobApplications: {
    items: [];
    limit: number | undefined;
    page: number | undefined;
    totalItems: number | undefined;
    totalPages: number | undefined;
  };
  isLoading: boolean;
  error: any;
  success: any;
}

const initialState: JobApplicationState = {
  jobApplications: {
    items: [],
    limit: undefined,
    page: undefined,
    totalItems: undefined,
    totalPages: undefined,
  },
  isLoading: false,
  error: undefined,
  success: undefined,
};

const jobApplicationsSlice = createSlice({
  name: "jobApplication",
  initialState,
  reducers: {
    // request
    requested: (state) => {
      state.isLoading = true;
      state.error = undefined;
    },
    jobApplicationsSuccess: (state, action) => {
      state.isLoading = false;
      state.jobApplications = action.payload;
    },
    failed: (state, action) => {
      state.isLoading = false;
      state.error = action.payload;
    },
    succeed: (state, action) => {
      state.isLoading = false;
      state.success = action.payload;
    },
    reinitialize: (state) => {
      state.error = undefined;
      state.success = undefined;
    },
  },
});

export const { requested, failed, succeed, jobApplicationsSuccess, reinitialize } =
  jobApplicationsSlice.actions;
export default jobApplicationsSlice.reducer;
