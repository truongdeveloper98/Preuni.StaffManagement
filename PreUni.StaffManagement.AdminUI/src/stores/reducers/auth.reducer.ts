import { createSlice } from "@reduxjs/toolkit";

export interface AuthState {
  email: "" | undefined;
  firstName: "";
  lastName: "";
  id: number | undefined;
  roles: string[];
  token: "" | undefined;
  error: "" | undefined;
}

const initialState: AuthState = {
  email: undefined,
  firstName: "",
  id: undefined,
  lastName: "",
  roles: [],
  token: undefined,

  error: undefined,
};

const slice = createSlice({
  name: "auth",
  initialState,
  reducers: {
    setAuth: (state, action) => {
      state.email = action.payload.email;
      state.firstName = action.payload.firstName;
      state.id = action.payload.id;
      state.lastName = action.payload.lastName;
      state.roles = action.payload.roles;
      state.token = action.payload.token;
    },

    failed: (state, action) => {
      state.error = action.payload;
    },

    reinitialize: (state) => {
      state.error = undefined;
    },
  },
});

export const { setAuth, failed, reinitialize } = slice.actions;

export default slice.reducer;
