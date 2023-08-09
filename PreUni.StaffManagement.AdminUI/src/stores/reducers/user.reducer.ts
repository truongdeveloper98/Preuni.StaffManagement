import { createSlice } from "@reduxjs/toolkit";

export interface UserState {
  users: {
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

const initialState: UserState = {
  users: {
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

const usersSlice = createSlice({
  name: "user",
  initialState,
  reducers: {
    // request
    requested: (state) => {
      state.isLoading = true;
      state.error = undefined;
    },
    usersSuccess: (state, action) => {
      state.isLoading = false;
      state.users = action.payload;
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

export const { requested, failed, succeed, usersSuccess, reinitialize } = usersSlice.actions;
export default usersSlice.reducer;
